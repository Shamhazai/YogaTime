namespace YogaTime.Context.Contracts.Models
{
    /// <summary>
    /// Сотрудник
    /// </summary>
    public class Employee: BaseAuditEntity
    {
        /// <summary>
        /// Идентификатор <inheritdoc cref="Person"/>
        /// </summary>
        public Guid PersonId { get; set; }

        /// <summary>
        /// Стаж
        /// </summary>
        public int Exp { get; set; }
    }
}
