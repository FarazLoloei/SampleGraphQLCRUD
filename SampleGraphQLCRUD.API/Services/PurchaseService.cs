using SampleGraphQLCRUD.API.Abstraction;
using SampleGraphQLCRUD.API.Data;
using SampleGraphQLCRUD.API.Models;

namespace SampleGraphQLCRUD.API.Services;

public class PurchaseService(ApplicationDbContext _context) : IPurchaseService
{
    public IQueryable<Purchase> GetAllPurchases()
        => _context.Purchases.AsQueryable();

    public Purchase GetPurchaseById(Guid id)
    {
        var purchase = _context.Purchases.FirstOrDefault(p => p.Id == id);
        if (purchase == null)
            throw new InvalidOperationException($"Purchase with ID {id} not found.");

        return purchase;
    }

    public IEnumerable<Purchase> GetPurchasesByCustomerId(Guid customerId)
        => _context.Purchases.Where(p => p.CustomerId == customerId).ToList();

    public Purchase CreatePurchase(Purchase purchase)
    {
        // Ensure customer exists
        if (!_context.Customers.Any(c => c.Id == purchase.CustomerId))
            throw new InvalidOperationException("Customer does not exist");

        _context.Purchases.Add(purchase);
        _context.SaveChanges();
        return purchase;
    }

    public Purchase UpdatePurchase(Purchase purchase)
    {
        var existing = _context.Purchases.Find(purchase.Id);
        if (existing == null)
            throw new InvalidOperationException("Purchase not found");

        // Ensure customer exists if changing customer
        if (existing.CustomerId != purchase.CustomerId &&
            !_context.Customers.Any(c => c.Id == purchase.CustomerId))
        {
            throw new InvalidOperationException("New customer does not exist");
        }

        _context.Entry(existing).CurrentValues.SetValues(purchase);
        _context.SaveChanges();
        return existing;
    }

    public void DeletePurchase(Guid id)
    {
        var purchase = _context.Purchases.Find(id);
        if (purchase == null)
            throw new InvalidOperationException($"Purchase with ID {id} not found.");

        _context.Purchases.Remove(purchase);
        _context.SaveChanges();
    }
}