namespace YogaTime.Context.Contracts.Models
{
    /// <summary>
    /// Группа
    /// </summary>
    public class Group : BaseAuditEntity
    {
        /// <summary>
        /// Инструктор
        /// </summary>
        public Guid? InstructorId { get; set; }

        /// <summary>
        /// Делаем связь один ко многим
        /// </summary>
        public Instructor? Instructor { get; set; }

        /// <summary>
        /// Наименование группы
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Описание группы
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// связь один ко многим
        /// </summary>
        public ICollection<Person>? Clients { get; set; }

        /// <summary>
        /// нужна для связи один ко многим
        /// </summary>
        public ICollection<TimeTableItem> TimeTableItem { get; set; }
    }
}
