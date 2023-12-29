using Microsoft.EntityFrameworkCore;
using Serilog;
using YogaTime.Common.Entity.InterfaceDB;
using YogaTime.Common.Entity.Repositories;
using YogaTime.Context.Contracts.Models;
using YogaTime.Repositories.Contracts;

namespace YogaTime.Repositories.Implementations
{
    public class YogaClassReadRepository : IYogaClassReadRepository, IRepositoryAnchor
    {
        private readonly IDbRead reader;

        public YogaClassReadRepository(IDbRead reader)
        {
            this.reader = reader;
            Log.Information("Инициализирован абстракция IDbReader в классе YogaClassReadRepository");
        }

        Task<IReadOnlyCollection<YogaClass>> IYogaClassReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => reader.Read<YogaClass>()
                .NotDeletedAt()
                .OrderBy(x => x.Name)
                .ToReadOnlyCollectionAsync(cancellationToken);

        Task<YogaClass?> IYogaClassReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<YogaClass>()
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);

        Task<Dictionary<Guid, YogaClass>> IYogaClassReadRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellation)
            => reader.Read<YogaClass>()
                .ByIds(ids)
                .OrderBy(x => x.Name)
                .ToDictionaryAsync(key => key.Id, cancellation);
    }
}
