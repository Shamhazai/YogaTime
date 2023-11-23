using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using YogaTime.Context.Contracts.Models;

namespace YogaTime.Context.Configuration
{
    public class EmployeeEntityTypeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employees");
            builder.HasIdAsKey();
            builder.PropertyAuditConfiguration();
            builder.HasMany(x => x.Group)
                .WithOne(x => x.Employee)
                .HasForeignKey(x => x.EmployeeId);

            builder
                .HasMany(x => x.TimeTableItem)
                .WithOne(x => x.Teacher)
                .HasForeignKey(x => x.TeacherId);
        }
    }
}
