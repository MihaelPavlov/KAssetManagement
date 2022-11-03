using EventBus.Base;
using EventBus.Base.Events;
using Newtonsoft.Json;
using Polly;
using Polly.Retry;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;

namespace EventBus.RabbitMQ
{
    public class EventBusRaabitMQ : BaseEventBus
    {
        RabbitMQPersistentConnection persistentConnection;
        private readonly IConnectionFactory connectionFactory;
        private readonly IModel consumerChannel;
        private string defaultTopicName;
        private int connectionRetryCount; 

        public EventBusRaabitMQ(EventBusConfig config, IServiceProvider serviceProvider) : base(config, serviceProvider)
        {
            connectionRetryCount = config.ConnectionRetryCount;
            defaultTopicName = config.DefaultTopicName;

            if (config.Connection != null)
            {
                var connJson = JsonConvert.SerializeObject(config.Connection, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                });

                this.connectionFactory = JsonConvert.DeserializeObject<ConnectionFactory>(connJson);
            }
            else
            {
                this.connectionFactory = new ConnectionFactory();
            }
            persistentConnection = new RabbitMQPersistentConnection(this.connectionFactory, config.ConnectionRetryCount);

            consumerChannel = CreateConsumerChannel();

            this.SubsManager.OnEventRemoved += SubsManager_OnEventRemoved;
        }

        private void SubsManager_OnEventRemoved(object sender, string eventName)
        {
            eventName = ProcessEventName(eventName);

            if (!persistentConnection.IsConnection)
            {
                persistentConnection.TryConnect();
            }

            consumerChannel.QueueUnbind(queue: eventName,
                exchange: defaultTopicName,
                routingKey: eventName);

            if (this.SubsManager.IsEmpty)
            {
                consumerChannel.Close();
            }

        }

        public override void Publish(IntegrationEvent @event)
        {
            if (!persistentConnection.IsConnection)
            {
                persistentConnection.TryConnect();
            }

            var policy = RetryPolicy.Handle<BrokerUnreachableException>()
                .Or<SocketException>()
                .WaitAndRetry(connectionRetryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) =>
                {
                });

            var eventName = @event.GetType().Name;
            eventName = ProcessEventName(eventName);

            consumerChannel.ExchangeDeclare(exchange: defaultTopicName, type: "direct");

            var message = JsonConvert.SerializeObject(@event);
            var body = Encoding.UTF8.GetBytes(message);

            policy.Execute(() =>
            {
                var properties = consumerChannel.CreateBasicProperties();
                properties.DeliveryMode = 2; // persistent


                consumerChannel.QueueDeclare(queue: GetSubName(eventName),
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                consumerChannel.BasicPublish(
                    exchange: defaultTopicName,
                    routingKey: eventName,
                    mandatory: true,
                    basicProperties: properties,
                    body: body);
            });
        }

        public override void Subscribe<T, TH>()
        {
            var eventName = typeof(T).Name;
            eventName = this.ProcessEventName(eventName);

            if (!this.SubsManager.HasSubscriptionsForEvent(eventName))
            {
                if (!persistentConnection.IsConnection)
                {
                    persistentConnection.TryConnect();
                }

                consumerChannel.QueueDeclare(queue: GetSubName(eventName),
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                consumerChannel.QueueBind(queue: GetSubName(eventName),
                    exchange: defaultTopicName,
                    routingKey: eventName);
            }

            this.SubsManager.AddSubscription<T, TH>();
            StartBasicConsume(eventName);
        }

        public override void Unsubscribe<T, TH>()
        {
            this.SubsManager.RemoveSubscription<T, TH>();
        }

        private IModel CreateConsumerChannel()
        {
            if (!this.persistentConnection.IsConnection)
            {
                this.persistentConnection.TryConnect();
            }

            var channel = this.persistentConnection.CreateModel();

            channel.ExchangeDeclare(exchange: defaultTopicName, type: "direct");

            return channel;
        }

        private void StartBasicConsume(string evenName)
        {
            if (consumerChannel != null)
            {
                var consumer = new EventingBasicConsumer(consumerChannel);

                consumer.Received += Consumer_Receiver;

                consumerChannel.BasicConsume(
                    queue: GetSubName(evenName),
                    autoAck: false,
                    consumer: consumer);
            }
        }

        private async void Consumer_Receiver(object sender, BasicDeliverEventArgs eventArgs)
        {
            var eventName = eventArgs.RoutingKey;

            eventName = ProcessEventName(eventName);

            var message = Encoding.UTF8.GetString(eventArgs.Body.Span);

            try
            {
                await ProcessEvent(eventName, message);
            }
            catch (Exception ex)
            {

            }

            consumerChannel.BasicAck(eventArgs.DeliveryTag, multiple: false);
        }
    }
}
