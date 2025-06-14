namespace SampleGraphQLCRUD.Tests.XUnit.TestCases;

public static class PurchaseTestCases
{
    public static IEnumerable<object[]> InvalidConstructorParameters => new List<object[]>
    {
            // Product name validation
             new object[] { null, 10.99m, 1, 1, "Product name is required" },
             new object[] { "", 10.99m, 1, 1, "Product name is required" },
             new object[] { " ", 10.99m, 1, 1, "Product name cannot be whitespace" },
             new object[] { new string('a', 101), 10.99m, 1, 1, "Product name cannot exceed 100 characters" },

            // Price validation
             new object[] { "Product", 0m, 1, 1, "Price must be at least 0.01" },
             new object[] { "Product", -1m, 1, 1, "Price must be at least 0.01" },

            // Quantity validation
             new object[] { "Product", 10.99m, 0, 1, "Quantity must be at least 1" },
             new object[] { "Product", 10.99m, -1, 1, "Quantity must be at least 1" },

            // Customer ID validation
             new object[] { "Product", 10.99m, 1, 0, "Customer ID must be positive" },
             new object[] { "Product", 10.99m, 1, -1, "Customer ID must be positive" }
        };

    //public static IEnumerable<object[]> InvalidProductDetailsParameters
    //{
    //    get
    //    {
    //        // Product name validation
    //        yield return new object[] { null, 10.99m, 1, 1, "Product name is required" };
    //        yield return new object[] { "", 10.99m, 1, 1, "Product name is required" };
    //        yield return new object[] { " ", 10.99m, 1, 1, "Product name cannot be whitespace" };
    //        yield return new object[] { new string('a', 101), 10.99m, 1, 1, "Product name cannot exceed 100 characters" };

    //        // Price validation
    //        yield return new object[] { "Product", 0m, 1, "Price must be at least 0.01" };

    //        // Quantity validation
    //        yield return new object[] { "Product", 10.99m, 0, "Quantity must be at least 1" };
    //    }
    //}

    public static IEnumerable<object[]> InvalidProductDetailsParameters => new List<object[]>
{
    new object[] { " ", 10.99m, 1, "Product name cannot be whitespace" },
    new object[] { "", 10.99m, 1, "Product name is required" },
    new object[] { null, 10.99m, 1, "Product name is required" },
    new object[] { new string('a', 101), 10.99m, 1, "Product name cannot exceed 100 characters" },
    new object[] { "Valid", 0m, 1, "Price must be at least 0.01" },
    new object[] { "Valid", 10.99m, 0, "Quantity must be at least 1" }
};
}