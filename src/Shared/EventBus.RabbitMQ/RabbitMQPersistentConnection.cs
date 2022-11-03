using Polly;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System.Data;
using System.Net.Sockets;

namespace EventBus.RabbitMQ
{
    public class RabbitMQPersistentConnection : IDisposable
    {
        private readonly IConnectionFactory connectionFactory;
        private readonly int retryCount;
        private IConnection connection;
        private object lock_object = new object();
        private bool _disposed;

        public RabbitMQPersistentConnection(IConnectionFactory connectionFactory, int retryCount = 5)
        {
            this.connectionFactory = connectionFactory;
            this.retryCount = retryCount;
        }

        public bool IsConnection => this.connection != null && this.connection.IsOpen;

        public IModel CreateModel()
        {
            return this.connection.CreateModel();
        }

        public void Dispose()
        {
            this._disposed = true;
            this.connection.Dispose();
        }

        public bool TryConnect()
        {
            lock (lock_object)
            {
                var policy = Policy.Handle<SocketException>()
                    .Or<BrokerUnreachableException>()
                    .WaitAndRetry(this.retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) =>
                    {

                    });

                policy.Execute(() =>
                {
                    this.connection = connectionFactory.CreateConnection();
                });

                if (this.IsConnection)
                {
                    connection.ConnectionShutdown += Connection_ConnectionShutdown;
                    connection.CallbackException += Connection_CallbackException;
                    connection.ConnectionBlocked += Connection_ConnectionBlocked;
                    return true;
                }

                return false;
            }
        }

        private void Connection_ConnectionShutdown(object? sender, ShutdownEventArgs e)
        {
            if (!this._disposed) return;

            TryConnect();
        }

        private void Connection_ConnectionBlocked(object? sender, ConnectionBlockedEventArgs e)
        {
            if (!this._disposed) return;

            TryConnect();
        }

        private void Connection_CallbackException(object? sender, CallbackExceptionEventArgs e)
        {
            if (!this._disposed) return;

            TryConnect();
        }

    }
}
