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
            yield return new object[] { "John", "Doe", new string('a', 257), "Email cannot exceed 256 characters" };
        }
    }

    public static IEnumerable<object[]> InvalidNameUpdateParameters
    {
        get
        {
            // First name validation cases
            yield return new object[] { null, "Smith", "Null first name" };
            yield return new object[] { "", "Smith", "Empty first name" };
            yield return new object[] { " ", "Smith", "Whitespace first name" };

            // Last name validation cases
            yield return new object[] { "Jane", null, "Null last name" };
            yield return new object[] { "Jane", "", "Empty last name" };
            yield return new object[] { "Jane", " ", "Whitespace last name" };
        }
    }

    public static IEnumerable<object[]> InvalidContactInfoParameters
    {
        get
        {
            // Phone validation cases
            yield return new object[] { "invalid-phone", "valid@email.com", "Invalid phone format" };

            // Email validation cases
            yield return new object[] { "+1234567890", "invalid-email", "Invalid email format" };
            yield return new object[] { "+1234567890", null, "Null email" };
            yield return new object[] { "+1234567890", "", "Empty email" };
            yield return new object[] { "+1234567890", " ", "Whitespace email" };
        }
    }

    public static IEnumerable<object[]> InvalidAddressParameters
    {
        get
        {
            // Address validation cases
            yield return new object[] {
                    "This address is way too long and exceeds the maximum allowed length of 100 characters for the address field in the customer entity.",
                    "City",
                    "Country",
                    "Address exceeds max length"
                };

            // City validation cases
            yield return new object[] {
                    "Address",
                    "This city name is way too long and exceeds the maximum allowed length of 50 characters",
                    "Country",
                    "City exceeds max length"
                };

            // Country validation cases
            yield return new object[] {
                    "Address",
                    "City",
                    "This country name is way too long and exceeds the maximum allowed length of 50 characters",
                    "Country exceeds max length"
                };
        }
    }
}