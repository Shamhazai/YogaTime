using Microsoft.EntityFrameworkCore;
using Serilog;
using YogaTime.Common.Entity.InterfaceDB;
using YogaTime.Common.Entity.Repositories;
using YogaTime.Context.Contracts.Models;
using YogaTime.Repositories.Contracts;

namespace YogaTime.Repositories.Implementations
{
    public class TimeTableItemReadRepository : ITimeTableItemReadRepository, IRepositoryAnchor
    {
        private readonly IDbRead reader;

        public TimeTableItemReadRepository(IDbRead reader)
        {
            this.reader = reader;
            Log.Information("Инициализирован абстракция IDbReader в классе TimeTableItemReadRepository");
        }

        Task<IReadOnlyCollection<TimeTableItem>> ITimeTableItemReadRepository.GetAllByDateAsync(DateTimeOffset startDate,
            DateTimeOffset endDate,
            CancellationToken cancellationToken)
            => reader.Read<TimeTableItem>()
                .NotDeletedAt()
                .Where(x => x.StartDate >= startDate &&
                            x.EndDate <= endDate)
                .OrderBy(x => x.StartDate)
                .ToReadOnlyCollectionAsync(cancellationToken);

        Task<TimeTableItem?> ITimeTableItemReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<TimeTableItem>()
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);
    }
}
