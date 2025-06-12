using System.ComponentModel.DataAnnotations;

namespace SampleGraphQLCRUD.API.DTOs;

public record CustomerInputDto
{
    // Properties with validation attributes
    [Required, MaxLength(50)]
    public string FirstName { get; set; }

    [Required, MaxLength(50)]
    public string LastName { get; set; }

    [Required, EmailAddress]
    public string Email { get; set; }

    [Phone]
    public string? Phone { get; set; }

    [MaxLength(100)]
    public string? Address { get; set; }

    [MaxLength(50)]
    public string? City { get; set; }

    [MaxLength(50)]
    public string? Country { get; set; }

    // Primary constructor
    public CustomerInputDto(
        string firstName,
        string lastName,
        string email,
        string? phone = null,
        string? address = null,
        string? city = null,
        string? country = null)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Phone = phone;
        Address = address;
        City = city;
        Country = country;
    }

    // Parameterless constructor for serialization
    public CustomerInputDto() : this(
        firstName: string.Empty,
        lastName: string.Empty,
        email: string.Empty)
    {
    }
}