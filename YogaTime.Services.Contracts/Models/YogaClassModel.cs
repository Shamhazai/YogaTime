namespace YogaTime.Services.Contracts.Models
{
    /// <summary>
    /// Модель занятия йогой
    /// </summary>
    public class YogaClassModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; } = string.Empty;
    }
}
