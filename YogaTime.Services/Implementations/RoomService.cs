using AutoMapper;
using YogaTime.Common.Entity.InterfaceDB;
using YogaTime.Context.Contracts.Models;
using YogaTime.Repositories.Contracts;
using YogaTime.Services.Contracts.Exceptions;
using YogaTime.Services.Contracts.Interface;
using YogaTime.Services.Contracts.Models;


namespace YogaTime.Services.Implementations
{
    /// <inheritdoc cref="IRoomService"/>
    public class RoomService : IRoomService, IServiceAnchor
    {
        private readonly IRoomReadRepository roomReadRepository;
        private readonly IRoomWriteRepository roomWriteRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public RoomService(IRoomReadRepository RoomReadRepository,
            IRoomWriteRepository RoomWriteRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            this.roomReadRepository = RoomReadRepository;
            this.roomWriteRepository = RoomWriteRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        async Task<IEnumerable<RoomModel>> IRoomService.GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await roomReadRepository.GetAllAsync(cancellationToken);
            return mapper.Map<IEnumerable<RoomModel>>(result);
        }

        async Task<RoomModel> IRoomService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await roomReadRepository.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                throw new TimeTableEntityNotFoundException<Room>(id);
            }
            return mapper.Map<RoomModel>(item);
        }

        async Task<RoomModel> IRoomService.AddAsync(string name, string description, CancellationToken cancellationToken)
        {
            var item = new Room
            {
                Id = Guid.NewGuid(),
                Name = name,
                Description = description,
            };
            roomWriteRepository.Add(item);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<RoomModel>(item);
        }

        async Task<RoomModel> IRoomService.EditAsync(RoomModel source, CancellationToken cancellationToken)
        {
            var targetRoom = await roomReadRepository.GetByIdAsync(source.Id, cancellationToken);
            if (targetRoom == null)
            {
                throw new TimeTableEntityNotFoundException<Room>(source.Id);
            }

            targetRoom.Name = source.Name;
            targetRoom.Description = source.Description;
            roomWriteRepository.Update(targetRoom);

            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<RoomModel>(targetRoom);
        }

        async Task IRoomService.DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var targetRoom = await roomReadRepository.GetByIdAsync(id, cancellationToken);
            if (targetRoom == null)
            {
                throw new TimeTableEntityNotFoundException<Room>(id);
            }

            if (targetRoom.DeletedAt.HasValue)
            {
                throw new TimeTableInvalidOperationException($"Занятие {id} удалено ранее");
            }

            roomWriteRepository.Delete(targetRoom);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
