using YogaTime.Services.Contracts.Models;

namespace YogaTime.Services.Contracts.Interface
{
    /// <summary>
    /// 
    /// </summary>
    public interface IRoomService
    {
        /// <summary>
        /// Получить список всех <see cref="RoomModel"/>
        /// </summary>
        Task<IEnumerable<RoomModel>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="RoomModel"/> по идентификатору
        /// </summary>
        Task<RoomModel> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Добавляет новый зал
        /// </summary>
        Task<RoomModel> AddAsync(string name, string description, CancellationToken cancellationToken);

        /// <summary>
        /// Редактирует существующий зал
        /// </summary>
        Task<RoomModel> EditAsync(RoomModel source, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет существующий зал
        /// </summary>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
