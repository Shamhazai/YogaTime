using Microsoft.EntityFrameworkCore;
using Serilog;
using YogaTime.Common.Entity.InterfaceDB;
using YogaTime.Common.Entity.Repositories;
using YogaTime.Context.Contracts.Models;
using YogaTime.Repositories.Contracts;

namespace YogaTime.Repositories.Implementations
{
    public class InstructorReadRepository : IInstructorReadRepository, IRepositoryAnchor
    {

        private readonly IDbRead reader;

        public InstructorReadRepository(IDbRead reader)
        {
            this.reader = reader;
            Log.Information("Инициализирован абстракция IDbReader в классе InstructorReadRepository");
        }

        Task<IReadOnlyCollection<Instructor>> IInstructorReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => reader.Read<Instructor>()
                .NotDeletedAt()
                .OrderBy(x => x.Desc)
                .ThenBy(x => x.Person!.LastName)
                .ThenBy(x => x.Person!.FirstName)
                .ThenBy(x => x.Person!.Patronymic)
                .ToReadOnlyCollectionAsync(cancellationToken);

        Task<Instructor?> IInstructorReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<Instructor>()
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);

        Task<Dictionary<Guid, Instructor>> IInstructorReadRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellation)
            => reader.Read<Instructor>()
                .NotDeletedAt()
                .ByIds(ids)
                .ToDictionaryAsync(key => key.Id, cancellation);

        public Task<Dictionary<Guid, Person?>> GetPersonByInstructorIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellation)
            => reader.Read<Instructor>()
                .NotDeletedAt()
                .ByIds(ids)
                .Select(x => new
                {
                    x.Id,
                    x.Person,
                })
                .ToDictionaryAsync(key => key.Id, val => val.Person, cancellation);
    }
}
