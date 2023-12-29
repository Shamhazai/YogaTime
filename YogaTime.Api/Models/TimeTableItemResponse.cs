namespace YogaTime.Api.Models
{
    /// <summary>
    /// Модель ответа сущности элемент расписания
    /// </summary>
    public class TimeTableItemResponse
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
        /// Наименование занятия
        /// </summary>
        public string? NameYogaClass { get; set; }

        /// <summary>
        /// Наименование группы
        /// </summary>
        public string? NameGroup { get; set; }

        /// <summary>
        /// Наименование зала
        /// </summary>
        public string? NameRoom { get; set; }

        /// <summary>
        /// Инструктор имя
        /// </summary>
        public string? InstructorName { get; set; }

        /// <summary>
        /// Телефон
        /// </summary>
        public string? Phone { get; set; }
    }
}
