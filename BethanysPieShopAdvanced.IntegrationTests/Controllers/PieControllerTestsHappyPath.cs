using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace BethanysPieShop.IntegrationTests.Controllers;

[Collection("Database Collection")]
public class PieControllerTestsHappyPath :
    TestBase, IClassFixture<CustomWebApplicationFactory>, IDisposable, IAsyncLifetime
{
    public PieControllerTestsHappyPath(CustomWebApplicationFactory factory)
    {
        _factory = factory;
        _client = _factory.CreateClient();

        _testScope = _factory.Services.CreateScope();
        _scopeFactory = _testScope.ServiceProvider.GetRequiredService<IServiceScopeFactory>();
        _mapper = _testScope.ServiceProvider.GetRequiredService<IMapper>();
    }

    public ValueTask InitializeAsync()
    {
        // Ensure each test starts from a known seeded state
        _factory.ResetDb();
        return ValueTask.CompletedTask;
    }

    public ValueTask DisposeAsync() => ValueTask.CompletedTask;

    [Fact]
    public async Task Index_ReturnsAllPies_GivenValidRouteAsync()
    {
        // Arrange
        var endpoint = ApiPieEndPoints.BasePieEndpoint;
        using var scopedDb = ScopedDbContext.Create(_scopeFactory);
        var dbContext = scopedDb.DbContext;

        var expected = (await dbContext.Pies.ToListAsync()).OrderBy(p => p.Name);
        var expectedPies = _mapper.Map<List<PieDto>>(expected);

        // Act
        var httpResponseMsg = await _client.GetAsync(endpoint);
        httpResponseMsg.EnsureSuccessStatusCode();
        var actual = await httpResponseMsg.Content.ReadFromJsonAsync<List<Pie>>();

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
        var endpoint = ApiPieEndPoints.CreatePieEndpoint;

        var expectedPie = GetCreatedPie(pieName, categoryId);
        var serializedPie = JsonSerializer.Serialize(expectedPie, JsonSettings.JsonOptions);
        var httpContent = new StringContent(serializedPie, Encoding.UTF8, GeneralValues.JsonMediaType);

        // Act
        var httpResponseMsg = await _client.PostAsync(endpoint, httpContent);
        var responseContent = await httpResponseMsg.Content.ReadAsStringAsync();

        // Assert
        httpResponseMsg.StatusCode.Should().Be(HttpStatusCode.Created, responseContent);

        // Use a fresh scope to avoid stale tracking
        using var verifyScope = _factory.Services.CreateScope();
        var verifyDb = verifyScope.ServiceProvider.GetRequiredService<PieShopDbContext>();

        var createdPie = await verifyDb.Pies.FirstOrDefaultAsync(p => p.Name == pieName);
        createdPie.Should().NotBeNull(responseContent);

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

        var expectedPie = GetUpdatedPie(id);
        var serializedPie = JsonSerializer.Serialize(expectedPie, JsonSettings.JsonOptions);
        var httpContent = new StringContent(serializedPie, Encoding.UTF8, GeneralValues.JsonMediaType);

        // Act
        var httpResponseMsg = await _client.PutAsync(endpoint, httpContent);
        var responseContent = await httpResponseMsg.Content.ReadAsStringAsync();

        // Assert
        httpResponseMsg.StatusCode.Should().Be(HttpStatusCode.OK, responseContent);

        // Use a fresh scope to verify persisted changes
        using var verifyScope = _factory.Services.CreateScope();
        var verifyDb = verifyScope.ServiceProvider.GetRequiredService<PieShopDbContext>();

        var updatedPie = await verifyDb.Pies.FindAsync(id);
        updatedPie.Should().NotBeNull(responseContent);

        var actualPie = _mapper.Map<PieDto>(updatedPie);
        actualPie.CategoryName = expectedPie.CategoryName;

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

        // Act
        var httpResponseMsg = await _client.DeleteAsync(endpoint);
        var responseContent = await httpResponseMsg.Content.ReadAsStringAsync();

        // Assert
        httpResponseMsg.StatusCode.Should().Be(HttpStatusCode.OK, responseContent);

        // Use a fresh scope to verify deletion
        using var verifyScope = _factory.Services.CreateScope();
        var verifyDb = verifyScope.ServiceProvider.GetRequiredService<PieShopDbContext>();

        var pie = await verifyDb.Pies.FindAsync(id);
        Assert.Null(pie);
    }

    public void Dispose() => _testScope.Dispose();

    private readonly CustomWebApplicationFactory _factory;
    private readonly HttpClient _client;
    private readonly IServiceScope _testScope;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IMapper _mapper;
}
