namespace Domain.DomainModels.Interfaces
{
    public interface IStorageFactory
    {
        public Storage CreateInstance(string storageName);
    }
}
