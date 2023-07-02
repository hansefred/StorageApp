using Domain.DomainModels;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Infrastructure.Tests.Helper;
using Serilog.Extensions.Logging;
using Serilog;
using Xunit.Abstractions;
using Xunit.Sdk;
using DotNet.Testcontainers.Containers;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Tests
{

    [Collection("Non-Parallel Collection")]
    public class InfrastructureStorageRepositoryTests : DBConfig, IAsyncLifetime
    {
        private readonly TestOutputHelper _logOutput;
        private IContainer? _dockerContainer;
        private DBConfig _dbConfig = new DBConfig();

        public InfrastructureStorageRepositoryTests(ITestOutputHelper logOutput)
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

        public async Task InitializeAsync()
        {
            _dockerContainer = await DockerHelper.CreateDockerDatabase(_dbConfig);
            DatabaseHelper.WaitforSQLDB(_dbConfig.DBConnectionString, Log.Logger);
            DatabaseHelper.DeployDatabase(_dbConfig);

        }



        [Theory]
        [InlineData("TestStorage")]
        public async Task ArticleRepository_Create_Returns_SingleList(string StorageName)
        {
            //Arrange
            IStorageRepository _refStorageRepository = new StorageRepository(_dbConfig.DBConnectionString, new SerilogLoggerFactory(Log.Logger).CreateLogger<StorageRepository>());
            Storage _dummyStorage = new Storage() { StorageName = StorageName };


            //Act
            await _refStorageRepository.CreateStorage(_dummyStorage);
            var queryStorages = await _refStorageRepository.GetAll();

            //Assert 
            Assert.Single(queryStorages);

        }


        [Fact]
        public async Task ArticleRepository_GetAll_Returns_EmptyList()
        {
            //Arrange
            IStorageRepository _refStorageRepository = new StorageRepository(_dbConfig.DBConnectionString, new SerilogLoggerFactory(Log.Logger).CreateLogger<StorageRepository>());

            //Act
            var queryStorages = await _refStorageRepository.GetAll();

            //Assert 
            Assert.Empty(queryStorages);

        }


        [Theory]
        [InlineData("FirstName","ChangedName")]
        public async Task ArticleRepository_Create_Update_Returns_ChangedArticle(string FirstStorageName, string ChangedStorageName)
        {
            //Arrange
            IStorageRepository _refStorageRepository = new StorageRepository(_dbConfig.DBConnectionString, new SerilogLoggerFactory(Log.Logger).CreateLogger<StorageRepository>());
            Storage _dummyStorage = new Storage() { StorageName = FirstStorageName };


            //Act
            await _refStorageRepository.CreateStorage(_dummyStorage);
            var queryStorage = await _refStorageRepository.GetAll();
            queryStorage.First().StorageName = ChangedStorageName;

            await _refStorageRepository.UpdateStorage(queryStorage.First());
            queryStorage = await _refStorageRepository.GetAll();


            //Assert
            Assert.Single(queryStorage);
            Assert.Equal(queryStorage.First().StorageName, ChangedStorageName);


        }


        [Theory]
        [InlineData("TestStorage")]
        public async Task ArticleRepository_Create_Delete_Returns_EmptyList(string FirstStorageName)
        {
            //Arrange
            IStorageRepository _refStorageRepository = new StorageRepository(_dbConfig.DBConnectionString, new SerilogLoggerFactory(Log.Logger).CreateLogger<StorageRepository>());
            Storage _dummyStorage = new Storage() { StorageName = FirstStorageName };


            //Act
            await _refStorageRepository.CreateStorage(_dummyStorage);
            var queryStorage = await _refStorageRepository.GetAll();
            await _refStorageRepository.DeleteStorage(queryStorage.First().ID);
            queryStorage = await _refStorageRepository.GetAll();


            //Assert
            Assert.Empty(queryStorage);

        }

        [Theory]
        [InlineData("TestStorage")]
        public async Task StorageRepository_Create_ReturnsbyID_SingleArticle(string StorageName)
        {
            //Arrange
            IStorageRepository _refStorageRepository = new StorageRepository(_dbConfig.DBConnectionString, new SerilogLoggerFactory(Log.Logger).CreateLogger<StorageRepository>());
            Storage _dummyStorage = new Storage() { StorageName = StorageName };


            //Act
            await _refStorageRepository.CreateStorage(_dummyStorage);
            var queryStorage = await _refStorageRepository.GetAll();
          
            var querybyID = await _refStorageRepository.GetStoragebyID(queryStorage.First().ID);
            
            //Assert 
            Assert.NotNull(querybyID);

        }



    }
}
