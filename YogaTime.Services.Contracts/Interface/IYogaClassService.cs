using YogaTime.Services.Contracts.Models;

namespace YogaTime.Services.Contracts.Interface
{
    /// <summary>
    /// 
    /// </summary>
    public interface IYogaClassService
    {
        /// <summary>
        /// Получить список всех <see cref="YogaClassModel"/>
        /// </summary>
        Task<IEnumerable<YogaClassModel>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="YogaClassModel"/> по идентификатору
        /// </summary>
        Task<YogaClassModel> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Добавляет новое занятие
        /// </summary>
        Task<YogaClassModel> AddAsync(string name, string description, CancellationToken cancellationToken);

        /// <summary>
        /// Редактирует существующее занятие
        /// </summary>
        Task<YogaClassModel> EditAsync(YogaClassModel source, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет существующее занятие
        /// </summary>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
