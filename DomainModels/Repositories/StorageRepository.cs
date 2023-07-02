using Microsoft.Extensions.Logging;
using System.Data;
using Dapper;
using Domain.DomainModels;
using Infrastructure.Interfaces;
using Infrastructure.Exceptions;
using Infrastructure.Factories;

namespace Infrastructure.Repositories
{
    public class StorageRepository : IStorageRepository
    {
        private readonly SQLConnectionFactory _sqlConnectionFactory;
        private readonly ILogger<StorageRepository> _logger;

        public StorageRepository(string DBConnectionString, ILogger<StorageRepository> logger)
        {
            _sqlConnectionFactory = new SQLConnectionFactory(DBConnectionString);
            _logger = logger;
        }

        public async Task<IEnumerable<Storage>> GetAll()
        {
            try
            {
                var storageDictionary = new Dictionary<Guid, Storage>();
                using (var dbConnection = _sqlConnectionFactory.GetSqlConnection())
                {
                    return await dbConnection.QueryAsync<Storage, Article, Storage>("[dbo].[sp_GetStorages]", (s, a) =>
                {
                    Storage? storageQuery;


                    if (!storageDictionary.TryGetValue(s.ID, out storageQuery))
                    {
                        storageQuery = s;
                        storageQuery.Articles = new List<Article>();
                        storageDictionary.Add(storageQuery.ID, storageQuery);

                    }
                    if (a is not null)
                    {
                        storageQuery.Articles.Add(a);
                    }


                    return storageQuery;

                }, splitOn: "Split", commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error execute sp_GetStorages: {Exception}", ex);
                return Enumerable.Empty<Storage>();
            }




        }
        public async Task<Storage?> GetStoragebyID(Guid ID)
        {

            try
            {
                var storageDictionary = new Dictionary<Guid, Storage>();
                using (var dbConnection = _sqlConnectionFactory.GetSqlConnection())
                {
                    var r = await dbConnection.QueryAsync<Storage, Article, Storage>("[dbo].[sp_GetStorageByID]", (s, a) =>
                {
                    Storage? storageQuery;


                    if (!storageDictionary.TryGetValue(s.ID, out storageQuery))
                    {
                        storageQuery = s;
                        storageQuery.Articles = new List<Article>();
                        storageDictionary.Add(storageQuery.ID, storageQuery);

                    }
                    if (a is not null)
                    {
                        storageQuery.Articles.Add(a);
                    }


                    return storageQuery;

                }, splitOn: "Split", param: new { ID = ID }, commandType: CommandType.StoredProcedure);
                    return r.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error execute sp_GetStorages: {Exception}", ex);
                throw;
            }
        }

        public async Task CreateStorage(Storage storage)
        {
            try
            {
                using (var dbConnection = _sqlConnectionFactory.GetSqlConnection())
                {

                    var rows = await dbConnection.ExecuteAsync("[dbo].[sp_CreateStorage]", new { Name = storage.StorageName }, commandType: CommandType.StoredProcedure); ;
                    if (rows < 1)
                    {
                        throw new CreateStorageException("No Row affected");
                    }
                }

            }
            catch (Exception ex)
            {
                _logger.LogError("Error execute sp_CreateStorage: {Exception}", ex);
                throw;
            }
        }

        public async Task UpdateStorage(Storage storage)
        {
            try
            {
                using (var dbConnection = _sqlConnectionFactory.GetSqlConnection())
                {
                    var rows = await dbConnection.ExecuteAsync("[dbo].[sp_UpdateStorage]", new { Id = storage.ID, Name = storage.StorageName }, commandType: CommandType.StoredProcedure);

                    if (rows < 1)
                    {
                        throw new UpdateStorageException("No Row affected");
                    }
                }

            }
            catch (Exception ex)
            {
                _logger.LogError("Error execute sp_UpdateStorage: {Exception}", ex);
                throw;
            }
        }

        public async Task DeleteStorage(Guid ID)
        {
            try
            {
                using (var dbConnection = _sqlConnectionFactory.GetSqlConnection())
                {
                    var rows = await dbConnection.ExecuteAsync("[dbo].[sp_DeleteStorage]", new { Id = ID }, commandType: CommandType.StoredProcedure);
                }


            }
            catch (Exception ex)
            {
                _logger.LogError("Error execute sp_DeleteStorage: {Exception}", ex);
                throw;
            }
        }
    }
}
