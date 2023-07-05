namespace Domain.DomainModels.Commands
{
    public class UpdateStorageNameCommand : StorageCommand
    {
        public UpdateStorageNameCommand(Guid id, string storageName)
        {
            StorageName = storageName;
            ID = id;
        }
    }
}
