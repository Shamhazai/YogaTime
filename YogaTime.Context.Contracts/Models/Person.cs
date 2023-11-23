namespace YogaTime.Context.Contracts.Models
{
    /// <summary>
    /// Сущность клиента
    /// </summary>
    public class Person: BaseAuditEntity
    {
        /// <summary>
        /// Фамилия
        /// </summary>
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Отчество
        /// </summary>
        public string? Patronymic { get; set; }

        /// <summary>
        /// Адрес электронной почты
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Телефон
        /// </summary>
        public string Phone { get; set; } = string.Empty;

        public Guid  GroupId { get; set; }

        /// <summary>
        /// Делаем связь один ко многим
        /// </summary>
        public Group Group { get; set; }


        /// <summary>
        /// нужна для связи один ко многим по вторичному ключу <see cref="Employee"/>
        /// </summary>
        public ICollection<Employee> Employee { get; set; }

    }
}
