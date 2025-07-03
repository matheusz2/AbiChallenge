using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.RegularExpressions;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id)
            .HasColumnType("uuid")
            .HasDefaultValueSql("gen_random_uuid()");

        builder.Property(u => u.Username)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(u => u.Username)
            .IsUnique();

        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasIndex(u => u.Email)
            .IsUnique();

        builder.Property(u => u.Phone)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(u => u.Password)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(u => u.Role)
            .IsRequired()
            .HasConversion<string>();

        builder.Property(u => u.Status)
            .IsRequired()
            .HasConversion<string>();

        builder.Property(u => u.CreatedAt)
            .IsRequired();

        builder.Property(u => u.UpdatedAt);

        // Configure Name as owned entity
        builder.OwnsOne(u => u.Name, name =>
        {
            name.Property(n => n.Firstname)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("FirstName");

            name.Property(n => n.Lastname)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("LastName");
        });

        // Configure Address as owned entity
        builder.OwnsOne(u => u.Address, address =>
        {
            address.Property(a => a.City)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("City");

            address.Property(a => a.Street)
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnName("Street");

            address.Property(a => a.Number)
                .IsRequired()
                .HasColumnName("Number");

            address.Property(a => a.Zipcode)
                .IsRequired()
                .HasMaxLength(20)
                .HasColumnName("Zipcode");

            // Configure Geolocation as nested owned entity
            address.OwnsOne(a => a.Geolocation, geo =>
            {
                geo.Property(g => g.Lat)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("Latitude");

                geo.Property(g => g.Long)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("Longitude");
            });
        });
    }
}
