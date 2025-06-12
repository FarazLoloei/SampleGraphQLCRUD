using System.ComponentModel.DataAnnotations;

namespace SampleGraphQLCRUD.API.DTOs;

public record CustomerUpdateInputDto : CustomerInputDto
{
    [Required]
    public int Id { get; set; }

    // Primary constructor
    public CustomerUpdateInputDto(
        int id,
        string firstName,
        string lastName,
        string email,
        string? phone = null,
        string? address = null,
        string? city = null,
        string? country = null)
        : base(firstName, lastName, email, phone, address, city, country)
    {
        Id = id;
    }

    // Parameterless constructor for serialization
    public CustomerUpdateInputDto() : this(
        id: 0,
        firstName: string.Empty,
        lastName: string.Empty,
        email: string.Empty)
    {
    }

    // Copy constructor from base
    public CustomerUpdateInputDto(int id, CustomerInputDto baseDto)
        : this(
            id,
            baseDto.FirstName,
            baseDto.LastName,
            baseDto.Email,
            baseDto.Phone,
            baseDto.Address,
            baseDto.City,
            baseDto.Country)
    {
    }
}