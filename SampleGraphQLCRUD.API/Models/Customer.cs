using System.ComponentModel.DataAnnotations;

namespace SampleGraphQLCRUD.API.Models;

public class Customer
{
    public int Id { get; private set; }

    [Required, MaxLength(50)]
    public string FirstName { get; private set; } = string.Empty;

    [Required, MaxLength(50)]
    public string LastName { get; private set; } = string.Empty;

    [Required, EmailAddress]
    public string Email { get; private set; } = string.Empty;

    [Phone]
    public string? Phone { get; private set; }

    [MaxLength(100)]
    public string? Address { get; private set; }

    [MaxLength(50)]
    public string? City { get; private set; }

    [MaxLength(50)]
    public string? Country { get; private set; }

    public DateTime CreatedAtUTC { get; private set; } = DateTime.UtcNow;

    public ICollection<Purchase> Purchases { get; private set; }

    // Constructor for required properties
    public Customer(string firstName, string lastName, string email)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Purchases = new List<Purchase>();
    }

    // Private constructor for EF Core
    private Customer()
    {
        // Required by EF Core
        Purchases = new List<Purchase>();
    }

    // Update methods
    public void UpdateName(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    public void UpdateContactInfo(string? phone, string email)
    {
        Phone = phone;
        Email = email;
    }

    public void UpdateAddress(string? address, string? city, string? country)
    {
        Address = address;
        City = city;
        Country = country;
    }

    public void AddPurchase(Purchase purchase)
    {
        Purchases.Add(purchase);
    }
}