﻿using YogaTime.Context.Contracts.Models;

namespace YogaTime.Repositories.Contracts
{

    /// <summary>
    /// Репозиторий чтения <see cref="Group"/>
    /// </summary>
    public interface IGroupReadRepository
    {
        /// <summary>
        /// Получить список всех <see cref="Group"/>
        /// </summary>
        Task<IReadOnlyCollection<Group>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="Group"/> по идентификатору
        /// </summary>
        Task<Group?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получить список <see cref="Group"/> по идентификаторам
        /// </summary>
        Task<Dictionary<Guid, Group>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellation);
    }

}
