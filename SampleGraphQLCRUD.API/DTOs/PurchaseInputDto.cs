using System.ComponentModel.DataAnnotations;

namespace SampleGraphQLCRUD.API.DTOs;

public record PurchaseInputDto
{
    [Required]
    public int CustomerId { get; init; }

    [Required, MaxLength(100)]
    public string ProductName { get; init; } = string.Empty;

    [Range(0.0, double.MaxValue)]
    public decimal Price { get; init; }

    [Range(1, int.MaxValue)]
    public int Quantity { get; init; }

    // Primary constructor
    public PurchaseInputDto(
        int customerId,
        string productName,
        decimal price,
        int quantity)
    {
        CustomerId = customerId;
        ProductName = productName;
        Price = price;
        Quantity = quantity;
    }

    // Parameterless constructor for serialization
    public PurchaseInputDto() : this(0, string.Empty, 0m, 0) { }
}