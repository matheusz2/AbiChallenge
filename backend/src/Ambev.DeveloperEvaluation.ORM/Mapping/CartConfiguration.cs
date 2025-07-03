using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

/// <summary>
/// Entity Framework configuration for Cart entity
/// </summary>
public class CartConfiguration : IEntityTypeConfiguration<Cart>
{
    /// <summary>
    /// Configures the Cart entity mapping
    /// </summary>
    /// <param name="builder">Entity type builder for Cart</param>
    public void Configure(EntityTypeBuilder<Cart> builder)
    {
        builder.ToTable("Carts");

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id)
            .HasColumnType("uuid")
            .HasDefaultValueSql("gen_random_uuid()");

        builder.Property(c => c.UserId)
            .IsRequired();

        builder.Property(c => c.Date)
            .IsRequired();

        builder.Property(c => c.CreatedAt)
            .IsRequired();

        builder.Property(c => c.UpdatedAt);

        // Configure one-to-many relationship with CartProducts
        builder.HasMany(c => c.Products)
            .WithOne(p => p.Cart)
            .HasForeignKey(p => p.CartId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

/// <summary>
/// Entity Framework configuration for CartProduct entity
/// </summary>
public class CartProductConfiguration : IEntityTypeConfiguration<CartProduct>
{
    /// <summary>
    /// Configures the CartProduct entity mapping
    /// </summary>
    /// <param name="builder">Entity type builder for CartProduct</param>
    public void Configure(EntityTypeBuilder<CartProduct> builder)
    {
        builder.ToTable("CartProducts");

        // Composite primary key
        builder.HasKey(cp => new { cp.CartId, cp.ProductId });

        builder.Property(cp => cp.CartId)
            .IsRequired();

        builder.Property(cp => cp.ProductId)
            .IsRequired();

        builder.Property(cp => cp.Quantity)
            .IsRequired();

        // Configure relationship with Cart
        builder.HasOne(cp => cp.Cart)
            .WithMany(c => c.Products)
            .HasForeignKey(cp => cp.CartId)
            .OnDelete(DeleteBehavior.Cascade);
    }
} 