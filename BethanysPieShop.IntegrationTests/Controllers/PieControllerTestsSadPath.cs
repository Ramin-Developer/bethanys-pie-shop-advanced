namespace BethanysPieShop.IntegrationTests.Controllers;

[Collection("Database Collection")]
public class PieControllerTestsSadPath :
    TestBase, IClassFixture<CustomWebApplicationFactory>, IDisposable
{
    public PieControllerTestsSadPath(CustomWebApplicationFactory factory)
    {
        _factory = factory;
        _factory.SeedData();
        _client = _factory.CreateClient();

        _testScope = _factory.Services.CreateScope();
        _scopeFactory = _testScope.ServiceProvider.GetRequiredService<IServiceScopeFactory>();
        _mapper = _testScope.ServiceProvider.GetRequiredService<IMapper>();
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    public async Task GetPieByIdAsync_ReturnsBadRequest_GivenInvalidIdAsync(int invalidId)
    {
        // Arrange
        var endpoint = PieApiEndPoints.SinglePieEndpoint(invalidId);

        // Act
        var httpResponseMsg = await _client.GetAsync(endpoint);

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
        var endpoint = PieApiEndPoints.SinglePieEndpoint(invalidId);

        // Act
        var httpResponseMsg = await _client.GetAsync(endpoint);

        // Assert
        httpResponseMsg.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    public void Dispose() => _testScope.Dispose();

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    public async Task Delete_ShouldReturnBadRequest_GivenInvalidData(int id)
    {
        // Arrange
        var endpoint = PieApiEndPoints.SinglePieEndpoint(id);

        // Act
        var httpResponseMsg = await _client.DeleteAsync(endpoint);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, httpResponseMsg.StatusCode);
    }

    [Theory]
    [InlineData(100)]
    [InlineData(1000)]
    public async Task Delete_ShouldReturnNotFound_GivenInvalidData(int id)
    {
        // Arrange
        var endpoint = PieApiEndPoints.SinglePieEndpoint(id);

        // Act
        var httpResponseMsg = await _client.DeleteAsync(endpoint);

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, httpResponseMsg.StatusCode);
    }

    private readonly CustomWebApplicationFactory _factory;
    private readonly HttpClient _client;
    private readonly IServiceScope _testScope;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IMapper _mapper;
}

