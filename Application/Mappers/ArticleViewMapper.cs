using Application.ViewModel;
using Domain.DomainModels;

namespace Application.Mappers
{
    internal class ArticleViewMapper
    {
        public IEnumerable<ArticelViewModel> ConstructFromList(IEnumerable<Article> articles)
        {
            return articles.Select(ConstructFormEntity);
        }

        public ArticelViewModel ConstructFormEntity(Article article)
        {
            return new ArticelViewModel(article.ID, article.ArticleName, article.Description, article.Created, article.Updated, article.Storage);
        }
    }
}
