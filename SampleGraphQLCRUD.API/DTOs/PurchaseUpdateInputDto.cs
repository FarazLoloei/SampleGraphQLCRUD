using System.ComponentModel.DataAnnotations;

namespace SampleGraphQLCRUD.API.DTOs;

public record PurchaseUpdateInputDto : PurchaseInputDto
{
    [Required]
    public int Id { get; init; }

    // Primary constructor
    public PurchaseUpdateInputDto(
        int id,
        int customerId,
        string productName,
        decimal price,
        int quantity)
        : base(customerId, productName, price, quantity)
    {
        Id = id;
    }

    // Parameterless constructor for serialization
    public PurchaseUpdateInputDto() : this(0, 0, string.Empty, 0m, 0) { }

    // Copy constructor from base
    public PurchaseUpdateInputDto(int id, PurchaseInputDto baseDto)
        : this(id, baseDto.CustomerId, baseDto.ProductName, baseDto.Price, baseDto.Quantity) { }
}