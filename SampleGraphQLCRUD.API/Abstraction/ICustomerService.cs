using SampleGraphQLCRUD.API.Models;

namespace SampleGraphQLCRUD.API.Abstraction;

public interface ICustomerService
{
    IQueryable<Customer> GetAllCustomers();

    Customer GetCustomerById(int id);

    Customer CreateCustomer(Customer customer);

    Customer UpdateCustomer(Customer customer);

    bool DeleteCustomer(int id);
}