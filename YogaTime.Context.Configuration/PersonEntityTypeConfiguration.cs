﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YogaTime.Context.Contracts.Models;

namespace YogaTime.Context.Configuration
{
    public class PersonEntityTypeConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("Persons");
            builder.HasIdAsKey();
            builder.PropertyAuditConfiguration();
            builder.Property(x => x.FirstName).IsRequired();
            builder.Property(x => x.LastName).IsRequired();
            builder.Property(x => x.Email).IsRequired();
            builder.Property(x => x.Phone).IsRequired();

            builder
               .HasMany(x => x.Instructor)
               .WithOne(x => x.Person)
               .HasForeignKey(x => x.PersonId);


            builder.HasIndex(x => x.Email)
                .IsUnique()
                .HasFilter($"{nameof(Person.DeletedAt)} is null")
                .HasDatabaseName($"IX_{nameof(Person)}_{nameof(Person.Email)}");

            builder.HasIndex(x => x.Phone)
               .IsUnique()
               .HasFilter($"{nameof(Person.DeletedAt)} is null")
               .HasDatabaseName($"IX_{nameof(Person)}_{nameof(Person.Phone)}");
        }
    }
}
