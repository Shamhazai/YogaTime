namespace YogaTime.Context.Contracts.Models
{
    public class Studio
    {
        /// <summary>
        /// Адрес
        /// </summary>
        public string Addres { get; set; } = string.Empty;

        public Guid RoomId { get; set; }


        /// <summary>
        /// Делаем связь один ко многим
        /// </summary>
        public Room Room { get; set; }

        /// <summary>
        /// нужна для связи один ко многим по вторичному ключу <see cref="TimeTableItem"/>
        /// </summary>
        public ICollection<TimeTableItem> TimeTableItem { get; set; }

    }
}
