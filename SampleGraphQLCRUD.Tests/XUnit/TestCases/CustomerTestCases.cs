namespace SampleGraphQLCRUD.Tests.XUnit.TestCases;

public static class CustomerTestCases
{
    // Constants for validation rules to avoid magic numbers
    private const int MaxNameLength = 50;

    private const int MaxAddressLength = 100;
    private const int MaxCityCountryLength = 50;
    private const int MaxEmailLength = 256;

    // Helper methods to generate test strings
    private static string LongString(int length) => new string('a', length);

    private static string LongEmail(int localPartLength) => $"{LongString(localPartLength)}@test.com";

    public static IEnumerable<object[]> InvalidConstructorParameters => new List<object[]>
    {
        // First name validation
        CreateTestCase(null, "Doe", "valid@email.com", "First name is required"),
        CreateTestCase("", "Doe", "valid@email.com", "First name is required"),
        CreateTestCase(" ", "Doe", "valid@email.com", "First name is required"),
        CreateTestCase(LongString(MaxNameLength + 1), "Doe", "valid@email.com", "First name cannot exceed 50 characters"),

        // Last name validation
        CreateTestCase("John", null, "valid@email.com", "Last name is required"),
        CreateTestCase("John", "", "valid@email.com", "Last name is required"),
        CreateTestCase("John", " ", "valid@email.com", "Last name is required"),
        CreateTestCase("John", LongString(MaxNameLength + 1), "valid@email.com", "Last name cannot exceed 50 characters"),

        // Email validation
        CreateTestCase("John", "Doe", null, "Email is required"),
        CreateTestCase("John", "Doe", "", "Email is required"),
        CreateTestCase("John", "Doe", " ", "Email is required"),
        CreateTestCase("John", "Doe", "invalid-email", "Invalid email format"),
        CreateTestCase("John", "Doe", LongEmail(MaxEmailLength - "@test.com".Length + 1), "Email cannot exceed 256 characters")
    };

    public static IEnumerable<object[]> InvalidNameUpdateParameters => new List<object[]>
    {
        // First name validation
        CreateNameUpdateTestCase(null, "Smith", "First name is required"),
        CreateNameUpdateTestCase("", "Smith", "First name is required"),
        CreateNameUpdateTestCase(" ", "Smith", "First name is required"),
        CreateNameUpdateTestCase(LongString(MaxNameLength + 1), "Smith", "First name cannot exceed 50 characters"),

        // Last name validation
        CreateNameUpdateTestCase("Jane", null, "Last name is required"),
        CreateNameUpdateTestCase("Jane", "", "Last name is required"),
        CreateNameUpdateTestCase("Jane", " ", "Last name is required"),
        CreateNameUpdateTestCase("Jane", LongString(MaxNameLength + 1), "Last name cannot exceed 50 characters")
    };

    public static IEnumerable<object[]> InvalidContactInfoParameters => new List<object[]>
    {
        // Phone validation
        CreateContactInfoTestCase("invalid-phone", "valid@email.com", "Invalid phone number format"),

        // Email validation
        CreateContactInfoTestCase("+1234567890", "invalid-email", "Invalid email format"),
        CreateContactInfoTestCase("+1234567890", null, "Email is required"),
        CreateContactInfoTestCase("+1234567890", "", "Email is required"),
        CreateContactInfoTestCase("+1234567890", " ", "Email is required"),
        CreateContactInfoTestCase("+1234567890", LongEmail(MaxEmailLength - "@test.com".Length + 1), "Email cannot exceed 256 characters")
    };

    public static IEnumerable<object[]> InvalidAddressParameters => new List<object[]>
    {
        // Address validation
        CreateAddressTestCase(
            LongString(MaxAddressLength + 1),
            "ValidCity",
            "ValidCountry",
            "Address exceeds max length"),

        // City validation
        CreateAddressTestCase(
            "ValidAddress",
            LongString(MaxCityCountryLength + 1),
            "ValidCountry",
            "City exceeds max length"),

        // Country validation
        CreateAddressTestCase(
            "ValidAddress",
            "ValidCity",
            LongString(MaxCityCountryLength + 1),
            "Country exceeds max length")
    };

    public static IEnumerable<object[]> ValidEmailLengthParameters => new List<object[]>
    {
        CreateValidEmailTestCase(LongEmail(MaxEmailLength - "@test.com".Length)), // Exactly 256
        CreateValidEmailTestCase(LongEmail(MaxEmailLength - "@example.com".Length)) // Shorter variant
    };

    // Helper methods to create test cases with consistent structure
    private static object[] CreateTestCase(string firstName, string lastName, string email, string expectedError)
        => new object[] { firstName, lastName, email, expectedError };

    private static object[] CreateNameUpdateTestCase(string firstName, string lastName, string expectedError)
        => new object[] { firstName, lastName, expectedError };

    private static object[] CreateContactInfoTestCase(string phone, string email, string expectedError)
        => new object[] { phone, email, expectedError };

    private static object[] CreateAddressTestCase(string address, string city, string country, string expectedError)
        => new object[] { address, city, country, expectedError };

    private static object[] CreateValidEmailTestCase(string email)
        => new object[] { email };
}