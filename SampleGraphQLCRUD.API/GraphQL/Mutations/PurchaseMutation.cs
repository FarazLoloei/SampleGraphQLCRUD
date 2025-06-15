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
        int id,
        PurchaseInput input,
        [Service] IPurchaseService service)
    {
        throw new NotImplementedException("UpdatePurchase method is not implemented yet.");
        var purchase = service.GetPurchaseById(id);
        //purchase.UpdateDetails(
        //    input.ProductName,
        //    input.Price,
        //    input.Quantity,
        //    input.CustomerId
        //);
        return service.UpdatePurchase(purchase);
    }

    public bool DeletePurchase(int id, [Service] IPurchaseService service)
        => service.DeletePurchase(id);
}