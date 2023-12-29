using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YogaTime.Context.Contracts.Models;

namespace YogaTime.Context.Configuration
{
    public class RoomEntityTypeConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.ToTable("Room");
            builder.HasIdAsKey();
            builder.PropertyAuditConfiguration();
            builder.Property(x => x.Name)
                .HasMaxLength(200)
                .IsRequired();

            builder
                .HasMany(x => x.TimeTableItem)
                .WithOne(x => x.Room)
                .HasForeignKey(x => x.RoomId);

            builder.HasIndex(x => x.Name)
                .IsUnique()
                .HasFilter($"{nameof(Room.DeletedAt)} is null")
                .HasDatabaseName($"IX_{nameof(Room)}_{nameof(Room.Name)}");
        }
    }
}
