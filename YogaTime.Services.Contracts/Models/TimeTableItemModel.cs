namespace YogaTime.Services.Contracts.Models
{
    /// <summary>
    /// Модель элемент расписания
    /// </summary>
    public class TimeTableItemModel
    {

        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Дата начала
        /// </summary>
        public DateTimeOffset StartDate { get; set; }

        /// <summary>
        /// Дата окончания
        /// </summary>
        public DateTimeOffset EndDate { get; set; }

        /// <summary>
        /// <inheritdoc cref="YogaClassModel"/>
        /// </summary>
        public YogaClassModel? YogaClass { get; set; }

        /// <summary>
        /// <inheritdoc cref="RoomModel"/>
        /// </summary>
        public RoomModel? Room { get; set; }


        /// <summary>
        /// <inheritdoc cref="GroupModel"/>
        /// </summary>
        public GroupModel? Group { get; set; }

        /// <summary>
        /// <inheritdoc cref="PersonModel"/>
        /// </summary>
        public PersonModel? Instructor { get; set; }
    }
}
