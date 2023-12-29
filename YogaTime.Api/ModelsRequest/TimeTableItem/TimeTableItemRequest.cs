using YogaTime.Api.ModelsRequest.TimeTableItemRequest;

namespace YogaTime.Api.ModelsRequest.TimeTableItem
{
    public class TimeTableItemRequest : CreateTimeTableItemRequest
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }
    }
}
