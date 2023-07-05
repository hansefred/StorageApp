using Dapper;
using System.Data;
using Microsoft.Extensions.Logging;
using Domain.DomainModels;
using Infrastructure.Exceptions;
using Infrastructure.Factories;
using Domain.DomainModels.Interfaces;

namespace Infrastructure.Repositories
{

    public class ArticleRepository : IArticleRepository
    {
        private readonly SQLConnectionFactory _sqlConnectionFactory;
        private readonly ILogger<ArticleRepository> _logger;

        public ArticleRepository(string ConnectionString, ILogger<ArticleRepository> logger)
        {
            _sqlConnectionFactory = new SQLConnectionFactory(ConnectionString);
            _logger = logger;
        }

        public async Task<IEnumerable<Article>> GetAll()
        {
            try
            {
                using (var dbConnection = _sqlConnectionFactory.GetSqlConnection())
                {
                    return await dbConnection.QueryAsync<Article, Storage, Article>("[dbo].[sp_GetArticles]", (a, s) =>
                    {
                        a.Storage = s;
                        return a;
                    }, splitOn: "Storage", commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error execute sp_GetArticles: {Exception}", ex);
                return Enumerable.Empty<Article>();
            }

        }
        public async Task<Article?> GetArticlebyID(Guid ID)
        {
            try
            {
                using (var dbConnection = _sqlConnectionFactory.GetSqlConnection())
                {
                    var r = await dbConnection.QueryAsync<Article, Storage, Article>("[dbo].[sp_GetArticledByID]", (a, s) =>
                {
                    a.Storage = s;
                    return a;
                }, param: new { Id = ID }, splitOn: "Storage", commandType: CommandType.StoredProcedure);
                    return r.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error execute sp_GetArticledByID: {Exception}", ex);
                throw;
            }
        }

        public async Task CreateArticle(Article article)
        {
            try
            {
                using (var dbConnection = _sqlConnectionFactory.GetSqlConnection())
                {
                    var rows = await dbConnection.ExecuteAsync("[dbo].[sp_CreateArticle]", new { Name = article.ArticleName, Description = article.Description, Storage = article.Storage.ID, Createdby = Guid.NewGuid() }, commandType: CommandType.StoredProcedure); ;
                    if (rows < 1)
                    {
                        throw new CreateArticleException("No Row affected");
                    }
                }

            }
            catch (Exception ex)
            {
                _logger.LogError("Error execute sp_CreateArticle: {Exception}", ex);
                throw;
            }
        }

        public async Task UpdateArticle(Article article)
        {
            try
            {
                using (var dbConnection = _sqlConnectionFactory.GetSqlConnection())
                {
                    var rows = await dbConnection.ExecuteAsync("[dbo].[sp_UpdateArticle]", new { Id = article.ID, Name = article.ArticleName, Description = article.Description, Storage = article.Storage.ID }, commandType: CommandType.StoredProcedure);

                    if (rows < 1)
                    {
                        throw new UpdateArticleException("No Row affected");
                    }
                }

            }
            catch (Exception ex)
            {
                _logger.LogError("Error execute sp_UpdateArticle: {Exception}", ex);
                throw;
            }
        }

        public async Task DeleteArticle(Guid ID)
        {
            try
            {
                using (var dbConnection = _sqlConnectionFactory.GetSqlConnection())
                {
                    var rows = await dbConnection.ExecuteAsync("[dbo].[sp_DeleteArticle]", new { Id = ID }, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error execute sp_DeleteArticle: {Exception}", ex);
                throw;
            }
        }
    }
}
