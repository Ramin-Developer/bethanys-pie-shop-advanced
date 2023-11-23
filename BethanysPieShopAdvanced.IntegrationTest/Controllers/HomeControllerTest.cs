namespace BethanysPieShop.IntegrationTest.Controllers;

public class HomeControllerTest(CustomWebApplicationFactory factory)
    : IClassFixture<CustomWebApplicationFactory>
{
    [Fact]
    public async Task HealthCheck_Returns_Healthy()
    {
        // Arrange

        // Act
        var response = await _client.GetAsync("/health");

        // Assert
        response.EnsureSuccessStatusCode();
    }

    private readonly HttpClient _client = factory.CreateClient();
}
