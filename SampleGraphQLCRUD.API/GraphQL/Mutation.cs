using SampleGraphQLCRUD.API.Data;
using SampleGraphQLCRUD.API.DTOs;
using SampleGraphQLCRUD.API.Models;

namespace SampleGraphQLCRUD.API.GraphQL;

public class Mutation
{
    private readonly ApplicationDbContext dbContext;

    public Mutation(ApplicationDbContext applicationDbContext)
    {
        dbContext = applicationDbContext;
    }

    public async Task<Customer> CreateCustomer(CustomerInputDto input)
    {
        var customer = new Customer
        (
            firstName: input.FirstName,
            lastName: input.LastName,
            email: input.Email
        );

        dbContext.Customers.Add(customer);
        await dbContext.SaveChangesAsync();
        return customer;
    }

    public async Task<Purchase> CreatePurchase(PurchaseInputDto input)
    {
        var purchase = new Purchase
        (
            customerId: input.CustomerId,
            productName: input.ProductName,
            price: input.Price,
            quantity: input.Quantity
        );

        dbContext.Purchases.Add(purchase);
        await dbContext.SaveChangesAsync();
        return purchase;
    }

    // Similarly Add/Update/Delete for Purchase...
}