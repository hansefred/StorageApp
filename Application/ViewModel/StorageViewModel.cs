using Domain.DomainModels;

namespace Application.ViewModel
{
    public class StorageViewModel
    {
        public StorageViewModel()
        {
            
        }

        public StorageViewModel(Guid id, string storageName, IEnumerable<Article> articelViewModels)
        {
            ID = id.ToString();
            StorageName = storageName;
            ArticelViewModels = articelViewModels.Select(article => new ArticelViewModel(article.ID, article.ArticleName, article.Description, article.Created, article.Updated, article.Storage));
        }

        public string ID { get; set; } = String.Empty;

        public string StorageName { get; set; } = String.Empty;

        public IEnumerable<ArticelViewModel> ArticelViewModels { get; set; } = new List<ArticelViewModel>();
    }
}
