using YogaTime.Common.Entity.InterfaceDB;
using YogaTime.Context.Contracts.Models;
using YogaTime.Repositories.Contracts;

namespace YogaTime.Repositories.Implementations
{
    /// <inheritdoc cref="IRoomWriteRepository"/>
    public class RoomWriteRepository : BaseWriteRepository<Room>,
        IRoomWriteRepository,
        IRepositoryAnchor
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="RoomWriteRepository"/>
        /// </summary>
        public RoomWriteRepository(IDbWriterContext writerContext)
            : base(writerContext)
        {
        }
    }
}
