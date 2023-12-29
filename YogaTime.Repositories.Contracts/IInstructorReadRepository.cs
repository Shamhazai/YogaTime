using YogaTime.Context.Contracts.Models;

namespace YogaTime.Repositories.Contracts
{
    /// <summary>
    /// Репозиторий чтения <see cref="Instructor"/>
    /// </summary>
    public interface IInstructorReadRepository
    {
        /// <summary>
        /// Получить список всех <see cref="Instructor"/>
        /// </summary>
        Task<IReadOnlyCollection<Instructor>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="Instructor"/> по идентификатору
        /// </summary>
        Task<Instructor?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получить список <see cref="Instructor"/> по идентификаторам
        /// </summary>
        Task<Dictionary<Guid, Instructor>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellation);

        /// <summary>
        /// Получить список <see cref="Person"/> по id инструкторов
        /// </summary>
        Task<Dictionary<Guid, Person?>> GetPersonByInstructorIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellation);
    }
}
