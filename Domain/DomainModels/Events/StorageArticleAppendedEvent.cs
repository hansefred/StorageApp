namespace Domain.DomainModels.Events
{
    internal class StorageArticleAppendedEvent : StorageEvent
    {
        public StorageArticleAppendedEvent(Guid id, IEnumerable<Article> articles)
        {
            ID = id;
            Articles = articles;
        }
    }
}
