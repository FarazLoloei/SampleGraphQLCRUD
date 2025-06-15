using SampleGraphQLCRUD.API.Abstraction;
using SampleGraphQLCRUD.API.Data;
using SampleGraphQLCRUD.API.Models;

namespace SampleGraphQLCRUD.API.Services;

public class PurchaseService(ApplicationDbContext dbContext) : IPurchaseService
{
    public IQueryable<Purchase> GetAllPurchases()
        => dbContext.Purchases.AsQueryable();

    public Purchase GetPurchaseById(Guid id) // Updated to match the interface's non-nullable return type
    {
        var purchase = dbContext.Purchases.FirstOrDefault(p => p.Id == id);
        if (purchase == null)
            throw new InvalidOperationException($"Purchase with ID {id} not found.");

        return purchase;
    }

    public IEnumerable<Purchase> GetPurchasesByCustomerId(Guid customerId)
        => dbContext.Purchases.Where(p => p.CustomerId == customerId).ToList();

    public Purchase CreatePurchase(Purchase purchase)
    {
        // Ensure customer exists
        if (!dbContext.Customers.Any(c => c.Id == purchase.CustomerId))
            throw new InvalidOperationException("Customer does not exist");

        dbContext.Purchases.Add(purchase);
        dbContext.SaveChanges();
        return purchase;
    }

    public Purchase UpdatePurchase(Purchase purchase)
    {
        var existing = dbContext.Purchases.Find(purchase.Id);
        if (existing == null)
            throw new InvalidOperationException("Purchase not found");

        // Ensure customer exists if changing customer
        if (existing.CustomerId != purchase.CustomerId &&
            !dbContext.Customers.Any(c => c.Id == purchase.CustomerId))
        {
            throw new InvalidOperationException("New customer does not exist");
        }

        dbContext.Entry(existing).CurrentValues.SetValues(purchase);
        dbContext.SaveChanges();
        return existing;
    }

    public void DeletePurchase(Guid id)
    {
        var purchase = dbContext.Purchases.Find(id);
        if (purchase == null) throw new InvalidOperationException($"Purchase with ID {id.ToString()} not found.");

        dbContext.Purchases.Remove(purchase);
        dbContext.SaveChanges();
    }
}