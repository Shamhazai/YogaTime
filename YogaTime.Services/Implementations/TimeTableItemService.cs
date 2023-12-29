using System.Xml;
using AutoMapper;
using Serilog;
using YogaTime.Common;
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
    public class TimeTableItemService : ITimeTableItemService, IServiceAnchor
    {
        private readonly ITimeTableItemReadRepository timeTableItemReadRepository;
        private readonly ITimeTableItemWriteRepository timeTableItemWriteRepository;
        private readonly IYogaClassReadRepository yogaClassReadRepository;
        private readonly IRoomReadRepository roomReadRepository;
        private readonly IGroupReadRepository groupReadRepository;
        private readonly IInstructorReadRepository employeeReadRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public TimeTableItemService(ITimeTableItemReadRepository timeTableItemReadRepository,
            ITimeTableItemWriteRepository timeTableItemWriteRepository,
            IYogaClassReadRepository YogaClassReadRepository,
            IRoomReadRepository roomReadRepository,
            IGroupReadRepository groupReadRepository,
            IInstructorReadRepository employeeReadRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            this.timeTableItemReadRepository = timeTableItemReadRepository;
            this.timeTableItemWriteRepository = timeTableItemWriteRepository;
            this.yogaClassReadRepository = YogaClassReadRepository;
            this.roomReadRepository = roomReadRepository;
            this.groupReadRepository = groupReadRepository;
            this.employeeReadRepository = employeeReadRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        async Task<IEnumerable<TimeTableItemModel>> ITimeTableItemService.GetAllAsync(DateTimeOffset targetDate, CancellationToken cancellationToken)
        {
            var timeTableItems = await timeTableItemReadRepository.GetAllByDateAsync(targetDate.Date, targetDate.Date.AddDays(1).AddMilliseconds(-1), cancellationToken);

            var YogaClassIds = timeTableItems.Select(x => x.YogaClassId).Distinct();
            var RoomIds = timeTableItems.Select(x => x.RoomId).Distinct();

            var groupIds = timeTableItems.Select(x => x.GroupId).Distinct();
            var instructorIds = timeTableItems.Where(x => x.InstructorId.HasValue)
                .Select(x => x.InstructorId!.Value)
                .Distinct();

            var YogaClassDictionary = await yogaClassReadRepository.GetByIdsAsync(YogaClassIds, cancellationToken);
            var roomDictionary = await roomReadRepository.GetByIdsAsync(RoomIds, cancellationToken);

            var groupDictionary = await groupReadRepository.GetByIdsAsync(groupIds, cancellationToken);
            var instructorDictionary = await employeeReadRepository.GetPersonByInstructorIdsAsync(instructorIds, cancellationToken);

            var listTimeTableItemModel = new List<TimeTableItemModel>();
            foreach (var timeTableItem in timeTableItems)
            {
                if (!YogaClassDictionary.TryGetValue(timeTableItem.YogaClassId, out var YogaClass))
                {
                    Log.Warning("Запрос вернул null(YogaClass) ITimeTableItemService.GetAllAsync");
                    continue;
                }
                if (!roomDictionary.TryGetValue(timeTableItem.RoomId, out var Room))
                {
                    Log.Warning("Запрос вернул null(Room) ITimeTableItemService.GetAllAsync");
                    continue;
                }
                if (!groupDictionary.TryGetValue(timeTableItem.GroupId, out var group))
                {
                    Log.Warning("Запрос вернул null(GroupId) ITimeTableItemService.GetAllAsync");
                    continue;
                }
                if (timeTableItem.InstructorId == null ||
                    !instructorDictionary.TryGetValue(timeTableItem.InstructorId.Value, out var instructor))
                {
                    Log.Warning("Запрос вернул null(InstructorId) ITimeTableItemService.GetAllAsync");
                    continue;
                }
                var timeTable = mapper.Map<TimeTableItemModel>(timeTableItem);
                timeTable.YogaClass = mapper.Map<YogaClassModel>(YogaClass);
                timeTable.Room = mapper.Map<RoomModel>(Room);
                timeTable.Group = mapper.Map<GroupModel>(group);
                timeTable.Instructor = mapper.Map<PersonModel>(instructor);

                listTimeTableItemModel.Add(timeTable);
            }

            return listTimeTableItemModel;
        }

        async Task<TimeTableItemModel?> ITimeTableItemService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await timeTableItemReadRepository.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                return null;
            }
            var room = await yogaClassReadRepository.GetByIdAsync(item.RoomId, cancellationToken);

            var YogaClass = await yogaClassReadRepository.GetByIdAsync(item.YogaClassId, cancellationToken);
            var group = await groupReadRepository.GetByIdAsync(item.GroupId, cancellationToken);
            var timeTable = mapper.Map<TimeTableItemModel>(item);
            timeTable.YogaClass = YogaClass != null
                ? mapper.Map<YogaClassModel>(YogaClass)
                : null;
            timeTable.Room = room != null
                ? mapper.Map<RoomModel>(room)
                : null;
            timeTable.Group = group != null
               ? mapper.Map<GroupModel>(group)
               : null;
            if (item.InstructorId != null)
            {
                var personDictionary = await employeeReadRepository.GetPersonByInstructorIdsAsync(new[] { item.InstructorId.Value }, cancellationToken);
                timeTable.Instructor = personDictionary.TryGetValue(item.InstructorId.Value, out var instructor)
                  ? mapper.Map<PersonModel>(instructor)
                  : null;
            }
            return timeTable;
        }

        async Task<TimeTableItemModel> ITimeTableItemService.AddAsync(TimeTableItemRequestModel timeTable, CancellationToken cancellationToken)
        {
            var item = new TimeTableItem
            {
                Id = Guid.NewGuid(),
                StartDate = timeTable.StartDate,
                EndDate = timeTable.EndDate
            };

            var employeeValidate = new PersonHelpValidate(employeeReadRepository);
            var employee = await employeeValidate.GetInstructorByIdInstructorAsync(timeTable.Instructor, cancellationToken);
            if (employee != null)
            {
                item.InstructorId = employee.Id;
                item.Instructor = employee;
            }

            var groupValidate = new PersonHelpValidate(groupReadRepository);
            var group = await groupValidate.GetGroupByIdAsync(timeTable.Group, cancellationToken);
            if (group != null)
            {
                item.GroupId = group.Id;
                item.Group = group;
            }

            var YogaClassValidate = new PersonHelpValidate(yogaClassReadRepository);
            var YogaClass = await YogaClassValidate.GetYogaClassByIdAsync(timeTable.YogaClass, cancellationToken);
            if (YogaClass != null)
            {
                item.YogaClassId = YogaClass.Id;
                item.YogaClass = YogaClass;
            }

            var roomValidate = new PersonHelpValidate(roomReadRepository);
            var room = await roomValidate.GetRoomByIdAsync(timeTable.Room, cancellationToken);
            if (room != null)
            {
                item.RoomId = room.Id;
                item.Room = room;
            }

            timeTableItemWriteRepository.Add(item);
            var timeTableItem = mapper.Map<TimeTableItemModel>(item);
            timeTableItem.YogaClass = YogaClass != null
                ? mapper.Map<YogaClassModel>(YogaClass)
                : null;
            timeTableItem.Room = room != null
                ? mapper.Map<RoomModel>(room)
                : null;
            timeTableItem.Group = group != null
               ? mapper.Map<GroupModel>(group)
               : null;
            if (item.InstructorId != null)
            {
                var personDictionary = await employeeReadRepository.GetPersonByInstructorIdsAsync(new[] { item.InstructorId.Value }, cancellationToken);
                timeTableItem.Instructor = personDictionary.TryGetValue(item.InstructorId.Value, out var instructor)
                  ? mapper.Map<PersonModel>(instructor)
                  : null;
            }
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return timeTableItem;
        }

        async Task<TimeTableItemModel> ITimeTableItemService.EditAsync(TimeTableItemRequestModel source, CancellationToken cancellationToken)
        {
            var targetTimeTableItem = await timeTableItemReadRepository.GetByIdAsync(source.Id, cancellationToken);

            if (targetTimeTableItem == null)
            {
                throw new TimeTableEntityNotFoundException<TimeTableItem>(source.Id);
            }

            targetTimeTableItem.StartDate = source.StartDate;
            targetTimeTableItem.EndDate = source.EndDate;

            var employeeValidate = new PersonHelpValidate(employeeReadRepository);
            var employee = await employeeValidate.GetInstructorByIdInstructorAsync(source.Instructor, cancellationToken);
            if (employee != null)
            {
                targetTimeTableItem.InstructorId = employee.Id;
                targetTimeTableItem.Instructor = employee;
            }

            var groupValidate = new PersonHelpValidate(groupReadRepository);
            var group = await groupValidate.GetGroupByIdAsync(source.Group, cancellationToken);
            if (group != null)
            {
                targetTimeTableItem.GroupId = group.Id;
                targetTimeTableItem.Group = group;
            }

            var YogaClassValidate = new PersonHelpValidate(yogaClassReadRepository);
            var YogaClass = await YogaClassValidate.GetYogaClassByIdAsync(source.YogaClass, cancellationToken);
            if (YogaClass != null)
            {
                targetTimeTableItem.YogaClassId = YogaClass.Id;
                targetTimeTableItem.YogaClass = YogaClass;
            }

            var roomValidate = new PersonHelpValidate(yogaClassReadRepository);
            var room = await roomValidate.GetRoomByIdAsync(source.Room, cancellationToken);
            if (room != null)
            {
                targetTimeTableItem.RoomId = room.Id;
                targetTimeTableItem.Room = room;
            }


            timeTableItemWriteRepository.Update(targetTimeTableItem);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<TimeTableItemModel>(targetTimeTableItem);
        }

        async Task ITimeTableItemService.DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var targetTimeTableItem = await timeTableItemReadRepository.GetByIdAsync(id, cancellationToken);
            if (targetTimeTableItem == null)
            {
                throw new TimeTableEntityNotFoundException<TimeTableItem>(id);
            }
            if (targetTimeTableItem.DeletedAt.HasValue)
            {
                throw new TimeTableInvalidOperationException($"Расписание с идентификатором {id} уже удален");
            }

            timeTableItemWriteRepository.Delete(targetTimeTableItem);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

    }
}
