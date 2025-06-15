using SampleGraphQLCRUD.API.Models;

namespace SampleGraphQLCRUD.API.Abstraction;

public interface ICustomerService
{
    IQueryable<Customer> GetAllCustomers();

    Customer GetCustomerById(Guid id);

    Customer CreateCustomer(Customer customer);

    Customer UpdateCustomer(Customer customer);

    void DeleteCustomer(Guid id);

}