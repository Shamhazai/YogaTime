using Microsoft.EntityFrameworkCore;
using Serilog;
using YogaTime.Common.Entity.InterfaceDB;
using YogaTime.Common.Entity.Repositories;
using YogaTime.Context.Contracts.Models;
using YogaTime.Repositories.Contracts;

namespace YogaTime.Repositories.Implementations
{
    public class RoomReadRepository : IRoomReadRepository, IRepositoryAnchor
    {
        private readonly IDbRead reader;

        public RoomReadRepository(IDbRead reader)
        {
            this.reader = reader;
            Log.Information("Инициализирован абстракция IDbReader в классе RoomReadRepository");
        }

        Task<IReadOnlyCollection<Room>> IRoomReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => reader.Read<Room>()
                .NotDeletedAt()
                .OrderBy(x => x.Name)
                .ToReadOnlyCollectionAsync(cancellationToken);

        Task<Room?> IRoomReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<Room>()
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);

        Task<Dictionary<Guid, Room>> IRoomReadRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellation)
            => reader.Read<Room>()
                .ByIds(ids)
                .OrderBy(x => x.Name)
                .ToDictionaryAsync(key => key.Id, cancellation);
    }
}
