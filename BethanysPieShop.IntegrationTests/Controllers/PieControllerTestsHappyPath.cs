namespace BethanysPieShop.IntegrationTest.Controllers;

[Collection("Database Collection")]
public class PieControllerTestsHappyPath :
    TestBase, IClassFixture<CustomWebApplicationFactory>, IDisposable
{
    public PieControllerTestsHappyPath(CustomWebApplicationFactory factory)
    {
        _factory = factory;
        _factory.SeedData();
        _client = _factory.CreateClient();

        _testScope = _factory.Services.CreateScope();
        _scopeFactory = _testScope.ServiceProvider.GetRequiredService<IServiceScopeFactory>();
        _mapper = _testScope.ServiceProvider.GetRequiredService<IMapper>();
    }

    [Fact]
    public async Task Index_ReturnsAllPies_GivenValidRouteAsync()
    {
        // Arrange
        var endPoint = ApiPieEndPoints.BasePieEndpoint;
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
    public async Task GetPieByIdAsync_ReturnsPie_GivenValidInputAsync(int pieId)
    {
        // Arrange
        var endpoint = ApiPieEndPoints.SinglePieEndpoint(pieId);
        using var scopedDb = ScopedDbContext.Create(_scopeFactory);
        var dbContext = scopedDb.DbContext;
        
        var expected = await dbContext.FindAsync<Pie>(pieId);
        var expectedPie = _mapper.Map<PieDto>(expected);

        // Act
        var httpResponseMsg = await _client.GetAsync(endpoint);
        httpResponseMsg.EnsureSuccessStatusCode();
        var actualPie = await httpResponseMsg.Content.ReadFromJsonAsync<PieDto>();

        // Assert
        httpResponseMsg.StatusCode.Should().Be(HttpStatusCode.OK);
        expected.Should().NotBeNull();
        actualPie.Should().BeEquivalentTo(expectedPie);
    }

    [Theory]
    [InlineData("My New Pie", 1)]
    public async Task Create_ShouldReturnCreated_WhenValidData(string pieName, int categoryId)
    {
        // Arrange
        var endPoint = ApiPieEndPoints.CreatePieEndpoint;
        using var scopedDb = ScopedDbContext.Create(_scopeFactory);
        var dbContext = scopedDb.DbContext;

        var expectedPie = GetCreatedPie(pieName, categoryId);

        // Act
        // Log the serialized JSON
        var serializedPie = JsonSerializer.Serialize(expectedPie, JsonSettings.JsonOptions);
        Console.WriteLine("Serialized Pie: " + serializedPie);
        var httpContent = new StringContent(serializedPie, Encoding.UTF8, GeneralValues.JsonMediaType);
        var httpResponseMsg = await _client.PostAsync(endPoint, httpContent);

        // Log response content for debugging
        var responseContent = await httpResponseMsg.Content.ReadAsStringAsync();
        Console.WriteLine("Response Content: " + responseContent);

        // Assert
        httpResponseMsg.StatusCode.Should().Be(HttpStatusCode.Created, responseContent);

        // Fetch the updated pie from the database
        var createdPie = await dbContext.Pies.FirstOrDefaultAsync(p => p.Name == pieName);
        expectedPie.Id = createdPie!.Id;

        var actualPie = _mapper.Map<PieDto>(createdPie);

        expectedPie.Should().NotBeNull();
        actualPie.Should().NotBeNull();
        actualPie.Should().BeEquivalentTo(expectedPie);
    }

    [Theory]
    [InlineData(5)]
    public async Task Update_ShouldReturnOk_GivenValidData(int id)
    {
        // Arrange
        var endpoint = ApiPieEndPoints.SinglePieEndpoint(id);
        using var scopedDb = ScopedDbContext.Create(_scopeFactory);
        var dbContext = scopedDb.DbContext;

        var expectedPie = GetUpdatedPie(id);
        var serializedPie = JsonSerializer.Serialize(expectedPie);
        var httpContent = new StringContent(serializedPie, Encoding.UTF8, GeneralValues.JsonMediaType);

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
        var endpoint = ApiPieEndPoints.SinglePieEndpoint(id);
        using var scopedDb = ScopedDbContext.Create(_scopeFactory);
        var dbContext = scopedDb.DbContext;

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

    public void Dispose() => _testScope.Dispose();

    private readonly CustomWebApplicationFactory _factory;
    private readonly HttpClient _client;
    private readonly IServiceScope _testScope;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IMapper _mapper;
}
