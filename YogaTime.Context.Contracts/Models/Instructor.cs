namespace YogaTime.Context.Contracts.Models
{
    /// <summary>
    /// Сотрудник
    /// </summary>
    public class Instructor : BaseAuditEntity
    {
        public string Desc { get; set; } = "Младший";

        /// <summary>
        /// нужна для связи один ко многим по вторичному ключу <see cref="Group"/>
        /// </summary>
        public ICollection<Group>? Group { get; set; }

        /// <summary>
        /// Идентификатор <inheritdoc cref="Person"/>
        /// </summary>
        public Guid PersonId { get; set; }

        /// <summary>
        /// Делаем связь один ко многим
        /// </summary>
        public Person Person { get; set; }

        /// <summary>
        /// нужна для связи один ко многим по вторичному ключу <see cref="TimeTableItem"/>
        /// </summary>
        public ICollection<TimeTableItem>? TimeTableItem { get; set; }
    }
}
