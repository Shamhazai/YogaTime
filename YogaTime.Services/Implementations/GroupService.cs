using AutoMapper;
using YogaTime.Common.Entity.InterfaceDB;
using YogaTime.Context.Contracts.Models;
using YogaTime.Repositories.Contracts;
using YogaTime.Services.Contracts.Exceptions;
using YogaTime.Services.Contracts.Interface;
using YogaTime.Services.Contracts.Models;
using YogaTime.Services.Contracts.ModelsRequest;
using YogaTime.Services.Helps;

namespace YogaTime.Services.Implementations
{
    public class GroupService : IGroupService, IServiceAnchor
    {
        private readonly IGroupReadRepository groupReadRepository;
        private readonly IGroupWriteRepository groupWriteRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IPersonReadRepository personReadRepository;
        private readonly IInstructorReadRepository employeeReadRepository;
        private readonly IMapper mapper;

        public GroupService(IGroupReadRepository groupReadRepository,
            IGroupWriteRepository groupWriteRepository,
            IUnitOfWork unitOfWork,
            IPersonReadRepository personReadRepository,
            IInstructorReadRepository employeeReadRepository,
            IMapper mapper)
        {
            this.groupReadRepository = groupReadRepository;
            this.groupWriteRepository = groupWriteRepository;
            this.unitOfWork = unitOfWork;
            this.personReadRepository = personReadRepository;
            this.employeeReadRepository = employeeReadRepository;
            this.mapper = mapper;
        }

        async Task<IEnumerable<GroupModel>> IGroupService.GetAllAsync(CancellationToken cancellationToken)
        {
            var groups = await groupReadRepository.GetAllAsync(cancellationToken);
            var employeeIds = groups.Where(x => x.InstructorId.HasValue)
                .Select(x => x.InstructorId!.Value)
                .Distinct();
            var instructorDictionary = await employeeReadRepository.GetPersonByInstructorIdsAsync(employeeIds, cancellationToken);

            var listGroupModel = new List<GroupModel>();
            foreach (var group in groups)
            {
                var groupModel = mapper.Map<GroupModel>(group);
                groupModel.ClassroomInstructor = group.InstructorId.HasValue &&
                                              instructorDictionary.TryGetValue(group.InstructorId!.Value, out var instructor)
                    ? mapper.Map<PersonModel>(instructor)
                    : null;

                var clients = await personReadRepository.GetAllByGroupIdAsync(group.Id, cancellationToken);
                groupModel.Clients = mapper.Map<ICollection<PersonModel>>(clients);

                listGroupModel.Add(groupModel);
            }
            return listGroupModel;
        }

        async Task<GroupModel?> IGroupService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await groupReadRepository.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                return null;
            }

            var groupModel = mapper.Map<GroupModel>(item);
            if (item.InstructorId.HasValue)
            {
                var instructorDictionary = await employeeReadRepository.GetPersonByInstructorIdsAsync(new[] { item.InstructorId.Value }, cancellationToken);
                groupModel.ClassroomInstructor = instructorDictionary.TryGetValue(item.InstructorId!.Value, out var instructor)
                    ? mapper.Map<PersonModel>(instructor)
                    : null;
            }

            var clients = await personReadRepository.GetAllByGroupIdAsync(item.Id, cancellationToken);
            groupModel.Clients = mapper.Map<ICollection<PersonModel>>(clients);

            return groupModel;
        }

        async Task<GroupModel> IGroupService.AddAsync(GroupRequestModel groupRequestModel, CancellationToken cancellationToken)
        {
            var item = new Group
            {
                Id = Guid.NewGuid(),
                Name = groupRequestModel.Name,
                Description = groupRequestModel.Description,
            };

            var employeeValidate = new PersonHelpValidate(employeeReadRepository);
            if (groupRequestModel.ClassroomInstructor != null)
            {
                var employee = await employeeValidate.GetInstructorByIdInstructorAsync(groupRequestModel.ClassroomInstructor!.Value, cancellationToken);
                if (employee != null)
                {
                    item.InstructorId = employee.Id;
                    item.Instructor = employee;
                }
            }

            groupWriteRepository.Add(item);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<GroupModel>(item);
        }
        async Task<GroupModel> IGroupService.EditAsync(GroupRequestModel source, CancellationToken cancellationToken)
        {
            var targetGroup = await groupReadRepository.GetByIdAsync(source.Id, cancellationToken);
            if (targetGroup == null)
            {
                throw new TimeTableEntityNotFoundException<Group>(source.Id);
            }
            var employeeValidate = new PersonHelpValidate(employeeReadRepository);
            if (source.ClassroomInstructor != null)
            {
                var employee = await employeeValidate.GetInstructorByIdInstructorAsync(source.ClassroomInstructor!.Value, cancellationToken);
                if (employee != null)
                {
                    targetGroup.InstructorId = employee.Id;
                    targetGroup.Instructor = employee;
                }
            }

            targetGroup.Name = source.Name;
            targetGroup.Description = source.Description;

            groupWriteRepository.Update(targetGroup);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<GroupModel>(targetGroup);
        }

        async Task IGroupService.DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var targetGroup = await groupReadRepository.GetByIdAsync(id, cancellationToken);
            if (targetGroup == null)
            {
                throw new TimeTableEntityNotFoundException<Group>(id);
            }

            if (targetGroup.DeletedAt.HasValue)
            {
                throw new TimeTableInvalidOperationException($"Группу с идентификатором {id} уже удалена");
            }

            groupWriteRepository.Delete(targetGroup);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
