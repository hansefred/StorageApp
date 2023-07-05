namespace Domain.DomainModels.Interfaces
{
    public interface IArticleRepository
    {
        Task CreateArticle(Article article);
        Task DeleteArticle(Guid ID);
        Task<IEnumerable<Article>> GetAll();
        Task<Article?> GetArticlebyID(Guid ID);
        Task UpdateArticle(Article article);
    }
}