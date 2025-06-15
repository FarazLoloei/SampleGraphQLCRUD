using SampleGraphQLCRUD.API.Abstraction;
using SampleGraphQLCRUD.API.GraphQL.Inputs;
using SampleGraphQLCRUD.API.Models;

namespace SampleGraphQLCRUD.API.GraphQL.Mutations;

public class PurchaseMutation
{
    public Purchase CreatePurchase(
        PurchaseInput input,
        [Service] IPurchaseService service)
    {
        var purchase = new Purchase(
            input.ProductName,
            input.Price,
            input.Quantity,
            input.CustomerId
        );
        return service.CreatePurchase(purchase);
    }

    public Purchase UpdatePurchase(
        Guid id,
        PurchaseInput input,
        [Service] IPurchaseService service)
    {
        var purchase = service.GetPurchaseById(id);

        purchase.UpdateProductDetails(purchase.ProductName, input.Price, input.Quantity);

        return service.UpdatePurchase(purchase);
    }

    public void DeletePurchase(Guid id, [Service] IPurchaseService service)
        => service.DeletePurchase(id);
}