﻿using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using Ambev.DeveloperEvaluation.ORM.Mapping;
using Ambev.DeveloperEvaluation.ORM.Seeds;

namespace Ambev.DeveloperEvaluation.ORM;

public class DefaultContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Sale> Sales { get; set; }
    public DbSet<SaleItem> SaleItems { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartProduct> CartProducts { get; set; }

    public DefaultContext(DbContextOptions<DefaultContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new SaleConfiguration());
        modelBuilder.ApplyConfiguration(new SaleItemConfiguration());
        modelBuilder.ApplyConfiguration(new ProductConfiguration());
        modelBuilder.ApplyConfiguration(new CartConfiguration());
        modelBuilder.ApplyConfiguration(new CartProductConfiguration());
        
        // Add seeds
        modelBuilder.SeedProducts();
    }
}
public class YourDbContextFactory : IDesignTimeDbContextFactory<DefaultContext>
{
    public DefaultContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var builder = new DbContextOptionsBuilder<DefaultContext>();
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        builder.UseNpgsql(
               connectionString,
               b => b.MigrationsAssembly("Ambev.DeveloperEvaluation.ORM")
        );

        return new DefaultContext(builder.Options);
    }
}