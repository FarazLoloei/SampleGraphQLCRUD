using SampleGraphQLCRUD.API.Abstraction;
using SampleGraphQLCRUD.API.GraphQL.Inputs;
using SampleGraphQLCRUD.API.Models;

namespace SampleGraphQLCRUD.API.GraphQL.Mutations;

public class CustomerMutation
{
    public Customer CreateCustomer(
       CustomerInput input,
       [Service] ICustomerService service)
    {
        throw new NotImplementedException("CreateCustomer method is not implemented yet.");
        var customer = new Customer(
            input.FirstName,
            input.LastName,
            input.Email
        );
        //{
        //    Phone = input.Phone,
        //    Address = input.Address,
        //    City = input.City,
        //    Country = input.Country
        //};

        return service.CreateCustomer(customer);
    }

    public Customer UpdateCustomer(
        Guid id,
        CustomerInput input,
        [Service] ICustomerService service)
    {
        throw new NotImplementedException("UpdateCustomer method is not implemented yet.");
        var customer = service.GetCustomerById(id);
        //customer.UpdateDetails(
        //    input.FirstName,
        //    input.LastName,
        //    input.Email,
        //    input.Phone,
        //    input.Address,
        //    input.City,
        //    input.Country
        //);
        return service.UpdateCustomer(customer);
    }

    public void DeleteCustomer(Guid id, [Service] ICustomerService service)
        => service.DeleteCustomer(id);
}