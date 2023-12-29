using YogaTime.Common.Entity.InterfaceDB;
using YogaTime.Context.Contracts.Models;
using YogaTime.Repositories.Contracts;

namespace YogaTime.Repositories.Implementations
{
    /// <inheritdoc cref="IPersonWriteRepository"/>
    public class PersonWriteRepository : BaseWriteRepository<Person>,
        IPersonWriteRepository,
        IRepositoryAnchor
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="PersonWriteRepository"/>
        /// </summary>
        public PersonWriteRepository(IDbWriterContext writerContext)
            : base(writerContext)
        {
        }
    }
}
