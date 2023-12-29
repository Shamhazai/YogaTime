using YogaTime.Context.Contracts.Models;
using YogaTime.Repositories.Contracts;
using YogaTime.Services.Contracts.Exceptions;

namespace YogaTime.Services.Helps
{
    public class PersonHelpValidate
    {
        private readonly IPersonReadRepository personReadRepository;
        private readonly IInstructorReadRepository employeeReadRepository;
        private readonly IGroupReadRepository groupReadRepository;

        private readonly IRoomReadRepository roomReadRepository;
        private readonly IYogaClassReadRepository yogaClassReadRepository;

        public PersonHelpValidate(IPersonReadRepository personReadRepository)
        {
            this.personReadRepository = personReadRepository;
        }

        public PersonHelpValidate(IInstructorReadRepository employeeReadRepository)
        {
            this.employeeReadRepository = employeeReadRepository;
        }

        public PersonHelpValidate(IGroupReadRepository groupReadRepository)
        {
            this.groupReadRepository = groupReadRepository;
        }

        public PersonHelpValidate(IYogaClassReadRepository YogaClassReadRepository)
        {
            this.yogaClassReadRepository = YogaClassReadRepository;
        }

        public PersonHelpValidate(IRoomReadRepository roomReadRepository)
        {
            this.roomReadRepository = roomReadRepository;
        }

        async public Task<Person?> GetPersonByIdAsync(Guid id_person, CancellationToken cancellationToken)
        {
            if (id_person != Guid.Empty)
            {
                var targetPerson = await personReadRepository.GetByIdAsync(id_person, cancellationToken);
                if (targetPerson == null)
                {
                    throw new TimeTableEntityNotFoundException<Person>(id_person);
                }
                return targetPerson;
            }
            return null;
        }


        async public Task<Instructor?> GetInstructorByIdInstructorAsync(Guid id_instructor, CancellationToken cancellationToken)
        {
            if (id_instructor != Guid.Empty)
            {
                var instructor = await employeeReadRepository.GetByIdAsync(id_instructor, cancellationToken);
                if (instructor == null)
                {
                    throw new TimeTableInvalidOperationException("Нет такого инструктора!");
                }
                return instructor;
            }
            return null;
        }

        async public Task<Group?> GetGroupByIdAsync(Guid id_group, CancellationToken cancellationToken)
        {
            if (id_group != Guid.Empty)
            {
                var group = await groupReadRepository.GetByIdAsync(id_group, cancellationToken);
                if (group == null)
                {
                    throw new TimeTableInvalidOperationException("Такой группы нет!");
                }
                return group;
            }
            return null;
        }

        async public Task<YogaClass?> GetYogaClassByIdAsync(Guid id_YogaClass, CancellationToken cancellationToken)
        {
            if (id_YogaClass != Guid.Empty)
            {
                var YogaClass = await yogaClassReadRepository.GetByIdAsync(id_YogaClass, cancellationToken);
                if (YogaClass == null)
                {
                    throw new TimeTableInvalidOperationException("Нет у нас такого занятия!");
                }
                return YogaClass;
            }
            return null;
        }

        async public Task<Room?> GetRoomByIdAsync(Guid id_Room, CancellationToken cancellationToken)
        {
            if (id_Room != Guid.Empty)
            {
                var room = await roomReadRepository.GetByIdAsync(id_Room, cancellationToken);
                if (room == null)
                {
                    throw new TimeTableInvalidOperationException("Нет у нас такой комнаты!");
                }
                return room;
            }
            return null;
        }
    }
}
