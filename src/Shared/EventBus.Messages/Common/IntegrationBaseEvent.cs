namespace EventBus.Messages.Common
{
    public class IntegrationBaseEvent
    {
        public IntegrationBaseEvent()
        {
            this.Id = Guid.NewGuid();
            this.CreationDate = DateTime.Now;
        }

        public IntegrationBaseEvent(Guid id, DateTime creationDate)
        {
            Id = id;
            CreationDate = creationDate;
        }

        public Guid Id { get; private set; }

        public DateTime CreationDate { get; private set; }
    }
}
