using YogaTime.Services.Contracts.Models;
using YogaTime.Services.Contracts.ModelsRequest;

namespace YogaTime.Services.Contracts.Interface
{
    public interface IInstructorService
    {
        /// <summary>
        /// Получить список всех <see cref="InstructorModel"/>
        /// </summary>
        Task<IEnumerable<InstructorModel>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="InstructorModel"/> по идентификатору
        /// </summary>
        Task<InstructorModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Добавляет нового инструктора
        /// </summary>
        Task<InstructorModel> AddAsync(InstructorRequestModel request, CancellationToken cancellationToken);

        /// <summary>
        /// Редактирует существующего инструктора
        /// </summary>
        Task<InstructorModel> EditAsync(InstructorRequestModel source, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет существующего инструктора
        /// </summary>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
