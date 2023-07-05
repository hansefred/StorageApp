namespace Domain.DomainModels.Interfaces
{
    public interface IStorageRepository
    {
        Task<Storage> CreateStorage(Storage storage);
        Task DeleteStorage(Guid ID);
        Task<IEnumerable<Storage>> GetAll();
        Task<Storage?> GetStoragebyID(Guid ID);
        Task UpdateStorage(Storage storage);
    }
}