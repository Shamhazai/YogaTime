using YogaTime.Context.Contracts.Enums;

namespace YogaTime.Context.Contracts.Models
{
    /// <summary>
    /// Занятие
    /// </summary>
    public class Lesson
    {

        /// <summary>
        /// Тип
        /// </summary>
        public LessonsType Type { get; set; } 

        /// <summary>
        /// Описание
        /// </summary>
        public string? Description { get; set; }

        
    }
}
