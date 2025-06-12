using SampleGraphQLCRUD.API.Models;

namespace SampleGraphQLCRUD.API.GraphQL.Types;

public class PurchaseType : ObjectType<Purchase>
{
    protected override void Configure(IObjectTypeDescriptor<Purchase> descriptor)
    {
        descriptor.Description("Represents a purchase made by a customer.");

        descriptor
            .Field(p => p.Id)
            .Description("The unique identifier of the purchase.");

        descriptor
            .Field(p => p.ProductName)
            .Description("The name of the product purchased.");

        descriptor
            .Field(p => p.Price)
            .Description("The price per unit of the product.");

        descriptor
            .Field(p => p.Quantity)
            .Description("The quantity of the product purchased.");

        descriptor
            .Field(p => p.PurchaseDateUTC)
            .Description("The date and time when the purchase was made (in UTC).");

        descriptor
            .Field(p => p.CustomerId)
            .Description("The ID of the customer who made the purchase.");

        descriptor
            .Field(p => p.Customer)
            .Description("The customer who made this purchase.");
    }
}