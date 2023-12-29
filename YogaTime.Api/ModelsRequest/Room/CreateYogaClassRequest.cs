namespace YogaTime.Api.ModelsRequest.Room
{
    /// <summary>
    /// Модель запроса создания зала
    /// </summary>
    public class CreateRoomRequest
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
