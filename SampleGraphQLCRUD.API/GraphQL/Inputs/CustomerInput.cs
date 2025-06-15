namespace SampleGraphQLCRUD.API.GraphQL.Inputs;

public class CustomerInput
{
    [GraphQLNonNullType]
    public string FirstName { get; set; } = string.Empty;

    [GraphQLNonNullType]
    public string LastName { get; set; } = string.Empty;

    [GraphQLNonNullType]
    public string Email { get; set; } = string.Empty;

    public string? Phone { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? Country { get; set; }
}