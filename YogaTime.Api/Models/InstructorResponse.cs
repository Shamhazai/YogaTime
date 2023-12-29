
namespace YogaTime.Api.Models
{
    /// <summary>
    /// Модель ответа сущности инструкторов
    /// </summary>
    public class InstructorResponse

    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        public string Desc { get; set; }

        /// <summary>
        /// ФИО
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Мобильный телефон
        /// </summary>
        public string MobilePhone { get; set; } = string.Empty;
    }
}
