namespace Domain.DomainModels
{
    public class Article
    {
        public Guid ID { get; set; } = Guid.NewGuid();
        public string ArticleName { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Updated { get; set; } = DateTime.Now;
        public Storage Storage { get; set; }


        public Article()
        {
            Storage = new Storage();  
        }

        public Article(Storage storage)
        {
            Storage = storage;
        }
    }
}
