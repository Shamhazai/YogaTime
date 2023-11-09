using Microsoft.EntityFrameworkCore;
using YogaTime.Context.Contracts;
using YogaTime.Context.Contracts.Models;

namespace YogaTime.Context
{
    public class YogaTimeContext : DbContext, IYogaTimeContext
    {
        public DbSet<Person> Persons { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Group> Groups { get; set; }

        public DbSet<Room> Rooms { get; set; }

        public DbSet<Studio> Studios { get; set; }

        public DbSet<Lesson> Lessons { get; set; }

        public DbSet<TimeTableItem> TimeTableItems { get; set; }

        public YogaTimeContext(DbContextOptions<YogaTimeContext> options) : base(options){ }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
