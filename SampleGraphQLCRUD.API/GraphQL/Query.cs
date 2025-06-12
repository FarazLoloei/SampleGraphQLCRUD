using SampleGraphQLCRUD.API.Data;
using SampleGraphQLCRUD.API.Models;

namespace SampleGraphQLCRUD.API.GraphQL;

public class Query
{
    private readonly ApplicationDbContext _db;

    public Query(ApplicationDbContext db)
    {
        _db = db;
    }

    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Customer> GetCustomers() => _db.Customers;

    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Purchase> GetPurchases() => _db.Purchases;
}