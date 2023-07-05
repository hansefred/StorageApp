namespace Domain.DomainModels.Events
{
    internal class StorageDeletedEvent : StorageEvent
    {
        public StorageDeletedEvent(Guid id)
        {
            ID = id;
        }
    }
}
