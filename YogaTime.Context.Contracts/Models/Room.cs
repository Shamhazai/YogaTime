namespace YogaTime.Context.Contracts.Models
{
    /// <summary>
    /// Занятие
    /// </summary>
    public class Room : BaseAuditEntity
    {
        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Описание
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// нужна для связи один ко многим по вторичному ключу <see cref="TimeTableItem"/>
        /// </summary>
        public ICollection<TimeTableItem> TimeTableItem { get; set; }
    }
}
