namespace YogaTime.Context.Contracts.Models
{
    /// <summary>
    /// Сотрудник
    /// </summary>
    public class Employee: BaseAuditEntity
    {
        /// <summary>
        /// Идентификатор <inheritdoc cref="Person"/>
        /// </summary>
        public Guid PersonId { get; set; }


        /// <summary>
        /// Делаем связь один ко многим
        /// </summary>
        public Person Person { get; set; }

        /// <summary>
        /// Стаж
        /// </summary>
        public int Exp { get; set; }

        /// <summary>
        /// нужна для связи один ко многим по вторичному ключу <see cref="TimeTableItem"/>
        /// </summary>
        public ICollection<TimeTableItem> TimeTableItem { get; set; }
    }
}
