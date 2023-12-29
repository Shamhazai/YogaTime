using YogaTime.Services.Contracts.Models;

namespace YogaTime.Services.Contracts.ModelsRequest
{
    public class InstructorRequestModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <inheritdoc cref="Descs"/>
        public string Desc { get; set; }

        /// <summary>
        /// <inheritdoc cref="PersonModel"/>
        /// </summary>
        public Guid PersonId { get; set; }
    }
}
