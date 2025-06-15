namespace SampleGraphQLCRUD.API.GraphQL.Inputs;

public class PurchaseInput
{
    [GraphQLNonNullType]
    public string ProductName { get; set; } = string.Empty;

    [GraphQLNonNullType]
    public decimal Price { get; set; }

    [GraphQLNonNullType]
    public int Quantity { get; set; }

    [GraphQLNonNullType]
    public Guid CustomerId { get; set; }
}