namespace Domain.DomainModels.Commands
{
    public class CreateNewStorageCommand : StorageCommand
    {
        public CreateNewStorageCommand(string storageName)
        {
            StorageName = storageName;
        }
    }
}
