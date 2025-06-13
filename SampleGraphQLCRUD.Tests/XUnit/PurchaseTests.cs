using FluentAssertions;
using SampleGraphQLCRUD.API.Models;
using SampleGraphQLCRUD.Tests.XUnit.TestCases;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace SampleGraphQLCRUD.Tests.XUnit;

public class PurchaseTests
{
    [Fact]
    public void Constructor_WithValidParameters_InitializesProperties()
    {
        // Arrange
        var productName = "Laptop";
        var price = 999.99m;
        var quantity = 1;
        var customerId = 1;

        // Act
        var purchase = new Purchase(productName, price, quantity, customerId);

        // Assert
        purchase.ProductName.Should().Be(productName);
        purchase.Price.Should().Be(price);
        purchase.Quantity.Should().Be(quantity);
        purchase.CustomerId.Should().Be(customerId);
        purchase.PurchaseDateUTC.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        purchase.Customer.Should().BeNull();
    }

    [Theory]
    [MemberData(nameof(PurchaseTestCases.InvalidConstructorParameters), MemberType = typeof(PurchaseTestCases))]
    public void Constructor_WithInvalidParameters_ThrowsValidationException(
        string productName, decimal price, int quantity, int customerId, string expectedErrorMessage)
    {
        // Act
        Action act = () => new Purchase(productName, price, quantity, customerId);

        // Assert
        act.Should()
            .Throw<ValidationException>()
            .WithMessage(expectedErrorMessage);
    }

    [Fact]
    public void UpdateProductDetails_WithValidParameters_UpdatesProperties()
    {
        // Arrange
        var purchase = new Purchase("Old Product", 50.00m, 2, 1);
        var newProductName = "New Product";
        var newPrice = 75.99m;
        var newQuantity = 3;

        // Act
        purchase.UpdateProductDetails(newProductName, newPrice, newQuantity);

        // Assert
        purchase.ProductName.Should().Be(newProductName);
        purchase.Price.Should().Be(newPrice);
        purchase.Quantity.Should().Be(newQuantity);
    }

    [Theory]
    [MemberData(nameof(PurchaseTestCases.InvalidProductDetailsParameters), MemberType = typeof(PurchaseTestCases))]
    public void UpdateProductDetails_WithInvalidParameters_ThrowsValidationException(
    string productName, decimal price, int quantity, string expectedErrorMessage)
    {
        // Arrange
        var purchase = new Purchase("Valid Product", 10.99m, 1, 1);

        // Act
        Action act = () => purchase.UpdateProductDetails(productName, price, quantity);

        // Assert
        act.Should()
            .Throw<ValidationException>()
            .WithMessage(expectedErrorMessage);
    }

    [Fact]
    public void UpdateCustomerAssociation_WithValidParameters_UpdatesProperties()
    {
        // Arrange
        var purchase = new Purchase("Product", 50.00m, 1, 1);
        var newCustomerId = 2;
        var newCustomer = new Customer("Jane", "Doe", "jane.doe@example.com");

        // Act
        purchase.UpdateCustomerAssociation(newCustomerId, newCustomer);

        // Assert
        purchase.CustomerId.Should().Be(newCustomerId);
        purchase.Customer.Should().Be(newCustomer);
    }

    [Fact]
    public void UpdateCustomerAssociation_WithInvalidCustomerId_ThrowsValidationException()
    {
        // Arrange
        var purchase = new Purchase("Product", 50.00m, 1, 1);
        var customer = new Customer("Jane", "Doe", "jane.doe@example.com");

        // Act
        Action act = () => purchase.UpdateCustomerAssociation(0, customer);

        // Assert
        act.Should().Throw<ValidationException>();
    }

    [Fact]
    public void UpdateCustomerAssociation_WithNullCustomer_ThrowsArgumentNullException()
    {
        // Arrange
        var purchase = new Purchase("Product", 50.00m, 1, 1);

        // Act
        Action act = () => purchase.UpdateCustomerAssociation(2, null!);

        // Assert
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void ProtectedConstructor_ForEfCore_InitializesWithDefaultValues()
    {
        // Arrange
        var constructor = typeof(Purchase).GetConstructor(
            BindingFlags.Instance | BindingFlags.NonPublic,
            null,
            Type.EmptyTypes,
            null)!;

        // Act
        var purchase = (Purchase)constructor.Invoke(null);

        // Assert
        purchase.ProductName.Should().BeEmpty();
        purchase.Price.Should().Be(0);
        purchase.Quantity.Should().Be(0);
        purchase.CustomerId.Should().Be(0);
        purchase.PurchaseDateUTC.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        purchase.Customer.Should().BeNull();
    }

    //[Fact]
    //public async Task EfCore_CanCreatePurchaseWithDefaultValues()
    //{
    //    // Arrange
    //    await using var context = new ApplicationDbContext()//(/* your options */);

    //    // Act
    //    var purchase = context.Purchases.Add(new Purchase(
    //        "Test Product",
    //        10.99m,
    //        1,
    //        1)).Entity;
    //    await context.SaveChangesAsync();

    //    var loadedPurchase = await context.Purchases.FindAsync(purchase.Id);

    //    // Assert
    //    loadedPurchase.Should().NotBeNull();
    //    // Verify default values if needed
    //}
}