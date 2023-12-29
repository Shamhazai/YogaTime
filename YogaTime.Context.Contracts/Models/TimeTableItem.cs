namespace YogaTime.Context.Contracts.Models
{
    /// <summary>
    /// Элемент расписания
    /// </summary>
    public class TimeTableItem : BaseAuditEntity
    {
        /// <summary>
        /// Дата начала
        /// </summary>
        public DateTimeOffset StartDate { get; set; }

        /// <summary>
        /// Дата окончания
        /// </summary>
        public DateTimeOffset EndDate { get; set; }

        /// <summary>
        /// Идентификатор занятия
        /// </summary>
        public Guid YogaClassId { get; set; }

        /// <summary>
        /// Делаем связь один ко многим
        /// </summary>
        public YogaClass YogaClass { get; set; }


        public Guid RoomId { get; set; }


        /// <summary>
        /// Делаем связь один ко многим
        /// </summary>
        public Room Room { get; set; }


        /// <summary>
        /// Идентификатор группы
        /// </summary>
        public Guid GroupId { get; set; }

        /// <summary>
        /// Делаем связь один ко многим
        /// </summary>
        public Group Group { get; set; }

        /// <summary>
        /// Преподаватель
        /// </summary>
        public Guid? InstructorId { get; set; }

        /// <summary>
        /// Преподаватель
        /// </summary>
        public Instructor? Instructor { get; set; }

    }
}
