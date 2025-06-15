using SampleGraphQLCRUD.API.Abstraction;
using SampleGraphQLCRUD.API.Data;
using SampleGraphQLCRUD.API.Models;

namespace SampleGraphQLCRUD.API.Services;

public class CustomerService(ApplicationDbContext _context) : ICustomerService
{
    public IQueryable<Customer> GetAllCustomers()
        => _context.Customers.AsQueryable();

    public Customer? GetCustomerById(int id)
        => _context.Customers.FirstOrDefault(c => c.Id == id);

    public IEnumerable<Purchase> GetPurchasesByCustomerId(int customerId)
        => _context.Purchases.Where(p => p.CustomerId == customerId).ToList();

    public Customer CreateCustomer(Customer customer)
    {
        _context.Customers.Add(customer);
        _context.SaveChanges();
        return customer;
    }

    public Customer UpdateCustomer(Customer customer)
    {
        var existing = _context.Customers.Find(customer.Id);
        if (existing == null)
            throw new InvalidOperationException("Customer not found");

        _context.Entry(existing).CurrentValues.SetValues(customer);
        _context.SaveChanges();
        return existing;
    }

    public bool DeleteCustomer(int id)
    {
        var customer = _context.Customers.Find(id);
        if (customer == null) return false;

        _context.Customers.Remove(customer);
        _context.SaveChanges();
        return true;
    }
}