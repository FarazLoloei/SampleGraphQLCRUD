using SampleGraphQLCRUD.API.Models;

namespace SampleGraphQLCRUD.API.Abstraction;

public interface IPurchaseService
{
    IQueryable<Purchase> GetAllPurchases();

    Purchase GetPurchaseById(Guid id);

    IEnumerable<Purchase> GetPurchasesByCustomerId(Guid customerId);

    Purchase CreatePurchase(Purchase purchase);

    Purchase UpdatePurchase(Purchase purchase);

    void DeletePurchase(Guid id);
}