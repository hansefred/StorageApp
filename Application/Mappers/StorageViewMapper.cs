using Application.ViewModel;
using Domain.DomainModels;
using Domain.DomainModels.Commands;

namespace Application.Mappers
{
    internal class StorageViewMapper
    {
        public IEnumerable<StorageViewModel> ConstructFromList (IEnumerable<Storage> storages)
        {
            return storages.Select(ConstructFormEntity);
        }

        public StorageViewModel ConstructFormEntity (Storage storage) 
        {
            return new StorageViewModel(storage.ID, storage.StorageName, storage.Articles);
        }

        public CreateNewStorageCommand ConvertToNewStorageCommand (StorageViewModel storageViewModel)
        {
            return new CreateNewStorageCommand(storageViewModel.StorageName);
        }

        public DeleteStorageCommand ConvertToDeleteStorageCommand (string ID) 
        {
            return new DeleteStorageCommand (Guid.Parse (ID));
        }

        public UpdateStorageNameCommand ConvertToUpdateStorageNameCommand (StorageViewModel storageViewModel)
        {
            return new UpdateStorageNameCommand(Guid.Parse (storageViewModel.ID),storageViewModel.StorageName);
        }

      
    }
}
