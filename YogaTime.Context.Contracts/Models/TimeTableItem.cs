namespace YogaTime.Context.Contracts.Models
{
    /// <summary>
    /// Сущность расписания
    /// </summary>
    public class TimeTableItem
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
        /// Группа, в которой находятся клиенты
        /// </summary>
        public Guid GroupId { get; set; }

        public Group Group { get; set; } 

        /// <summary>
        /// Инструктор
        /// </summary>
        public Guid EmployeeId { get; set; }

        public Employee Emploee { get; set; }
        /// <summary>
        /// Зал
        /// </summary>
        public Guid RoomId { get; set; }

        public Room Room { get; set; }

        /// <summary>
        /// Студия
        /// </summary>
        public Guid StudioId { get; set; }

        public Studio Studio { get; set; }
    }
}
