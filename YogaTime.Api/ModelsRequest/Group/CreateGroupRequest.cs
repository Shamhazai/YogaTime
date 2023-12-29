using YogaTime.Services.Contracts.Models;

namespace YogaTime.Api.ModelsRequest.Group
{
    public class CreateGroupRequest
    {
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
