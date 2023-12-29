﻿using YogaTime.Services.Contracts.Models;
using YogaTime.Services.Contracts.ModelsRequest;

namespace YogaTime.Services.Contracts.Interface
{
    public interface IGroupService
    {
        /// <summary>
        /// Получить список всех <see cref="GroupModel"/>
        /// </summary>
        Task<IEnumerable<GroupModel>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="GroupModel"/> по идентификатору
        /// </summary>
        Task<GroupModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Добавляет новую группу
        /// </summary>
        Task<GroupModel> AddAsync(GroupRequestModel groupRequestModel, CancellationToken cancellationToken);

        /// <summary>
        /// Редактирует существующую группу
        /// </summary>
        Task<GroupModel> EditAsync(GroupRequestModel source, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет существующую группу
        /// </summary>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
