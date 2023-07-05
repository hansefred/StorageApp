using Application.Mappers;
using Domain.DomainModels;

namespace Application.ViewModel
{
    public class ArticelViewModel
    {
        public ArticelViewModel()
        {
            
        }

        public ArticelViewModel(Guid id, string articleName, string description, DateTime created, DateTime updated, Storage storage )
        {
            ID = id.ToString();
            ArticleName = articleName;
            Description = description;
            Created = created.ToString("HH:mm dd.MM.yyyy");
            Updated = updated.ToString("HH:mm dd.MM.yyyy");

            var mapper = new StorageViewMapper();
            Storage = mapper.ConstructFormEntity(storage);
        }
        public string ID { get; set; } = String.Empty;
        public string ArticleName { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public string Created { get; set; } = String.Empty;
        public string Updated { get; set; } = String.Empty;
        public StorageViewModel Storage { get; set; } = new StorageViewModel();
    }
}
