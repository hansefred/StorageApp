namespace Domain.DomainModels.Events
{
    internal class StorageCreatedEvent : StorageEvent
    {
        public StorageCreatedEvent(Guid id, string storageName, IEnumerable<Article> articles)
        {
            ID = id;
            StorageName = storageName;
            Articles = articles;
        }
    }
}
