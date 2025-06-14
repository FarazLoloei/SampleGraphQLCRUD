namespace SampleGraphQLCRUD.Tests.XUnit.TestCases;

public static class CustomerTestCases
{
    public static IEnumerable<object[]> InvalidConstructorParameters
    {
        get
        {
            // First name validation cases
            yield return new object[] { null, "Doe", "valid@email.com", "First name is required" };
            yield return new object[] { "", "Doe", "valid@email.com", "First name is required" };
            yield return new object[] { " ", "Doe", "valid@email.com", "First name is required" };
            yield return new object[] { new string('a', 51), "Doe", "valid@email.com", "First name cannot exceed 50 characters" };

            // Last name validation cases
            yield return new object[] { "John", null, "valid@email.com", "Last name is required" };
            yield return new object[] { "John", "", "valid@email.com", "Last name is required" };
            yield return new object[] { "John", " ", "valid@email.com", "Last name is required" };
            yield return new object[] { "John", new string('a', 51), "valid@email.com", "Last name cannot exceed 50 characters" };

            // Email validation cases
            yield return new object[] { "John", "Doe", null, "Email is required" };
            yield return new object[] { "John", "Doe", "", "Email is required" };
            yield return new object[] { "John", "Doe", " ", "Email is required" };
            yield return new object[] { "John", "Doe", "invalid-email", "Invalid email format" };

            // CORRECTED: Only include INVALID email length (257 characters)
            yield return new object[] {
                "John",
                "Doe",
                $"{new string('a', 248)}@test.com", // 248 + 9 = 257 characters
                "Email cannot exceed 256 characters"
            };
        }
    }

    public static IEnumerable<object[]> InvalidNameUpdateParameters
    {
        get
        {
            // First name validation cases
            yield return new object[] { null, "Smith", "First name is required" };
            yield return new object[] { "", "Smith", "First name is required" };
            yield return new object[] { " ", "Smith", "First name is required" };
            yield return new object[] { new string('a', 51), "Smith", "First name cannot exceed 50 characters" };

            // Last name validation cases
            yield return new object[] { "Jane", null, "Last name is required" };
            yield return new object[] { "Jane", "", "Last name is required" };
            yield return new object[] { "Jane", " ", "Last name is required" };
            yield return new object[] { "Jane", new string('a', 51), "Last name cannot exceed 50 characters" };
        }
    }

    public static IEnumerable<object[]> InvalidContactInfoParameters
    {
        get
        {
            // Phone validation cases
            yield return new object[] { "invalid-phone", "valid@email.com", "Invalid phone number format" };

            // Email validation cases
            yield return new object[] { "+1234567890", "invalid-email", "Invalid email format" };
            yield return new object[] { "+1234567890", null, "Email is required" };
            yield return new object[] { "+1234567890", "", "Email is required" };
            yield return new object[] { "+1234567890", " ", "Email is required" };

            // CORRECTED: Only include INVALID email length (257 characters)
            yield return new object[] {
                "+1234567890",
                $"{new string('a', 248)}@test.com", // 248 + 9 = 257 characters
                "Email cannot exceed 256 characters"
            };
        }
    }

    public static IEnumerable<object[]> InvalidAddressParameters
    {
        get
        {
            // Address validation
            yield return new object[] {
                new string('a', 101), // Exactly 101 characters
                "ValidCity",
                "ValidCountry",
                "Address exceeds max length"
            };

            // City validation
            yield return new object[] {
                "ValidAddress",
                new string('a', 51), // Exactly 51 characters
                "ValidCountry",
                "City exceeds max length"
            };

            // Country validation
            yield return new object[] {
                "ValidAddress",
                "ValidCity",
                new string('a', 51), // Exactly 51 characters
                "Country exceeds max length"
            };
        }
    }

    // ADDED: Valid email length test cases (256 characters)
    public static IEnumerable<object[]> ValidEmailLengthParameters
    {
        get
        {
            yield return new object[] { $"{new string('a', 247)}@test.com" }; // 247 + 9 = 256
            yield return new object[] { $"{new string('a', 240)}@example.com" }; // 240 + 11 = 251
        }
    }
}