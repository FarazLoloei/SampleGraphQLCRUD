using System.ComponentModel.DataAnnotations;

namespace SampleGraphQLCRUD.API.Models;

public class Purchase
{
    public int Id { get; private set; }

    [Required, MaxLength(100)]
    public string ProductName { get; private set; } = string.Empty;

    [Range(0.0, double.MaxValue)]
    public decimal Price { get; private set; }

    [Range(1, int.MaxValue)]
    public int Quantity { get; private set; }

    public DateTime PurchaseDateUTC { get; private set; } = DateTime.UtcNow;

    public int CustomerId { get; private set; }

    public Customer? Customer { get; private set; }

    // Constructor for required properties
    public Purchase(string productName, decimal price, int quantity, int customerId)
    {
        ProductName = productName;
        Price = price;
        Quantity = quantity;
        CustomerId = customerId;
    }

    // Private constructor for EF Core
    private Purchase()
    {
        // Required by EF Core
    }

    // Update methods
    public void UpdateProductDetails(string productName, decimal price, int quantity)
    {
        ProductName = productName;
        Price = price;
        Quantity = quantity;
    }

    public void UpdateCustomerAssociation(int customerId, Customer customer)
    {
        CustomerId = customerId;
        Customer = customer;
    }
}