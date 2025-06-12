using SampleGraphQLCRUD.API.Models;

namespace SampleGraphQLCRUD.API.GraphQL.Types;

public class CustomerType : ObjectType<Customer>
{
    protected override void Configure(IObjectTypeDescriptor<Customer> descriptor)
    {
        descriptor.Description("Represents a customer who can make purchases.");

        descriptor
            .Field(c => c.Id)
            .Description("The unique identifier of the customer.");

        descriptor
            .Field(c => c.FirstName)
            .Description("The customer's first name.");

        descriptor
            .Field(c => c.LastName)
            .Description("The customer's last name.");

        descriptor
            .Field(c => c.Email)
            .Description("The customer's email address.");

        descriptor
            .Field(c => c.Phone)
            .Description("The customer's phone number.");

        descriptor
            .Field(c => c.Address)
            .Description("The customer's street address.");

        descriptor
            .Field(c => c.City)
            .Description("The city the customer lives in.");

        descriptor
            .Field(c => c.Country)
            .Description("The country the customer resides in.");

        descriptor
            .Field(c => c.CreatedAtUTC)
            .Description("The date and time when the customer was created (UTC).");

        descriptor
            .Field(c => c.Purchases)
            .Description("The list of purchases made by this customer.");
    }
}