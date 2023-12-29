using YogaTime.Services.Contracts.Models;

namespace YogaTime.Api.ModelsRequest.Instructor
{
    public class CreateInstructorRequest
    {


        /// <summary>
        /// <inheritdoc cref="PersonModel"/>
        /// </summary>
        public Guid PersonId { get; set; }

        /// <inheritdoc cref="DescsResponse"/>
        public string Desc { get; set; }
    }
}
