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
        var endpoint = ApiEndPoints.BaseCategoryEndpoint;
        using var dbContext = ScopedDbContext
            .Create(_scopeFactory)
            .DbContext;

        var expected = await dbContext
            .Categories
            .ToListAsync();

        var expectedCategories = _mapper
            .Map<List<CategoryDto>>(expected);

        // Act
        var httpResponseMsg = await _client
            .GetAsync(endpoint);

        httpResponseMsg
            .EnsureSuccessStatusCode();

        var categoryList = await httpResponseMsg
            .Content
            .ReadFromJsonAsync<List<Category>>();

        var actualCategories = categoryList!
            .Select(_mapper.Map<CategoryDto>)
            .ToList();

        // Assert
        Assert.NotNull(actualCategories);
        Assert.NotEmpty(actualCategories);
        actualCategories.Should().BeEquivalentTo(expectedCategories);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public async Task GetById_ReturnsCategry_GivenValidData(int categoryId)
    {
        // Arrange
        var endpopint = ApiEndPoints.SingleCategoryEndpoint(categoryId);
        using var dbContext = ScopedDbContext
            .Create(_scopeFactory)
            .DbContext;

        var expected = await dbContext
            .Categories
            .FindAsync(categoryId);

        var expectedCatagory = _mapper
            .Map<CategoryDto>(expected);

        // Act
        var httpResponseMsg = await _client
            .GetAsync(endpopint);

        httpResponseMsg.EnsureSuccessStatusCode();
        var actualCategory = await httpResponseMsg
            .Content
            .ReadFromJsonAsync<CategoryDto>();

        // Assert
        httpResponseMsg.StatusCode.Should().Be(HttpStatusCode.OK);
        actualCategory.Should().NotBeNull();
        actualCategory!.Name.Should().Be(expectedCatagory.Name);
        actualCategory!.DateAdded.Should().Be(expectedCatagory.DateAdded);
        actualCategory!.Description.Should().Be(expectedCatagory.Description);
    }

    public void Dispose() => _testScope.Dispose();

    private readonly CustomWebApplicationFactory _factory;
    private readonly HttpClient _client;
    private readonly IServiceScope _testScope;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IMapper _mapper;
}

