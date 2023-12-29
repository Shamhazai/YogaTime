using YogaTime.Common.Entity.InterfaceDB;
using YogaTime.Context.Contracts.Models;
using YogaTime.Repositories.Contracts;

namespace YogaTime.Repositories.Implementations
{
    /// <inheritdoc cref="IYogaClassWriteRepository"/>
    public class YogaClassWriteRepository : BaseWriteRepository<YogaClass>,
        IYogaClassWriteRepository,
        IRepositoryAnchor
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="YogaClassWriteRepository"/>
        /// </summary>
        public YogaClassWriteRepository(IDbWriterContext writerContext)
            : base(writerContext)
        {
        }
    }
}
