using Microsoft.EntityFrameworkCore;
using TimeTable203.Common.Entity.InterfaceDB;
using YogaTime.Context.Contracts;
using YogaTime.Context.Contracts.Models;

namespace YogaTime.Context
{
    public class YogaTimeContext : DbContext, 
        IYogaTimeContext,
         IDbRead,
        IDbWriter,
        IUnitOfWork
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
            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(IContextConfigurationAnchor).Assembly);
        }

        IQueryable<TEntity> IDbRead.Read<TEntity>()
            => base.Set<TEntity>()
                .AsNoTracking()
                .AsQueryable();

        void IDbWriter.Add<TEntities>(TEntities entity)
            => base.Entry(entity).State = EntityState.Added;

        void IDbWriter.Update<TEntities>(TEntities entity)
              => base.Entry(entity).State = EntityState.Modified;

        void IDbWriter.Delete<TEntities>(TEntities entity)
              => base.Entry(entity).State = EntityState.Deleted;


        async Task<int> IUnitOfWork.SaveChangesAsync(CancellationToken cancellationToken)
        {
            Log.Information("Идет сохранение данных в бд");
            var count = await base.SaveChangesAsync(cancellationToken);
            foreach (var entry in base.ChangeTracker.Entries().ToArray())
            {
                entry.State = EntityState.Detached;
            }
            return count;
        }
    }
}
