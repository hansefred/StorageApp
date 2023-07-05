namespace Domain.DomainModels.Commands
{
    public class StorageCommand
    {
        public Guid ID { get; set; }
        public string StorageName { get; set; } = String.Empty;
       public IEnumerable<Article> Articles { get; set; } = new List<Article>();


    }
}
