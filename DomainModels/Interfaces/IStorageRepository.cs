using Domain.DomainModels;

namespace Infrastructure.Interfaces
{
    public interface IStorageRepository
    {
        Task CreateStorage(Storage storage);
        Task DeleteStorage(Guid ID);
        Task<IEnumerable<Storage>> GetAll();
        Task<Storage?> GetStoragebyID(Guid ID);
        Task UpdateStorage(Storage storage);
    }
}