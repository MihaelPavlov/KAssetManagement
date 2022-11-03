using EventBus.Base.Abstraction;
using EventBus.Base.SubManagers;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace EventBus.Base.Events
{
    public abstract class BaseEventBus : IEventBus
    {
        public readonly IServiceProvider ServiceProvider;
        public readonly IEventBusSubscriptionsManager SubsManager;

        private EventBusConfig eventBusConfig;

        public BaseEventBus(EventBusConfig config, IServiceProvider serviceProvider)
        {
            eventBusConfig = config;
            ServiceProvider = serviceProvider;
            SubsManager = new InMemoryEventBusSubscriptionsManager(ProcessEventName);
        }

        public virtual string ProcessEventName(string eventName)
        {
            if (this.eventBusConfig.DeleteEventPrefix)
                eventName = eventName.TrimStart(this.eventBusConfig.EventNamePrefix.ToArray());

            if (this.eventBusConfig.DeleteEventSuffix)
                eventName = eventName.TrimStart(this.eventBusConfig.EventNameSuffix.ToArray());

            return eventName;
        }

        public virtual string GetSubName(string eventName)
        {
            return $"{this.eventBusConfig.SubscriberClientAppName}.{ProcessEventName(eventName)}";
        }

        public virtual void Dispose()
        {
            eventBusConfig = null;
        }

        public async Task<bool> ProcessEvent(string eventName, string message)
        {
            eventName = ProcessEventName(eventName);

            var processed = false;

            if (this.SubsManager.HasSubscriptionsForEvent(eventName))
            {
                var subscriptions = this.SubsManager.GetHandlersForEvent(eventName);

                using (var scope = this.ServiceProvider.CreateScope())
                {
                    foreach (var subsrciption in subscriptions)
                    {
                        var handler = this.ServiceProvider.GetService(subsrciption.HandlerType);

                        if (handler == null)
                            continue;

                        var eventType = this.SubsManager.GetEventTypeByName($"{this.eventBusConfig.EventNamePrefix}{eventName}{this.eventBusConfig.EventNameSuffix}");
                        var integrationEvent = JsonConvert.DeserializeObject(message, eventType);

                        var concreteType = typeof(IIntegrationEventHandler<>).MakeGenericType(eventType);
                        await (Task)concreteType.GetMethod("Handle").Invoke(handler, new object[] { integrationEvent });
                    }
                }

                processed = true;
            }

            return processed;
        }

        public abstract void Publish(IntegrationEvent @event);

        public abstract void Subscribe<T, TH>() where T : IntegrationEvent where TH : IIntegrationEventHandler<T>;

        public abstract void Unsubscribe<T, TH>() where T : IntegrationEvent where TH : IIntegrationEventHandler<T>;
    }
}
