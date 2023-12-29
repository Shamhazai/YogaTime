namespace YogaTime.Api.ModelsRequest.YogaClass
{
    /// <summary>
    /// Модель запроса создания занятия
    /// </summary>
    public class CreateYogaClassRequest
    {
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
