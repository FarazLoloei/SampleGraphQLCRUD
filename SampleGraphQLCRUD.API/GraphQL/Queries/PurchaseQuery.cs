using SampleGraphQLCRUD.API.Abstraction;
using SampleGraphQLCRUD.API.Models;

namespace SampleGraphQLCRUD.API.GraphQL.Queries;

public class PurchaseQuery
{
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Purchase> GetPurchases([Service] IPurchaseService service)
        => service.GetAllPurchases();

    [UseFirstOrDefault]
    public Purchase? GetPurchaseById(Guid id, [Service] IPurchaseService service)
        => service.GetPurchaseById(id);
}