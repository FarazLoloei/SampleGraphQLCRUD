namespace SampleGraphQLCRUD.Tests.XUnit.TestCases;

public static class PurchaseTestCases
{
    public static IEnumerable<object[]> InvalidConstructorParameters
    {
        get
        {
            // Product name validation cases
            yield return new object[] { null, 100.00m, 1, 1, "Null product name" };
            yield return new object[] { "", 100.00m, 1, 1, "Empty product name" };
            yield return new object[] { " ", 100.00m, 1, 1, "Whitespace product name" };
            yield return new object[] {
                    "This product name is way too long and exceeds the maximum allowed length of 100 characters for the product name field in the purchase entity",
                    100.00m, 1, 1,
                    "Product name exceeds max length"
                };

            // Price validation cases
            yield return new object[] { "Product", -1.00m, 1, 1, "Negative price" };
            yield return new object[] { "Product", -0.01m, 1, 1, "Fractional negative price" };

            // Quantity validation cases
            yield return new object[] { "Product", 100.00m, 0, 1, "Zero quantity" };
            yield return new object[] { "Product", 100.00m, -1, 1, "Negative quantity" };

            // Customer ID validation cases
            yield return new object[] { "Product", 100.00m, 1, 0, "Zero customer ID" };
            yield return new object[] { "Product", 100.00m, 1, -1, "Negative customer ID" };
        }
    }

    public static IEnumerable<object[]> InvalidProductDetailsParameters
    {
        get
        {
            // Product name validation cases
            yield return new object[] { null, 100.00m, 1, "Null product name" };
            yield return new object[] { "", 100.00m, 1, "Empty product name" };
            yield return new object[] { " ", 100.00m, 1, "Whitespace product name" };
            yield return new object[] {
                    "This product name is way too long and exceeds the maximum allowed length of 100 characters for the product name field in the purchase entity",
                    100.00m, 1,
                    "Product name exceeds max length"
                };

            // Price validation cases
            yield return new object[] { "Product", -1.00m, 1, "Negative price" };
            yield return new object[] { "Product", -0.01m, 1, "Fractional negative price" };

            // Quantity validation cases
            yield return new object[] { "Product", 100.00m, 0, "Zero quantity" };
            yield return new object[] { "Product", 100.00m, -1, "Negative quantity" };
        }
    }
}