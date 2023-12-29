using YogaTime.Common.Entity.InterfaceDB;
using YogaTime.Context.Contracts.Models;
using YogaTime.Repositories.Contracts;

namespace YogaTime.Repositories.Implementations
{
    /// <inheritdoc cref="IGroupWriteRepository"/>
    public class GroupWriteRepository : BaseWriteRepository<Group>,
        IGroupWriteRepository,
        IRepositoryAnchor
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="GroupWriteRepository"/>
        /// </summary>
        public GroupWriteRepository(IDbWriterContext writerContext)
            : base(writerContext)
        {
        }
    }
}
