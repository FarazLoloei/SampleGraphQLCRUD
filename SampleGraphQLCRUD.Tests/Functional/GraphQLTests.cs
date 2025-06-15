using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json.Linq;
using SampleGraphQLCRUD.API;
using System.Net.Http.Json;

namespace SampleGraphQLCRUD.Tests.Functional;

public class GraphQLTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public GraphQLTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task CreateCustomer_ReturnsCorrectResult()
    {
        var query = new
        {
            query = @"
                mutation {
                    createCustomer(input: {
                        firstName: ""Jane"",
                        lastName: ""Doe"",
                        email: ""jane.doe@example.com""
                    }) {
                        id
                        email
                    }
                }"
        };

        var response = await _client.PostAsJsonAsync("/graphql", query);
        response.EnsureSuccessStatusCode();

        var json = JObject.Parse(await response.Content.ReadAsStringAsync());
        var email = json["data"]?["createCustomer"]?["email"]?.ToString();

        email.Should().Be("jane.doe@example.com");
    }
}