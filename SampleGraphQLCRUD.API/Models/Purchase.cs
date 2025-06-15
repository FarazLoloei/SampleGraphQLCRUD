using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SampleGraphQLCRUD.API.Models;

public class Purchase
{
    public Guid Id { get; private set; }

    [Required(ErrorMessage = "Product name is required")]
    [MaxLength(100, ErrorMessage = "Product name cannot exceed 100 characters")]
    [RegularExpression(@"^(?!\s*$).+", ErrorMessage = "Product name cannot be whitespace")]
    public string ProductName { get; private set; } = string.Empty;

    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be at least 0.01")]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; private set; }

    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
    public int Quantity { get; private set; }

    [Required]
    public DateTime PurchaseDateUTC { get; private set; } = DateTime.UtcNow;

    [Required(ErrorMessage = "Customer ID is required")]
    public Guid CustomerId { get; private set; }

    [ForeignKey("CustomerId")]
    public Customer? Customer { get; private set; }

    public Purchase(string productName, decimal price, int quantity, Guid customerId)
    {
        // Validate product details first
        if (string.IsNullOrEmpty(productName))
            throw new ValidationException("Product name is required");

        if (string.IsNullOrWhiteSpace(productName))
            throw new ValidationException("Product name cannot be whitespace");

        // Then validate other parameters
        ValidateProductDetails(productName, price, quantity);
        ValidateCustomerId(customerId);

        // Assign values
        Id = Guid.NewGuid();
        ProductName = productName;
        Price = price;
        Quantity = quantity;
        CustomerId = customerId;
    }

    private Purchase()
    {
        // Required by EF Core
    }

    public void UpdateProductDetails(string productName, decimal price, int quantity)
    {
        ValidateProductDetails(productName, price, quantity);

        ProductName = productName;
        Price = price;
        Quantity = quantity;
    }

    public void UpdateCustomerAssociation(Guid customerId, Customer customer)
    {
        ValidateCustomerId(customerId);
        ValidateCustomer(customer);

        CustomerId = customerId;
        Customer = customer;
    }

    #region Validation Methods

    private void ValidateProductDetails(string productName, decimal price, int quantity)
    {
        if (string.IsNullOrEmpty(productName))
            throw new ValidationException("Product name is required");

        if (string.IsNullOrWhiteSpace(productName))
            throw new ValidationException("Product name cannot be whitespace");

        if (productName.Length > 100)
            throw new ValidationException("Product name cannot exceed 100 characters");

        if (price < 0.01m)
            throw new ValidationException("Price must be at least 0.01");

        if (quantity < 1)
            throw new ValidationException("Quantity must be at least 1");
    }

    private void ValidateCustomerId(Guid customerId)
    {
        if (customerId == Guid.Empty)
            throw new ValidationException("Customer ID must be a valid non-empty GUID");
    }

    private void ValidateCustomer(Customer customer)
    {
        if (customer == null)
            throw new ArgumentNullException(nameof(customer), "Customer cannot be null");
    }

    public void ValidateModel()
    {
        var context = new ValidationContext(this);
        var results = new List<ValidationResult>();

        if (!Validator.TryValidateObject(this, context, results, true))
        {
            throw new ValidationException(string.Join(", ", results.Select(r => r.ErrorMessage)));
        }
    }

    #endregion Validation Methods
}