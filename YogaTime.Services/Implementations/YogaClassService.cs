using AutoMapper;
using YogaTime.Common.Entity.InterfaceDB;
using YogaTime.Context.Contracts.Models;
using YogaTime.Repositories.Contracts;
using YogaTime.Services.Contracts.Exceptions;
using YogaTime.Services.Contracts.Interface;
using YogaTime.Services.Contracts.Models;


namespace YogaTime.Services.Implementations
{
    /// <inheritdoc cref="IYogaClassService"/>
    public class YogaClassService : IYogaClassService, IServiceAnchor
    {
        private readonly IYogaClassReadRepository yogaClassReadRepository;
        private readonly IYogaClassWriteRepository yogaClassWriteRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public YogaClassService(IYogaClassReadRepository YogaClassReadRepository,
            IYogaClassWriteRepository YogaClassWriteRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            this.yogaClassReadRepository = YogaClassReadRepository;
            this.yogaClassWriteRepository = YogaClassWriteRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        async Task<IEnumerable<YogaClassModel>> IYogaClassService.GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await yogaClassReadRepository.GetAllAsync(cancellationToken);
            return mapper.Map<IEnumerable<YogaClassModel>>(result);
        }

        async Task<YogaClassModel> IYogaClassService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await yogaClassReadRepository.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                throw new TimeTableEntityNotFoundException<YogaClass>(id);
            }
            return mapper.Map<YogaClassModel>(item);
        }

        async Task<YogaClassModel> IYogaClassService.AddAsync(string name, string description, CancellationToken cancellationToken)
        {
            var item = new YogaClass
            {
                Id = Guid.NewGuid(),
                Name = name,
                Description = description,
            };
            yogaClassWriteRepository.Add(item);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<YogaClassModel>(item);
        }

        async Task<YogaClassModel> IYogaClassService.EditAsync(YogaClassModel source, CancellationToken cancellationToken)
        {
            var targetYogaClass = await yogaClassReadRepository.GetByIdAsync(source.Id, cancellationToken);
            if (targetYogaClass == null)
            {
                throw new TimeTableEntityNotFoundException<YogaClass>(source.Id);
            }

            targetYogaClass.Name = source.Name;
            targetYogaClass.Description = source.Description;
            yogaClassWriteRepository.Update(targetYogaClass);

            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<YogaClassModel>(targetYogaClass);
        }

        async Task IYogaClassService.DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var targetYogaClass = await yogaClassReadRepository.GetByIdAsync(id, cancellationToken);
            if (targetYogaClass == null)
            {
                throw new TimeTableEntityNotFoundException<YogaClass>(id);
            }

            if (targetYogaClass.DeletedAt.HasValue)
            {
                throw new TimeTableInvalidOperationException($"Занятие {id} удалено ранее");
            }

            yogaClassWriteRepository.Delete(targetYogaClass);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
