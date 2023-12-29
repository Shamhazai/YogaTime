using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YogaTime.Context.Contracts.Models;

namespace YogaTime.Context.Configuration
{
    public class InstructorEntityTypeConfiguration : IEntityTypeConfiguration<Instructor>
    {
        public void Configure(EntityTypeBuilder<Instructor> builder)
        {
            builder.ToTable("Instructors");
            builder.HasIdAsKey();
            builder.PropertyAuditConfiguration();
            builder.HasMany(x => x.Group)
                .WithOne(x => x.Instructor)
                .HasForeignKey(x => x.InstructorId);

            builder
                .HasMany(x => x.TimeTableItem)
                .WithOne(x => x.Instructor)
                .HasForeignKey(x => x.InstructorId);
        }
    }
}
