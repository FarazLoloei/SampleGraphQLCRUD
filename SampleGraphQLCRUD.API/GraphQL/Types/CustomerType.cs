using SampleGraphQLCRUD.API.Models;

namespace SampleGraphQLCRUD.API.GraphQL.Types;

public class CustomerType : ObjectType<Customer>
{
    protected override void Configure(IObjectTypeDescriptor<Customer> descriptor)
    {
        descriptor.Description("Represents a customer who can make purchases.");

        descriptor
            .Field(c => c.Id)
            .Description("The unique identifier of the customer.")
            .Type<NonNullType<IdType>>();

        descriptor
            .Field(c => c.FirstName)
            .Description("The customer's first name.")
            .Type<NonNullType<StringType>>();

        descriptor
            .Field(c => c.LastName)
            .Description("The customer's last name.")
            .Type<NonNullType<StringType>>();

        descriptor
            .Field(c => c.Email)
            .Description("The customer's email address.")
            .Type<NonNullType<StringType>>();

        descriptor
            .Field(c => c.Phone)
            .Description("The customer's phone number.")
            .Type<StringType>();

        descriptor
            .Field(c => c.Address)
            .Description("The customer's street address.")
            .Type<StringType>();

        descriptor
            .Field(c => c.City)
            .Description("The city the customer lives in.")
            .Type<StringType>();

        descriptor
            .Field(c => c.Country)
            .Description("The country the customer resides in.")
            .Type<StringType>();

        descriptor
            .Field(c => c.CreatedAtUTC)
            .Description("The date and time when the customer was created (UTC).")
            .Type<NonNullType<DateTimeType>>();

        descriptor
            .Field(c => c.Purchases)
            .Description("The list of purchases made by this customer.")
            .Type<ListType<PurchaseType>>();
        //.ResolveWith<Resolvers>(r => r.GetPurchases(default!, default!));
    }

    //private class Resolvers
    //{
    //    public IEnumerable<Purchase> GetPurchases([Parent] Customer customer, [Service] IPurchaseService service)
    //        => service.GetPurchasesByCustomerId(customer.Id);
    //}
}