﻿namespace YogaTime.Context.Contracts.Models
{
    /// <summary>
    /// Сущность участника
    /// </summary>
    public class Person : BaseAuditEntity
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

        /// <summary>
        /// сваязь один ко многим
        /// </summary>
        public Guid? GroupId { get; set; }

        /// <summary>
        /// сваязь один ко многим
        /// </summary>
        public Group? Group { get; set; }

        /// <summary>
        /// нужна для связи один ко многим по вторичному ключу <see cref="Instructor"/>
        /// </summary>
        public ICollection<Instructor> Instructor { get; set; }
    }
}
