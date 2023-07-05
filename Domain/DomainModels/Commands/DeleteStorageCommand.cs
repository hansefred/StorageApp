namespace Domain.DomainModels.Commands
{
    public class DeleteStorageCommand : StorageCommand
    {
        public DeleteStorageCommand(Guid id)
        {
            ID = id;
        }
    }
}
