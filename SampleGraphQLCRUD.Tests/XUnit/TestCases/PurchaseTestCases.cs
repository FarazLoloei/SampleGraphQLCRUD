namespace SampleGraphQLCRUD.Tests.XUnit.TestCases;

public static class PurchaseTestCases
{
    // Constants for validation rules
    private const int MaxProductNameLength = 100;

    // Helper method for generating long strings
    private static string LongString(int length) => new string('a', length);

    public static IEnumerable<object[]> InvalidConstructorParameters => new List<object[]>
    {
        // Product name validation
        CreateTestCase(null, 10.99m, 1, 1, "Product name is required"),
        CreateTestCase("", 10.99m, 1, 1, "Product name is required"),
        CreateTestCase(" ", 10.99m, 1, 1, "Product name cannot be whitespace"),
        CreateTestCase(LongString(MaxProductNameLength + 1), 10.99m, 1, 1, "Product name cannot exceed 100 characters"),

        // Price validation
        CreateTestCase("Product", 0m, 1, 1, "Price must be at least 0.01"),
        CreateTestCase("Product", -1m, 1, 1, "Price must be at least 0.01"),

        // Quantity validation
        CreateTestCase("Product", 10.99m, 0, 1, "Quantity must be at least 1"),
        CreateTestCase("Product", 10.99m, -1, 1, "Quantity must be at least 1"),

        // Customer ID validation
        CreateTestCase("Product", 10.99m, 1, 0, "Customer ID must be positive"),
        CreateTestCase("Product", 10.99m, 1, -1, "Customer ID must be positive")
    };

    public static IEnumerable<object[]> InvalidProductDetailsParameters => new List<object[]>
    {
        // Product name validation
        CreateProductDetailsTestCase(" ", 10.99m, 1, "Product name cannot be whitespace"),
        CreateProductDetailsTestCase("", 10.99m, 1, "Product name is required"),
        CreateProductDetailsTestCase(null, 10.99m, 1, "Product name is required"),
        CreateProductDetailsTestCase(LongString(MaxProductNameLength + 1), 10.99m, 1, "Product name cannot exceed 100 characters"),

        // Price and quantity validation
        CreateProductDetailsTestCase("Valid", 0m, 1, "Price must be at least 0.01"),
        CreateProductDetailsTestCase("Valid", 10.99m, 0, "Quantity must be at least 1")
    };

    // Helper methods for creating test cases
    private static object[] CreateTestCase(
        string productName, decimal price, int quantity, int customerId, string expectedError)
        => new object[] { productName, price, quantity, customerId, expectedError };

    private static object[] CreateProductDetailsTestCase(
        string productName, decimal price, int quantity, string expectedError)
        => new object[] { productName, price, quantity, expectedError };
}