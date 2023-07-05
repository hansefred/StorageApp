using Domain.DomainModels;
using Domain.DomainModels.Interfaces;

namespace Infrastructure.Factories
{
    internal class StorageFactory : IStorageFactory
    {
        public Storage CreateInstance(string storageName)
        {
            return new Storage () { StorageName = storageName };
        }
    }
}
