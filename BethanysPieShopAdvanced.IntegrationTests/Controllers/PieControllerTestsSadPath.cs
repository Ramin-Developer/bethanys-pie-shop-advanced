namespace BethanysPieShop.IntegrationTests.Controllers;

[Collection("Database Collection")]
public class PieControllerTestsSadPath(CustomWebApplicationFactory factory) :
    TestBase, IClassFixture<CustomWebApplicationFactory>, IDisposable
{
    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    public async Task GetPieByIdAsync_ReturnsBadRequest_GivenInvalidIdAsync(int invalidId)
    {
        // Arrange
        var endpoint = ApiPieEndPoints.SinglePieEndpoint(invalidId);

        // Act
        var httpResponseMsg = await _client.GetAsync(endpoint, TestContext.Current.CancellationToken);

        // Assert
        httpResponseMsg.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Theory]
    [InlineData(100)]
    [InlineData(1000)]
    [InlineData(10000)]
    public async Task GetPieByIdAsync_ReturnsNotFound_GivenNonExistentIdAsync(int invalidId)
    {
        // Arrange
        var endpoint = ApiPieEndPoints.SinglePieEndpoint(invalidId);

        // Act
        var httpResponseMsg = await _client.GetAsync(endpoint, TestContext.Current.CancellationToken);

        // Assert
        httpResponseMsg.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    public void Dispose()
    {
        _testScope.Dispose();
        GC.SuppressFinalize(this);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    public async Task Delete_ShouldReturnBadRequest_GivenInvalidData(int id)
    {
        // Arrange
        var endpoint = ApiPieEndPoints.SinglePieEndpoint(id);

        // Act
        var httpResponseMsg = await _client.DeleteAsync(endpoint, TestContext.Current.CancellationToken);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, httpResponseMsg.StatusCode);
    }

    [Theory]
    [InlineData(100)]
    [InlineData(1000)]
    public async Task Delete_ShouldReturnNotFound_GivenInvalidData(int id)
    {
        // Arrange
        var endpoint = ApiPieEndPoints.SinglePieEndpoint(id);

        // Act
        var httpResponseMsg = await _client.DeleteAsync(endpoint, TestContext.Current.CancellationToken);

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, httpResponseMsg.StatusCode);
    }

    private readonly HttpClient _client = factory.CreateClient();
    private readonly IServiceScope _testScope = factory.Services.CreateScope();
    private readonly IServiceScopeFactory _scopeFactory = factory.Services.GetRequiredService<IServiceScopeFactory>();
    private readonly IMapper _mapper = factory.Services.GetRequiredService<IMapper>();
}

