namespace YogaTime.Services.Contracts.ModelsRequest
{
    public class GroupRequestModel
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

        /// <summary>
        /// <inheritdoc cref="PersonModel"/>
        /// </summary>
        public Guid? ClassroomInstructor { get; set; }
    }
}
