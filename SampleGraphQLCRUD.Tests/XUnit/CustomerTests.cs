using FluentAssertions;
using SampleGraphQLCRUD.API.Models;
using SampleGraphQLCRUD.Tests.XUnit.TestCases;
using System.ComponentModel.DataAnnotations;

namespace SampleGraphQLCRUD.Tests.XUnit;

public class CustomerTests
{
    [Fact]
    public void Constructor_WithValidParameters_InitializesProperties()
    {
        // Arrange
        var firstName = "John";
        var lastName = "Doe";
        var email = "john.doe@example.com";

        // Act
        var customer = new Customer(firstName, lastName, email);

        // Assert
        customer.FirstName.Should().Be(firstName);
        customer.LastName.Should().Be(lastName);
        customer.Email.Should().Be(email);
        customer.Phone.Should().BeNull();
        customer.Address.Should().BeNull();
        customer.City.Should().BeNull();
        customer.Country.Should().BeNull();
        customer.CreatedAtUTC.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        customer.Purchases.Should().BeEmpty();
    }

    [Theory]
    [MemberData(nameof(CustomerTestCases.InvalidConstructorParameters), MemberType = typeof(CustomerTestCases))]
    public void Constructor_WithInvalidParameters_ThrowsValidationException(
        string firstName, string lastName, string email, string expectedErrorMessage)
    {
        // Act
        Action act = () => new Customer(firstName, lastName, email);

        // Assert
        act.Should()
            .Throw<ValidationException>()
            .WithMessage(expectedErrorMessage);
    }

    [Theory]
    [MemberData(nameof(CustomerTestCases.ValidEmailLengthParameters), MemberType = typeof(CustomerTestCases))]
    public void Constructor_WithValidEmailLength_DoesNotThrow(string email)
    {
        // Act
        Action act = () => new Customer("John", "Doe", email);

        // Assert
        act.Should().NotThrow();
    }

    [Fact]
    public void UpdateName_WithValidParameters_UpdatesProperties()
    {
        // Arrange
        var customer = new Customer("John", "Doe", "john.doe@example.com");
        var newFirstName = "Jane";
        var newLastName = "Smith";

        // Act
        customer.UpdateName(newFirstName, newLastName);

        // Assert
        customer.FirstName.Should().Be(newFirstName);
        customer.LastName.Should().Be(newLastName);
    }

    [Theory]
    [MemberData(nameof(CustomerTestCases.InvalidNameUpdateParameters), MemberType = typeof(CustomerTestCases))]
    public void UpdateName_WithInvalidParameters_ThrowsValidationException(
        string firstName, string lastName, string expectedErrorMessage)
    {
        // Arrange
        var customer = new Customer("John", "Doe", "john.doe@example.com");

        // Act
        Action act = () => customer.UpdateName(firstName, lastName);

        // Assert
        act.Should()
            .Throw<ValidationException>()
            .WithMessage(expectedErrorMessage);
    }

    [Fact]
    public void UpdateContactInfo_WithValidParameters_UpdatesProperties()
    {
        // Arrange
        var customer = new Customer("John", "Doe", "john.doe@example.com");
        var newPhone = "+1234567890";
        var newEmail = "new.email@example.com";

        // Act
        customer.UpdateContactInfo(newPhone, newEmail);

        // Assert
        customer.Phone.Should().Be(newPhone);
        customer.Email.Should().Be(newEmail);
    }

    [Fact]
    public void UpdateContactInfo_WithNullPhone_UpdatesProperties()
    {
        // Arrange
        var customer = new Customer("John", "Doe", "john.doe@example.com");
        customer.UpdateContactInfo("+1234567890", "john.doe@example.com");

        var newEmail = "new.email@example.com";

        // Act
        customer.UpdateContactInfo(null, newEmail);

        // Assert
        customer.Phone.Should().BeNull();
        customer.Email.Should().Be(newEmail);
    }

    [Theory]
    [MemberData(nameof(CustomerTestCases.InvalidContactInfoParameters), MemberType = typeof(CustomerTestCases))]
    public void UpdateContactInfo_WithInvalidParameters_ThrowsValidationException(
        string phone, string email, string expectedErrorMessage)
    {
        // Arrange
        var customer = new Customer("John", "Doe", "john.doe@example.com");

        // Act
        Action act = () => customer.UpdateContactInfo(phone, email);

        // Assert
        act.Should()
            .Throw<ValidationException>()
            .WithMessage(expectedErrorMessage);
    }

    [Fact]
    public void UpdateAddress_WithValidParameters_UpdatesProperties()
    {
        // Arrange
        var customer = new Customer("John", "Doe", "john.doe@example.com");
        var address = "123 Main St";
        var city = "Koblenz";
        var country = "Germany";

        // Act
        customer.UpdateAddress(address, city, country);

        // Assert
        customer.Address.Should().Be(address);
        customer.City.Should().Be(city);
        customer.Country.Should().Be(country);
    }

    [Fact]
    public void UpdateAddress_WithNullParameters_UpdatesProperties()
    {
        // Arrange
        var customer = new Customer("John", "Doe", "john.doe@example.com");
        customer.UpdateAddress("123 Main St", "Koblenz", "Germany");

        // Act
        customer.UpdateAddress(null, null, null);

        // Assert
        customer.Address.Should().BeNull();
        customer.City.Should().BeNull();
        customer.Country.Should().BeNull();
    }

    [Theory]
    [MemberData(nameof(CustomerTestCases.InvalidAddressParameters), MemberType = typeof(CustomerTestCases))]
    public void UpdateAddress_WithTooLongParameters_ThrowsValidationException(
        string address, string city, string country, string expectedErrorMessage)
    {
        // Arrange
        var customer = new Customer("John", "Doe", "john.doe@example.com");

        // Act
        Action act = () => customer.UpdateAddress(address, city, country);

        // Assert
        act.Should()
            .Throw<ValidationException>()
            .WithMessage(expectedErrorMessage);
    }

    [Fact]
    public void AddPurchase_WithValidPurchase_AddsToCollection()
    {
        // Arrange
        var customer = new Customer("John", "Doe", "john.doe@example.com");
        var purchase = new Purchase("Product", 100.00m, 1, 1);

        // Act
        customer.AddPurchase(purchase);

        // Assert
        customer.Purchases.Should()
            .Contain(purchase)
            .And.HaveCount(1);
    }

    [Fact]
    public void AddPurchase_WithNullPurchase_ThrowsArgumentNullException()
    {
        // Arrange
        var customer = new Customer("John", "Doe", "john.doe@example.com");

        // Act
        Action act = () => customer.AddPurchase(null!);

        // Assert
        act.Should()
            .Throw<ArgumentNullException>()
            .WithMessage("*Value cannot be null.*")
            .WithParameterName("purchase");
    }
}