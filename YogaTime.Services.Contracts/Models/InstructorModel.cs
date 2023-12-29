namespace YogaTime.Services.Contracts.Models
{
    /// <summary>
    /// Модель инструкторов
    /// </summary>
    public class InstructorModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        public string Desc { get; set; }

        /// <summary>
        /// <inheritdoc cref="PersonModel"/>
        /// </summary>
        public PersonModel? Person { get; set; }
    }
}
