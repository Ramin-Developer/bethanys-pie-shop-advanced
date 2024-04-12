namespace BethanysPieShop.IntegrationTests.Controllers;

[Collection("Database Collection")]
public class CategoryControllerTestsHappyPath :
    TestBase, IClassFixture<CustomWebApplicationFactory>, IDisposable
{
    public CategoryControllerTestsHappyPath(CustomWebApplicationFactory factory)
    {
        _factory = factory;
        _factory.SeedData();

        _client = _factory
            .CreateClient();

        _testScope = _factory
            .Services
            .CreateScope();

        _scopeFactory = _testScope
            .ServiceProvider
            .GetRequiredService<IServiceScopeFactory>();

        _mapper = _testScope
            .ServiceProvider
            .GetRequiredService<IMapper>();
    }

    [Fact]
    public async Task GetAllAsync_ReturnsCategoryDtoList()
    {
        // Arrange
        var endpoint = ApiCategoryEndpoints.BaseCategoryEndpoint;
        var expectedCategories = await GetExpectedCategoriesAsync();

        // Act
        var actualCategories = await GetActualAsync<List<CategoryDto>>(endpoint);

        // Assert
        Assert.NotNull(actualCategories);
        Assert.NotEmpty(actualCategories);
        actualCategories.Should().BeEquivalentTo(expectedCategories);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public async Task GetById_ReturnsCategry_GivenValidData(int id)
    {
        // Arrange
        var endpopint = ApiCategoryEndpoints.SingleCategoryIdEndpoint(id);
        var expectedCatagory = await GetExpectedCategoryAsync(id);

        // Act
        var actualCategory = await GetActualAsync<CategoryDto>(endpopint);

        // Assert
        actualCategory.Should().NotBeNull();
        actualCategory.Should()
            .BeEquivalentTo(expectedCatagory, options => options
                .Excluding(c => c.Id)
                .Excluding(c => c.ErrorMessage)
                .Excluding(c => c.SuccessMessage)
                .Excluding(c => c.PieList));
    }

    public void Dispose() => _testScope.Dispose();

    private async Task<List<CategoryDto>> GetExpectedCategoriesAsync()
    {
        using var dbContext = ScopedDbContext
            .Create(_scopeFactory)
            .DbContext;

        var expected = await dbContext
            .Categories
            .ToListAsync();

        return _mapper
            .Map<List<CategoryDto>>(expected);
    }

    private async Task<CategoryDto> GetExpectedCategoryAsync(int id)
    {
        using var dbContext = ScopedDbContext
            .Create(_scopeFactory)
            .DbContext;

        var expected = await dbContext
            .Categories
            .FindAsync(id);

        return _mapper
            .Map<CategoryDto>(expected);
    }

    private async Task<T?> GetActualAsync<T>(string endpoint)
    {
        var httpResponseMsg = await _client
            .GetAsync(endpoint);

        httpResponseMsg.EnsureSuccessStatusCode();

        return await httpResponseMsg
            .Content.ReadFromJsonAsync<T>();
    }

    private readonly CustomWebApplicationFactory _factory;
    private readonly HttpClient _client;
    private readonly IServiceScope _testScope;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IMapper _mapper;
}
