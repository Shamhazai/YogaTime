namespace YogaTime.Context.Contracts.Models
{
    public class Group
    {
        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Описание
        /// </summary>
        public string? Description { get; set; } = string.Empty;

        /// <summary>
        /// нужна для связи один ко многим по вторичному ключу <see cref="TimeTableItem"/>
        /// </summary>
        public ICollection<TimeTableItem> TimeTableItem { get; set; }

       

        /// <summary>
        /// нужна для связи один ко многим по вторичному ключу <see cref="Lesson"/>
        /// </summary>
        public ICollection<Lesson> Lesson { get; set; }

        /// <summary>
        /// нужна для связи один ко многим по вторичному ключу <see cref="Person"/>
        /// </summary>
        public ICollection<Person> Person { get; set; }
    }
}
