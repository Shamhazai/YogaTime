using YogaTime.Common.Entity.InterfaceDB;
using YogaTime.Context.Contracts.Models;
using YogaTime.Repositories.Contracts;

namespace YogaTime.Repositories.Implementations
{
    /// <inheritdoc cref="ITimeTableItemWriteRepository"/>
    public class TimeTableItemWriteRepository : BaseWriteRepository<TimeTableItem>,
        ITimeTableItemWriteRepository,
        IRepositoryAnchor
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="TimeTableItemWriteRepository"/>
        /// </summary>
        public TimeTableItemWriteRepository(IDbWriterContext writerContext)
            : base(writerContext)
        {
        }
    }
}
