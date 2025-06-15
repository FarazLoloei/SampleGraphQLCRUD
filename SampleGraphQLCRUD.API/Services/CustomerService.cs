using SampleGraphQLCRUD.API.Abstraction;
using SampleGraphQLCRUD.API.Data;
using SampleGraphQLCRUD.API.Models;

namespace SampleGraphQLCRUD.API.Services;

public class CustomerService(ApplicationDbContext dbContext) : ICustomerService
{
    public IQueryable<Customer> GetAllCustomers()
        => dbContext.Customers.AsQueryable();

    public Customer GetCustomerById(Guid id)
    {
        var customer = dbContext.Customers.FirstOrDefault(c => c.Id == id);
        if (customer == null)
            throw new InvalidOperationException($"Customer with ID {id} not found.");
        return customer;
    }

    public IEnumerable<Purchase> GetPurchasesByCustomerId(Guid customerId)
        => dbContext.Purchases.Where(p => p.CustomerId == customerId).ToList();

    public Customer CreateCustomer(Customer customer)
    {
        dbContext.Customers.Add(customer);
        dbContext.SaveChanges();
        return customer;
    }

    public Customer UpdateCustomer(Customer customer)
    {
        var existing = dbContext.Customers.Find(customer.Id);
        if (existing == null)
            throw new InvalidOperationException("Customer not found");

        dbContext.Entry(existing).CurrentValues.SetValues(customer);
        dbContext.SaveChanges();
        return existing;
    }

    public void DeleteCustomer(Guid id)
    {
        var customer = dbContext.Customers.Find(id);
        if (customer == null) throw new InvalidOperationException($"Customer with ID {id} not found.");

        dbContext.Customers.Remove(customer);
        dbContext.SaveChanges();
    }
}