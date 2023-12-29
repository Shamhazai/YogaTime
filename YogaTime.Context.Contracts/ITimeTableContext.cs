using Microsoft.EntityFrameworkCore;
using YogaTime.Context.Contracts.Models;

namespace YogaTime.Context.Contracts
{
    /// <summary>
    /// Контекст работы с сущностями
    /// </summary>
    public interface ITimeTableContext
    {
        /// <summary>Список <inheritdoc cref="YogaClass"/></summary>
        DbSet<YogaClass> YogaClasses { get; }

        /// <summary>Список <inheritdoc cref="Room"/></summary>
        DbSet<Room> Rooms { get; }

        /// <summary>Список <inheritdoc cref="Instructor"/></summary>
        DbSet<Instructor> Instructors { get; }

        /// <summary>Список <inheritdoc cref="Group"/></summary>
        DbSet<Group> Groups { get; }

        /// <summary>Список <inheritdoc cref="Person"/></summary>
        DbSet<Person> Persons { get; }

        /// <summary>Список <inheritdoc cref="TimeTableItem"/></summary>
        DbSet<TimeTableItem> TimeTableItems { get; }

    }
}
