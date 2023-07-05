namespace Domain.DomainModels.Events
{
    internal class StorageNameUpdatedEvent : StorageEvent
    {
        public StorageNameUpdatedEvent(Guid id, string storageName)
        {
            ID = id;
            StorageName = storageName;
        }
    }
}
