using YogaTime.Context.Contracts.Models;

namespace YogaTime.Repositories.Contracts
{
    /// <summary>
    /// Репозиторий чтения <see cref="YogaClass"/>
    /// </summary>
    public interface IYogaClassReadRepository
    {
        /// <summary>
        /// Получить список всех <see cref="YogaClass"/>
        /// </summary>
        Task<IReadOnlyCollection<YogaClass>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="YogaClass"/> по идентификатору
        /// </summary>
        Task<YogaClass?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        /// <summary>
        /// Получить список <see cref="YogaClass"/> по идентификаторам
        /// </summary>
        Task<Dictionary<Guid, YogaClass>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellation);
    }
}
