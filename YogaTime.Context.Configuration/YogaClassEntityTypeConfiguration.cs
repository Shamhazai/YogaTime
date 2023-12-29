using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YogaTime.Context.Contracts.Models;

namespace YogaTime.Context.Configuration
{
    public class YogaClassEntityTypeConfiguration : IEntityTypeConfiguration<YogaClass>
    {
        public void Configure(EntityTypeBuilder<YogaClass> builder)
        {
            builder.ToTable("YogaClass");
            builder.HasIdAsKey();
            builder.PropertyAuditConfiguration();
            builder.Property(x => x.Name)
                .HasMaxLength(200)
                .IsRequired();

            builder
                .HasMany(x => x.TimeTableItem)
                .WithOne(x => x.YogaClass)
                .HasForeignKey(x => x.YogaClassId);

            builder.HasIndex(x => x.Name)
                .IsUnique()
                .HasFilter($"{nameof(YogaClass.DeletedAt)} is null")
                .HasDatabaseName($"IX_{nameof(YogaClass)}_{nameof(YogaClass.Name)}");
        }
    }
}
