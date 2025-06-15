using SampleGraphQLCRUD.API.Models;

namespace SampleGraphQLCRUD.API.Abstraction;

public interface IPurchaseService
{
    IQueryable<Purchase> GetAllPurchases();

    Purchase GetPurchaseById(int id);

    IEnumerable<Purchase> GetPurchasesByCustomerId(int customerId);

    Purchase CreatePurchase(Purchase purchase);

    Purchase UpdatePurchase(Purchase purchase);

    bool DeletePurchase(int id);
}