using Application.ViewModel;

namespace Application.Services
{
    internal interface IStorageService
    {
        Task<StorageViewModel> Create(StorageViewModel storageViewModel);
        Task Delete(string id);
        Task<IEnumerable<StorageViewModel>> GetAll();
        Task<StorageViewModel?> GetbyId(string ID);
        Task<StorageViewModel> UpdateName(StorageViewModel storageViewModel);
    }
}