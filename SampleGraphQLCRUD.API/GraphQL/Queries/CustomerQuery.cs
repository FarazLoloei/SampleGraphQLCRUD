using SampleGraphQLCRUD.API.Abstraction;
using SampleGraphQLCRUD.API.Models;

namespace SampleGraphQLCRUD.API.GraphQL.Queries;

public class CustomerQuery
{
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Customer> GetCustomers([Service] ICustomerService service)
        => service.GetAllCustomers();

    [UseFirstOrDefault]
    public Customer? GetCustomerById(Guid id, [Service] ICustomerService service)
        => service.GetCustomerById(id);
}