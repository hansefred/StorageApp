using Microsoft.Extensions.Logging;
using DotNet.Testcontainers.Containers;
using Serilog;
using Xunit.Sdk;
using Xunit.Abstractions;
using Serilog.Extensions.Logging;
using Domain.DomainModels;
using Infrastructure.Repositories;
using Infrastructure.Tests.Helper;
using Domain.DomainModels.Interfaces;

namespace Infrastructure.Tests
{
    [Collection("Non-Parallel Collection")]
    public class InfrastructureArticleRepositoryTests : DBConfig, IAsyncLifetime
    {
        private readonly TestOutputHelper _logOutput;
        private IContainer? _dockerContainer;
        private DBConfig _dbConfig = new DBConfig();

        public InfrastructureArticleRepositoryTests(ITestOutputHelper logOutput)
        {
            _logOutput = (TestOutputHelper)logOutput;
            Log.Logger = new LoggerConfiguration()
                .WriteTo.TestOutput(_logOutput)
                .CreateLogger();
        }

        public async Task DisposeAsync()
        {
            if (_dockerContainer is not null)
            {
                await _dockerContainer.StopAsync();
            }
        }

        public  async Task InitializeAsync()
        {
            _dockerContainer = await DockerHelper.CreateDockerDatabase(_dbConfig);
            DatabaseHelper.WaitforSQLDB(_dbConfig.DBConnectionString, Log.Logger);
            DatabaseHelper.DeployDatabase(_dbConfig);

        }



        [Theory]
        [InlineData("TestArticle", "Test", "TestStorage")]
        public async Task ArticleRepository_Create_Returns_SingleList(string ArticleName, string ArticleDescription, string StorageName)
        {
            //Arrange
            IArticleRepository _refArticleRepository = new ArticleRepository(_dbConfig.DBConnectionString, new SerilogLoggerFactory(Log.Logger).CreateLogger<ArticleRepository>());
            IStorageRepository _refStorageRepository = new StorageRepository(_dbConfig.DBConnectionString, new SerilogLoggerFactory(Log.Logger).CreateLogger<StorageRepository>());
            Storage _dummyStorage = new Storage() { StorageName = StorageName };


            //Act
            await _refStorageRepository.CreateStorage(_dummyStorage);
            var queryStorage = _refStorageRepository.GetAll().Result.FirstOrDefault();
            var dummyArticle = new Article(queryStorage ?? new Storage()) { ArticleName = ArticleName, Description = ArticleDescription };
            await _refArticleRepository.CreateArticle(dummyArticle);

            var queryArticles = await _refArticleRepository.GetAll();
            var queryStorages = await _refStorageRepository.GetAll();

            //Assert 
            Assert.Single(queryArticles);
            Assert.Single(queryStorages);
            var firstStorage = queryStorages.First();
            Assert.Equal(1, firstStorage?.Articles.Count);
        }


        [Fact]
        public async Task ArticleRepository_GetAll_Returns_EmptyList()
        {
            //Arrange
            IArticleRepository _refArticleRepository = new ArticleRepository(_dbConfig.DBConnectionString, new SerilogLoggerFactory(Log.Logger).CreateLogger<ArticleRepository>());

            //Act
            var queryArticles = await _refArticleRepository.GetAll();

            //Assert 
            Assert.Empty(queryArticles);

        }


        [Theory]
        [InlineData("FirstName", "FirstDescription" ,"ChangedName", "ChangedDescription")]
        public async Task ArticleRepository_Create_Update_Returns_ChangedArticle(string FirstArticleName, string FirstArticleDescription, string ChangedArticleName, string ChangedArticleDescription)
        {
            //Arrange
            IArticleRepository _refArticleRepository = new ArticleRepository(_dbConfig.DBConnectionString, new SerilogLoggerFactory(Log.Logger).CreateLogger<ArticleRepository>());
            IStorageRepository _refStorageRepository = new StorageRepository(_dbConfig.DBConnectionString, new SerilogLoggerFactory(Log.Logger).CreateLogger<StorageRepository>());
            Storage _dummyStorage = new Storage() { StorageName = "TestStorage" };


            //Act
            await _refStorageRepository.CreateStorage(_dummyStorage);
            var queryStorage = _refStorageRepository.GetAll().Result.FirstOrDefault();
            var dummyArticle = new Article(queryStorage ?? new Storage()) { ArticleName = FirstArticleName, Description = FirstArticleDescription };
            await _refArticleRepository.CreateArticle(dummyArticle);

            var queryArticle = await _refArticleRepository.GetAll();
            var updateArticle = queryArticle.First();
            updateArticle.ArticleName = ChangedArticleName;
            updateArticle.Description = ChangedArticleDescription;
            await _refArticleRepository.UpdateArticle(updateArticle);
            queryArticle = await _refArticleRepository.GetAll();

            //Assert
            Assert.Single(queryArticle);
            Assert.Equal(queryArticle.First().ArticleName, ChangedArticleName);
            Assert.Equal(queryArticle.First().Description, ChangedArticleDescription);

        }


        [Theory]
        [InlineData("FirstName", "FirstDescription")]
        public async Task ArticleRepository_Create_Delete_Returns_EmptyList(string FirstArticleName, string FirstArticleDescription)
        {
            //Arrange
            IArticleRepository _refArticleRepository = new ArticleRepository(_dbConfig.DBConnectionString, new SerilogLoggerFactory(Log.Logger).CreateLogger<ArticleRepository>());
            IStorageRepository _refStorageRepository = new StorageRepository(_dbConfig.DBConnectionString, new SerilogLoggerFactory(Log.Logger).CreateLogger<StorageRepository>());
            Storage _dummyStorage = new Storage() { StorageName = "TestStorage" };


            //Act
            await _refStorageRepository.CreateStorage(_dummyStorage);
            var queryStorage = _refStorageRepository.GetAll().Result.FirstOrDefault();
            var dummyArticle = new Article(queryStorage ?? new Storage()) { ArticleName = FirstArticleName, Description = FirstArticleDescription };
            await _refArticleRepository.CreateArticle(dummyArticle);

            var queryArticle = await _refArticleRepository.GetAll();
            var firstArticle = queryArticle.First();

            await _refArticleRepository.DeleteArticle(firstArticle.ID);
            queryArticle = await _refArticleRepository.GetAll();

            //Assert
            Assert.Empty(queryArticle);

        }

        [Theory]
        [InlineData("TestArticle", "Test", "TestStorage")]
        public async Task ArticleRepository_Create_ReturnsbyID_SingleArticle(string ArticleName, string ArticleDescription, string StorageName)
        {
            //Arrange
            IArticleRepository _refArticleRepository = new ArticleRepository(_dbConfig.DBConnectionString, new SerilogLoggerFactory(Log.Logger).CreateLogger<ArticleRepository>());
            IStorageRepository _refStorageRepository = new StorageRepository(_dbConfig.DBConnectionString, new SerilogLoggerFactory(Log.Logger).CreateLogger<StorageRepository>());
            Storage _dummyStorage = new Storage() { StorageName = StorageName };


            //Act
            await _refStorageRepository.CreateStorage(_dummyStorage);
            var queryStorage = _refStorageRepository.GetAll().Result.FirstOrDefault();
            var dummyArticle = new Article(queryStorage ?? new Storage()) { ArticleName = ArticleName, Description = ArticleDescription };
            await _refArticleRepository.CreateArticle(dummyArticle);

            var queryArticles = await _refArticleRepository.GetAll();

            var querybyID = await _refArticleRepository.GetArticlebyID(queryArticles.First().ID);

            //Assert 
            Assert.NotNull(querybyID);
        }


    }
}