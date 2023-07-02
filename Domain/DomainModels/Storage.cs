using System.ComponentModel.DataAnnotations.Schema;


namespace Domain.DomainModels
{
    public class Storage
    {
        public Guid ID { get; set; } = Guid.NewGuid();
        public string StorageName { get; set; } = String.Empty;
        public List<Article> Articles { get; set; } = new List<Article>();


        public Storage()
        {
            
        }

    }
}
