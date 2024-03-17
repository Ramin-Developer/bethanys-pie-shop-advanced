namespace BethanysPieShop.IntegrationTest.Controllers;

// Todo: Create and Update methods in WebApi.Controllers should return Created and NoContent, respectively.
// Todo: Modify also the corresponding tests accordingly.
// Todo: Fix the following problem with CreatePie() test: 
//       CategoryName is empty because CategoryId is wrong.

[Collection("Database Collection")]
public class PieControllerTests : TestBase, IDisposable
{
    public PieControllerTests()
    {
        _factory = new CustomWebApplicationFactory();
        _factory.SeedData();
        _client = _factory.CreateClient();

        using var scope = _factory.Services.CreateScope();
        _scopeFactory = _factory.Services.GetRequiredService<IServiceScopeFactory>();
        _mapper = scope.ServiceProvider.GetRequiredService<IMapper>();
    }

    public void Dispose()
    {
        _client.Dispose();
        _factory.Dispose();
    }

    [Fact]
    public async Task Index_ReturnsAllPies_GivenValidRouteAsync()
    {
        // Arrange
        var endPoint = ApiEndPoints.BasePieEndpoint;
        using var scopedDb = ScopedDbContext.Create(_scopeFactory);
        var dbContext = scopedDb.DbContext;

        var expected = (await dbContext.Pies.ToListAsync()).OrderBy(p => p.Name);
        var expectedPies = _mapper.Map<List<PieDto>>(expected);

        // Act
        var httpResponseMsg = await _client.GetAsync(endPoint);
        httpResponseMsg.EnsureSuccessStatusCode();
        var actual = await
            httpResponseMsg
            .Content
            .ReadFromJsonAsync<List<Pie>>();

        // Assert
        actual.Should().NotBeNull();
        var actualPies = actual!.OrderBy(p => p.Name).Select(p => _mapper.Map<PieDto>(p)).ToList();
        actualPies.Should().BeEquivalentTo(expectedPies);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(3)]
    [InlineData(7)]
    [InlineData(12)]
    [InlineData(16)]
    public async Task GetById_ReturnsPie_GivenValidInputAsync(int pieId)
    {
        // Arrange
        var endpoint = ApiEndPoints.SinglePieEndpoint(pieId);
        using var scopedDb = ScopedDbContext.Create(_scopeFactory);
        var dbContext = scopedDb.DbContext;
        var expected = await dbContext.FindAsync<Pie>(pieId);
        var expectedPie = _mapper.Map<PieDto>(expected);

        // Act
        var httpResponseMsg = await _client.GetAsync(endpoint);
        httpResponseMsg.EnsureSuccessStatusCode();
        var actualPie = await httpResponseMsg
            .Content
            .ReadFromJsonAsync<PieDto>();

        // Assert
        httpResponseMsg.StatusCode.Should().Be(HttpStatusCode.OK);
        expected.Should().NotBeNull();
        actualPie.Should().BeEquivalentTo(expectedPie);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    public async Task GetById_ReturnsNotFound_GivenInvalidIdAsync(int invalidId)
    {
        // Arrange
        var endpoint = ApiEndPoints.SinglePieEndpoint(invalidId);

        // Act
        var httpResponseMsg = await _client.GetAsync(endpoint);

        // Assert
        httpResponseMsg.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Theory]
    [InlineData("My New Pie", 1)]
    public async Task Create_ShouldReturnOk_WhenValidData(string pieName, int categoryId)
    {
        // Arrange
        var endPoint = ApiEndPoints.CreatePieEndpoint;
        using var scopedDb = ScopedDbContext.Create(_scopeFactory);
        var dbContext = scopedDb.DbContext;
        var expectedPie = GetCreatedPie(pieName, categoryId);

        // Act
        var serializedPie = JsonSerializer.Serialize(expectedPie, JsonSettings.JsonOptions);
        var httpContent = new StringContent(serializedPie, Encoding.UTF8, "application/json");
        var httpResponseMsg = await _client.PostAsync(endPoint, httpContent);

        // Fetch the updated pie from the database
        var createdPie = await dbContext
            .Pies
            .FirstOrDefaultAsync(p => p.Name == pieName);
        expectedPie.Id = createdPie!.Id;

        var actualPie = _mapper.Map<PieDto>(createdPie);
        actualPie.CategoryId = expectedPie.CategoryId;
        actualPie.CategoryName = expectedPie.CategoryName;

        // Assert
        httpResponseMsg.StatusCode.Should().Be(HttpStatusCode.OK);
        expectedPie.Should().NotBeNull();
        actualPie.Should().NotBeNull();

        actualPie.Should().BeEquivalentTo(expectedPie);
    }

    [Theory]
    [InlineData(5)]
    public async Task Update_ShouldReturnOk_GivenValidData(int id)
    {
        // Arrange
        var endpoint = ApiEndPoints.SinglePieEndpoint(id);
        using var dbContext = ScopedDbContext
            .Create(_scopeFactory)
            .DbContext;

        var expectedPie = GetUpdatedPie(id);
        var serializedPie = JsonSerializer.Serialize(expectedPie);
        var httpContent = new StringContent(serializedPie, Encoding.UTF8, "application/json");

        // Act
        var httpResponseMsg = await _client.PutAsync(endpoint, httpContent);
        httpResponseMsg.EnsureSuccessStatusCode();

        // Fetch the updated pie from the database
        var updatedPie = await dbContext.Pies.FindAsync(id);
        var actualPie = _mapper.Map<PieDto>(updatedPie);
        actualPie.CategoryName = expectedPie.CategoryName;

        // Assert
        httpResponseMsg.StatusCode.Should().Be(HttpStatusCode.OK);
        expectedPie.Should().NotBeNull();
        actualPie.Should().NotBeNull();

        actualPie.Should().BeEquivalentTo(expectedPie);
    }

    [Theory]
    [InlineData(5)]
    [InlineData(7)]
    [InlineData(12)]
    public async Task Delete_ShouldRemovePie_GivenValidData(int id)
    {
        // Arrange
        var endpoint = ApiEndPoints.SinglePieEndpoint(id);
        using var dbContext = ScopedDbContext
            .Create(_scopeFactory)
            .DbContext;
        var originalPiesCount = await dbContext.Pies.CountAsync();
        var expectedPiesCount = originalPiesCount - 1;

        // Act
        var httpResponseMsg = await _client.DeleteAsync(endpoint);
        httpResponseMsg.EnsureSuccessStatusCode();
        var actualPiesCount = await dbContext.Pies.CountAsync();

        // Assert
        var pie = await dbContext.Pies.FindAsync(id);
        Assert.Equal(HttpStatusCode.OK, httpResponseMsg.StatusCode);
        Assert.Null(pie);
        Assert.Equal(expectedPiesCount, actualPiesCount);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    public async Task Delete_ShouldReturnBadRequest_GivenInvalidData(int id)
    {
        // Arrange
        var endpoint = ApiEndPoints.SinglePieEndpoint(id);

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
        var endpoint = ApiEndPoints.SinglePieEndpoint(id);

        // Act
        var httpResponseMsg = await _client.DeleteAsync(endpoint);

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, httpResponseMsg.StatusCode);
    }

    private readonly CustomWebApplicationFactory _factory;
    private readonly HttpClient _client;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IMapper _mapper;
}
