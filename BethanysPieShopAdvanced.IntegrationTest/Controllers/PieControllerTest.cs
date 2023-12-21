namespace BethanysPieShop.IntegrationTest.Controllers;

// Todo: Create and Update methods in WebApi.Controllers should return Created and NoContent, respectively.
// Todo: Modify also the corresponding tests accordingly.

[Collection("Database Collection")]
public class PieControllerTest : TestBase, IDisposable
{
    public PieControllerTest()
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
        var endpoint = "api/pie";
        using var scopedDb = ScopedDbContext.Create(_scopeFactory);
        var dbContext = scopedDb.DbContext;

        var expected = (await dbContext.Pies.ToListAsync()).OrderBy(p => p.Name);
        var expectedPies = _mapper.Map<List<PieDto>>(expected);

        // Act
        var httpResponseMsg = await _client.GetAsync(endpoint);
        httpResponseMsg.EnsureSuccessStatusCode();
        var actual = await
            httpResponseMsg
            .Content
            .ReadFromJsonAsync<List<PieDto>>();

        // Assert
        httpResponseMsg.StatusCode.Should().Be(HttpStatusCode.OK);
        actual.Should().NotBeNull();
        var actualPies = actual!.OrderBy(p => p.Name).ToList();

        //actualPies.Should().BeEquivalentTo(expectedPies, options =>
        //    options.Excluding(p => p.Path.StartsWith("RowVersion")));

        for (var counter = 0; counter < expectedPies.Count; counter++)
        {
            actualPies[counter]!.Name.Should().Be(expectedPies[counter]!.Name);
            actualPies[counter].ShortDescription.Should().Be(expectedPies[counter]!.ShortDescription);
            actualPies[counter].LongDescription.Should().Be(expectedPies[counter]!.LongDescription);
            actualPies[counter].AllergyInformation.Should().Be(expectedPies[counter]!.AllergyInformation);
            actualPies[counter].Price.Should().Be(expectedPies[counter]!.Price);
            actualPies[counter].ImageUrl.Should().Be(expectedPies[counter]!.ImageUrl);
            actualPies[counter].ImageThumbnailUrl.Should().Be(expectedPies[counter]!.ImageThumbnailUrl);
            actualPies[counter].IsPieOfTheWeek.Should().Be(expectedPies[counter]!.IsPieOfTheWeek);
            actualPies[counter].InStock.Should().Be(expectedPies[counter]!.InStock);
            actualPies[counter].CategoryId.Should().Be(expectedPies[counter]!.CategoryId);
            //actualPies[counter].CategoryName.Should().Be(expectedPies[counter]!.CategoryName);
        }
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
        var endpoint = $"api/pie/{pieId}";
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
        actualPie.Should().NotBeNull();

        //actualPie.Should().BeEquivalentTo(expectedPie);

        actualPie!.Id.Should().Be(expected!.Id);
        actualPie!.Name.Should().Be(expected!.Name);
        actualPie.ShortDescription.Should().Be(expected.ShortDescription);
        actualPie.LongDescription.Should().Be(expected.LongDescription);
        actualPie.AllergyInformation.Should().Be(expected.AllergyInformation);
        actualPie.Price.Should().Be(expected.Price);
        actualPie.ImageUrl.Should().Be(expected.ImageUrl);
        actualPie.ImageThumbnailUrl.Should().Be(expected.ImageThumbnailUrl);
        actualPie.IsPieOfTheWeek.Should().Be(expected.IsPieOfTheWeek);
        actualPie.InStock.Should().Be(expected.InStock);
        actualPie.CategoryId.Should().Be(expected.CategoryId);
        //actualPie.CategoryName.Should().Be(expected.CategoryName);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    public async Task GetById_ReturnsNotFound_GivenInvalidIdAsync(int invalidId)
    {
        // Arrange
        var endpoint = $"api/pie/{invalidId}";

        // Act
        var httpResponseMsg = await _client.GetAsync(endpoint);

        // Assert
        httpResponseMsg.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Create_ShouldReturnOk_WhenValidData()
    {
        // Arrange
        var endpoint = $"api/pie/";
        using var scopedDb = ScopedDbContext.Create(_scopeFactory);
        var dbContext = scopedDb.DbContext;
        var expectedPie = GetCreatedPie();
        var serializedPie = JsonSerializer.Serialize(expectedPie);
        var httpContent = new StringContent(serializedPie, Encoding.UTF8, "application/json");

        // Act
        var httpResponseMsg = await _client.PostAsync(endpoint, httpContent);

        // Fetch the updated pie from the database
        var createdPie = await dbContext
            .Pies
            .FirstOrDefaultAsync(p => p.Name == expectedPie.Name);

        var actualPie = _mapper.Map<PieDto>(createdPie);
        expectedPie.Id = createdPie!.Id;
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
        var endpoint = $"api/pie/{id}";
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

        //actualPie.Should().BeEquivalentTo(expectedPie, options => options
        //    .Excluding(p => p.RowVersion));

        actualPie.Name.Should().Be(expectedPie.Name);
        actualPie.ShortDescription.Should().Be(expectedPie.ShortDescription);
        actualPie.LongDescription.Should().Be(expectedPie.LongDescription);
        actualPie.AllergyInformation.Should().Be(expectedPie.AllergyInformation);
        actualPie.Price.Should().Be(expectedPie.Price);
        actualPie.ImageUrl.Should().Be(expectedPie.ImageUrl);
        actualPie.ImageThumbnailUrl.Should().Be(expectedPie.ImageThumbnailUrl);
        actualPie.IsPieOfTheWeek.Should().Be(expectedPie.IsPieOfTheWeek);
        actualPie.InStock.Should().Be(expectedPie.InStock);
        actualPie.CategoryId.Should().Be(expectedPie.CategoryId);
        //actualPie.CategoryName.Should().Be(expectedPie.CategoryName);
    }

    [Theory]
    [InlineData(5)]
    [InlineData(7)]
    [InlineData(12)]
    public async Task Delete_ShouldRemovePie_GivenValidData(int id)
    {
        // Arrange
        var endpoint = $"api/pie/{id}";
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
        var endpoint = $"api/pie/{id}";

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
        var endpoint = $"api/pie/{id}";

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
