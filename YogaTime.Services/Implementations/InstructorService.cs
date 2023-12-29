using AutoMapper;
using Serilog;
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
    public class InstructorService : IInstructorService, IServiceAnchor
    {
        private readonly IInstructorReadRepository employeeReadRepository;
        private readonly IInstructorWriteRepository employeeWriteRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IPersonReadRepository personReadRepository;
        private readonly IMapper mapper;

        public InstructorService(IInstructorReadRepository employeeReadRepository,
            IInstructorWriteRepository employeeWriteRepository,
            IUnitOfWork unitOfWork,
            IPersonReadRepository personReadRepository,
            IMapper mapper)
        {
            this.employeeReadRepository = employeeReadRepository;
            this.employeeWriteRepository = employeeWriteRepository;
            this.unitOfWork = unitOfWork;
            this.personReadRepository = personReadRepository;
            this.mapper = mapper;
        }

        async Task<IEnumerable<InstructorModel>> IInstructorService.GetAllAsync(CancellationToken cancellationToken)
        {
            var employees = await employeeReadRepository.GetAllAsync(cancellationToken);
            var persons = await personReadRepository.GetByIdsAsync(employees.Select(x => x.PersonId).Distinct(), cancellationToken);
            var result = new List<InstructorModel>();
            foreach (var employee in employees)
            {
                if (!persons.TryGetValue(employee.PersonId, out var person))
                {
                    Log.Warning("Запрос вернул null(Person) IInstructorService.GetAllAsync");
                    continue;
                }
                var empl = mapper.Map<InstructorModel>(employee);
                empl.Person = mapper.Map<PersonModel>(person);
                result.Add(empl);
            }

            return result;
        }

        async Task<InstructorModel?> IInstructorService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await employeeReadRepository.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                Log.Warning("Запрос вернул null IInstructorService.GetByIdAsync");
                return null;
            }
            var person = await personReadRepository.GetByIdAsync(item.PersonId, cancellationToken);
            var employee = mapper.Map<InstructorModel>(item);
            employee.Person = person != null
                ? mapper.Map<PersonModel>(person)
                : null;
            return employee;
        }

        async Task<InstructorModel> IInstructorService.AddAsync(InstructorRequestModel employeeRequestModel, CancellationToken cancellationToken)
        {
            var item = new Instructor
            {
                Id = Guid.NewGuid(),
                Desc = employeeRequestModel.Desc,
            };

            var personValidate = new PersonHelpValidate(personReadRepository);
            var person = await personValidate.GetPersonByIdAsync(employeeRequestModel.PersonId, cancellationToken);
            if (person != null)
            {
                item.PersonId = person.Id;
                item.Person = person;
            }

            employeeWriteRepository.Add(item);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<InstructorModel>(item);
        }
        async Task<InstructorModel> IInstructorService.EditAsync(InstructorRequestModel source, CancellationToken cancellationToken)
        {
            var targetInstructor = await employeeReadRepository.GetByIdAsync(source.Id, cancellationToken);
            if (targetInstructor == null)
            {
                throw new TimeTableEntityNotFoundException<Instructor>(source.Id);
            }

            targetInstructor.Desc = source.Desc;
            var personValidate = new PersonHelpValidate(personReadRepository);
            var person = await personValidate.GetPersonByIdAsync(source.PersonId, cancellationToken);
            if (person != null)
            {
                targetInstructor.PersonId = person.Id;
                targetInstructor.Person = person;
            }

            employeeWriteRepository.Update(targetInstructor);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<InstructorModel>(targetInstructor);
        }
        async Task IInstructorService.DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var targetInstructor = await employeeReadRepository.GetByIdAsync(id, cancellationToken);
            if (targetInstructor == null)
            {
                throw new TimeTableEntityNotFoundException<Instructor>(id);
            }
            if (targetInstructor.DeletedAt.HasValue)
            {
                throw new TimeTableInvalidOperationException($"Рабочий с идентификатором {id} уже удален");
            }

            employeeWriteRepository.Delete(targetInstructor);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
