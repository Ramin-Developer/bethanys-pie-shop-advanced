namespace BethanysPieShop.IntegrationTests.Controllers;

[Collection("Database Collection")]
public class CategoryControllerTestsHappyPath(CustomWebApplicationFactory factory) :
    TestBase, IClassFixture<CustomWebApplicationFactory>, IDisposable
{
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
    public async Task GetById_ReturnsCategory_GivenValidData(int id)
    {
        // Arrange
        var endpoint = ApiCategoryEndpoints.SingleCategoryIdEndpoint(id);
        var expectedCategory = await GetExpectedCategoryAsync(id);

        // Act
        var actualCategory = await GetActualAsync<CategoryDto>(endpoint);

        // Assert
        actualCategory.Should().NotBeNull();
        actualCategory.Should()
            .BeEquivalentTo(expectedCategory, options => options
                .Excluding(c => c.Id)
                .Excluding(c => c.ErrorMessage)
                .Excluding(c => c.SuccessMessage)
                .Excluding(c => c.PieList));
    }

    public void Dispose()
    {
        _testScope.Dispose();
        GC.SuppressFinalize(this);
    }

    private async Task<List<CategoryDto>> GetExpectedCategoriesAsync()
    {
        using var dbContext = ScopedDbContext
            .Create(_scopeFactory)
            .DbContext;

        var expected = await dbContext
            .Categories
            .ToListAsync(TestContext.Current.CancellationToken);

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
            .FindAsync([id], TestContext.Current.CancellationToken);

        return _mapper
            .Map<CategoryDto>(expected);
    }

    private async Task<T?> GetActualAsync<T>(string endpoint)
    {
        var httpResponseMsg = await _client
            .GetAsync(endpoint, TestContext.Current.CancellationToken);

        httpResponseMsg.EnsureSuccessStatusCode();

        return await httpResponseMsg
            .Content.ReadFromJsonAsync<T>(TestContext.Current.CancellationToken);
    }

    private readonly HttpClient _client = factory.CreateClient();
    private readonly IServiceScope _testScope = factory.Services.CreateScope();
    private readonly IServiceScopeFactory _scopeFactory = factory.Services.GetRequiredService<IServiceScopeFactory>();
    private readonly IMapper _mapper = factory.Services.GetRequiredService<IMapper>();
}
