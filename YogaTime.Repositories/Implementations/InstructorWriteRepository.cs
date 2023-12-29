using YogaTime.Common.Entity.InterfaceDB;
using YogaTime.Context.Contracts.Models;
using YogaTime.Repositories.Contracts;

namespace YogaTime.Repositories.Implementations
{
    /// <inheritdoc cref="IInstructorWriteRepository"/>
    public class InstructorWriteRepository : BaseWriteRepository<Instructor>,
        IInstructorWriteRepository,
        IRepositoryAnchor
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="InstructorWriteRepository"/>
        /// </summary>
        public InstructorWriteRepository(IDbWriterContext writerContext)
            : base(writerContext)
        {
        }
    }
}
