namespace YogaTime.Context.Contracts.Models
{
    /// <summary>
    /// Зал
    /// </summary>
    public class Room : BaseAuditEntity
    {
        /// <summary>
        /// Наименование
        /// </summary>
        public int Number { get; set; } 

        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Описание
        /// </summary>
        public string? Description { get; set; }

    }
}
