using YogaTime.Services.Contracts.Models;

namespace YogaTime.Services.Contracts.ModelsRequest
{
    public class TimeTableItemRequestModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Дата начала
        /// </summary>
        public DateTimeOffset StartDate { get; set; }

        /// <summary>
        /// Дата окончания
        /// </summary>
        public DateTimeOffset EndDate { get; set; }

        /// <summary>
        /// <inheritdoc cref="YogaClassModel"/>
        /// </summary>
        public Guid YogaClass { get; set; }

        /// <summary>
        /// <inheritdoc cref="YogaClassModel"/>
        /// </summary>
        public Guid Room { get; set; }


        /// <summary>
        /// <inheritdoc cref="GroupModel"/>
        /// </summary>
        public Guid Group { get; set; }

        /// <summary>
        /// <inheritdoc cref="PersonModel"/>
        /// </summary>
        public Guid Instructor { get; set; }
    }
}
