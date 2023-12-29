﻿namespace YogaTime.Services.Contracts.Models
{
    /// <summary>
    /// Модель группы
    /// </summary>
    public class GroupModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Наименование группы
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Описание группы
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Студенты группы
        /// </summary>
        public ICollection<PersonModel>? Clients { get; set; }

        /// <summary>
        /// <inheritdoc cref="PersonModel"/>
        /// </summary>
        public PersonModel? ClassroomInstructor { get; set; }
    }
}
