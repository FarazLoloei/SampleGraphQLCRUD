using Microsoft.EntityFrameworkCore;
using SampleGraphQLCRUD.API.Models;

namespace SampleGraphQLCRUD.API.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<Customer> Customers => Set<Customer>();

    public DbSet<Purchase> Purchases => Set<Purchase>();

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Customer configuration
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Email).IsRequired();
            entity.HasIndex(c => c.Email).IsUnique();
            entity.HasMany(c => c.Purchases)
                  .WithOne(p => p.Customer)
                  .HasForeignKey(p => p.CustomerId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // Purchase configuration
        modelBuilder.Entity<Purchase>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Price);
            entity.Property(p => p.ProductName).IsRequired();
        });
    }
}