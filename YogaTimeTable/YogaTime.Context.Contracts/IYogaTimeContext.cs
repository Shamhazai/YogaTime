using Microsoft.EntityFrameworkCore;
using YogaTime.Context.Contracts.Models;

namespace YogaTime.Context.Contracts
{
    public interface IYogaTimeContext
    {
        /// <summary>Список <inheritdoc cref="Person"/></summary>
        DbSet<Person> Persons { get; }

        /// <summary>Список <inheritdoc cref="Employee"/></summary>
        DbSet<Employee> Employees { get; }

        /// <summary>Список <inheritdoc cref="Group"/></summary>
        DbSet<Group> Groups { get; }

        /// <summary>Список <inheritdoc cref="Room"/></summary>
        DbSet<Room> Rooms { get; }

        /// <summary>Список <inheritdoc cref="Studio"/></summary>
        DbSet<Studio> Studios { get; }

        /// <summary>Список <inheritdoc cref="Lesson"/></summary>
        DbSet<Lesson> Lessons { get; }

        /// <summary>Список <inheritdoc cref="TimeTableItem"/></summary>
        DbSet<TimeTableItem> TimeTableItems { get; }
    }
}
