using Domain.DomainModels;
using Domain.DomainModels.Commands;
using Domain.DomainModels.Interfaces;
using FluentMediator;

namespace Application.Handler
{
    internal class StorageCommandHandler
    {
        private readonly IStorageFactory _storageFactory;
        private readonly IStorageRepository _storageRepository;
        private readonly IMediator _mediator;

        public StorageCommandHandler(IStorageFactory storageFactory, IStorageRepository storageRepository, IMediator mediator)
        {
            _storageFactory = storageFactory;
            _storageRepository = storageRepository;
            _mediator = mediator;
        }

    //    public async Task<Storage> HandleNewStorage(CreateNewStorageCommand createNewStorageCommand)
    //    {
    //        var storage = _storageFactory.CreateInstance(createNewStorageCommand.StorageName);

    //        var taskCreated = await _storageRepository.CreateStorage(storage);
    //}
    }


}
