namespace YogaTime.Context.Contracts.Models
{
    /// <summary>
    /// Зал
    /// </summary>
    public class Room : BaseAuditEntity
    {
        /// <summary>
        /// Наименование
        /// </summary>
        public int Number { get; set; } 

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

        /// <summary>
        /// нужна для связи один ко многим по вторичному ключу <see cref="Studio"/>
        /// </summary>
        public ICollection<Studio> Studio { get; set; }
    }
}
