using YogaTime.Services.Contracts.Models;

namespace YogaTime.Api.Models
{
    /// <summary>
    /// Модель ответа сущности группы
    /// </summary>
    public class GroupResponse
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Наименование группы
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Описание группы
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Участники группы
        /// </summary>
        public ICollection<PersonModel>? Clients { get; set; }

        /// <summary>
        /// <inheritdoc cref="InstructorModel"/>
        /// </summary>
        public PersonModel? ClassroomInstructor { get; set; }
    }
}
