using System.ComponentModel.DataAnnotations;

namespace SampleGraphQLCRUD.API.Models;

public class Customer
{
    public Guid Id { get; private set; }

    [Required(ErrorMessage = "First name is required")]
    [MaxLength(50, ErrorMessage = "First name cannot exceed 50 characters")]
    public string FirstName { get; private set; }

    [Required(ErrorMessage = "Last name is required")]
    [MaxLength(50, ErrorMessage = "Last name cannot exceed 50 characters")]
    public string LastName { get; private set; }

    [Required(ErrorMessage = "Email is required")]
    [MaxLength(256, ErrorMessage = "Email cannot exceed 256 characters")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string Email { get; private set; }

    [Phone(ErrorMessage = "Invalid phone number format")]
    public string? Phone { get; private set; }

    [MaxLength(100, ErrorMessage = "Address exceeds max length")]
    public string? Address { get; private set; }

    [MaxLength(50, ErrorMessage = "City exceeds max length")]
    public string? City { get; private set; }

    [MaxLength(50, ErrorMessage = "Country exceeds max length")]
    public string? Country { get; private set; }

    public DateTime CreatedAtUTC { get; private set; } = DateTime.UtcNow;

    public ICollection<Purchase> Purchases { get; private set; }

    // Constructor for required properties
    public Customer(string firstName, string lastName, string email)
    {
        Id = Guid.NewGuid();
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Purchases = new List<Purchase>();

        //Validate using data annotations
        ValidateModel();
    }

    // Private constructor for EF Core
    private Customer()
    {
        // Required by EF Core
        Purchases = new List<Purchase>();
    }

    #region UpdateMethods

    public void UpdateName(string firstName, string lastName)
    {
        ValidateName(firstName, lastName);

        FirstName = firstName;
        LastName = lastName;
    }

    public void UpdateContactInfo(string? phone, string email)
    {
        ValidateContactInfo(phone, email);
        Phone = phone;
        Email = email;
    }

    public void UpdateAddress(string? address, string? city, string? country)
    {
        ValidateAddressFields(address, city, country);

        Address = address;
        City = city;
        Country = country;
    }

    public void AddPurchase(Purchase purchase)
    {
        if (purchase == null)
        {
            throw new ArgumentNullException(nameof(purchase));
        }
        Purchases.Add(purchase);
    }

    #endregion UpdateMethods

    #region ValidationMethods

    private void ValidateModel()
    {
        var context = new ValidationContext(this);
        var results = new List<ValidationResult>();

        if (!Validator.TryValidateObject(this, context, results, true))
        {
            // For constructor validation, throw the first error
            throw new ValidationException(results.First().ErrorMessage);

            // For update methods, you might want to collect all errors
            // throw new ValidationException(string.Join(", ", results.Select(r => r.ErrorMessage)));
        }
    }

    private void ValidateName(string firstName, string lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ValidationException("First name is required");

        if (firstName.Length > 50)
            throw new ValidationException("First name cannot exceed 50 characters");

        if (string.IsNullOrWhiteSpace(lastName))
            throw new ValidationException("Last name is required");

        if (lastName.Length > 50)
            throw new ValidationException("Last name cannot exceed 50 characters");
    }

    private void ValidateContactInfo(string? phone, string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ValidationException("Email is required");

        if (!new EmailAddressAttribute().IsValid(email))
            throw new ValidationException("Invalid email format");

        if (email.Length > 256) // Standard max length for emails
            throw new ValidationException("Email cannot exceed 256 characters");

        if (phone != null && !string.IsNullOrWhiteSpace(phone))
        {
            if (!new PhoneAttribute().IsValid(phone))
                throw new ValidationException("Invalid phone number format");

            if (phone.Length > 20) // Reasonable max length for phone numbers
                throw new ValidationException("Phone number cannot exceed 20 characters");
        }
    }

    private void ValidateAddressFields(string? address, string? city, string? country)
    {
        if (address != null && address.Length > 100)
        {
            throw new ValidationException("Address exceeds max length");
        }

        if (city != null && city.Length > 50)
        {
            throw new ValidationException("City exceeds max length");
        }

        if (country != null && country.Length > 50)
        {
            throw new ValidationException("Country exceeds max length");
        }
    }

    #endregion ValidationMethods
}