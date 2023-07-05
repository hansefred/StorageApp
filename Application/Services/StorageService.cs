using Application.Mappers;
using Application.ViewModel;
using Domain.DomainModels;
using Domain.DomainModels.Interfaces;
using FluentMediator;
using OpenTracing;

namespace Application.Services
{
    internal class StorageService : IStorageService
    {
        private readonly IStorageRepository _storageRepository;
        private readonly IStorageFactory _storageFactory;
        private readonly StorageViewMapper _storageViewMapper;
        private readonly ITracer _tracer;
        private readonly IMediator _mediator;

        public StorageService(IStorageRepository storageRepository, IStorageFactory storageFactory, StorageViewMapper storageViewMapper, ITracer tracer, IMediator mediator)
        {
            _storageRepository = storageRepository;
            _storageFactory = storageFactory;
            _storageViewMapper = storageViewMapper;
            _tracer = tracer;
            _mediator = mediator;
        }

        public async Task<StorageViewModel> Create(StorageViewModel storageViewModel)
        {
            using (var scope = _tracer.BuildSpan("Create_StorageService").StartActive(true))
            {
                var newCommand = _storageViewMapper.ConvertToNewStorageCommand(storageViewModel);

                var result = await _mediator.SendAsync<Storage>(newCommand);

                return _storageViewMapper.ConstructFormEntity(result);
            }
        }


        public async Task Delete(string id)
        {
            using (var scope = _tracer.BuildSpan("Delete_StorageService").StartActive(true))
            {
                var deleteCommand = _storageViewMapper.ConvertToDeleteStorageCommand(id);

                await _mediator.PublishAsync(deleteCommand);
            }
        }

        public async Task<StorageViewModel> UpdateName(StorageViewModel storageViewModel)
        {
            using (var scope = _tracer.BuildSpan("UpdateName_StorageService").StartActive(true))
            {
                var deleteCommand = _storageViewMapper.ConvertToUpdateStorageNameCommand(storageViewModel);
                var result = await _mediator.SendAsync<Storage>(deleteCommand);
                return _storageViewMapper.ConstructFormEntity(result);
            }
        }

        public async Task<IEnumerable<StorageViewModel>> GetAll()
        {
            using (var scope = _tracer.BuildSpan("GetAll_StroageService").StartActive(true))
            {
                var result = await _storageRepository.GetAll();

                return _storageViewMapper.ConstructFromList(result);
            }
        }


        public async Task<StorageViewModel?> GetbyId(string ID)
        {
            using (var scope = _tracer.BuildSpan("GetbyId_StroageService").StartActive(true))
            {
                var result = await _storageRepository.GetStoragebyID(Guid.Parse(ID));
                if (result is null)
                {
                    return null;
                }
                return _storageViewMapper.ConstructFormEntity(result);
            }
        }
    }

}
