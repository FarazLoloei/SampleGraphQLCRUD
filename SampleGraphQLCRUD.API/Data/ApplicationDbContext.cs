using Microsoft.EntityFrameworkCore;
using SampleGraphQLCRUD.API.Models;

namespace SampleGraphQLCRUD.API.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<Customer> Customers => Set<Customer>();

    public DbSet<Purchase> Purchases => Set<Purchase>();

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }
}