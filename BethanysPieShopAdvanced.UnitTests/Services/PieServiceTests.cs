namespace BethanysPieShop.UnitTests.Services;

public class PieServiceTests
{
    private readonly IPieRepository _pieRepo = Substitute.For<IPieRepository>();
    private readonly ICategoryRepository _categoryRepo = Substitute.For<ICategoryRepository>();
    private readonly IMapper _mapper = Substitute.For<IMapper>();

    private PieService CreateSut() => new(_pieRepo, _categoryRepo, _mapper);

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public async Task GetPieByIdAsync_WithNonPositiveId_ThrowsEntityPropertyFormatException(int id)
    {
        var sut = CreateSut();

        Func<Task> act = () => sut.GetPieByIdAsync(id);

        await act.Should().ThrowAsync<EntityPropertyFormatException<Pie>>();
    }

    [Fact]
    public async Task GetPieByIdAsync_WhenPieNotFound_ThrowsEntityNotFoundException()
    {
        _pieRepo.GetPieByIdAsync(5).Returns((Pie?)null);
        var sut = CreateSut();

        Func<Task> act = () => sut.GetPieByIdAsync(5);

        await act.Should().ThrowAsync<EntityNotFoundException<Pie>>();
    }

    [Fact]
    public async Task GetPieByIdAsync_WhenPieExists_ReturnsMappedDto()
    {
        var pie = new Pie { Id = 5, Name = "Apple Pie" };
        _pieRepo.GetPieByIdAsync(5).Returns(pie);
        _mapper.Map<PieDto>(pie).Returns(new PieDto { Id = 5, Name = "Apple Pie" });
        var sut = CreateSut();

        var result = await sut.GetPieByIdAsync(5);

        result.Should().NotBeNull();
        result!.Id.Should().Be(5);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public async Task GetPieByNameAsync_WithEmptyName_ThrowsEntityPropertyFormatException(string name)
    {
        var sut = CreateSut();

        Func<Task> act = () => sut.GetPieByNameAsync(name);

        await act.Should().ThrowAsync<EntityPropertyFormatException<Pie>>();
    }

    [Fact]
    public async Task GetPieByNameAsync_WhenNotFound_ReturnsNull()
    {
        _pieRepo.GetPieByNameAsync("Unknown").Returns((Pie?)null);
        var sut = CreateSut();

        var result = await sut.GetPieByNameAsync("Unknown");

        result.Should().BeNull();
    }

    [Fact]
    public async Task AddPieAsync_WithNull_ThrowsArgumentNullException()
    {
        var sut = CreateSut();

        Func<Task> act = () => sut.AddPieAsync(null!);

        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public async Task AddPieAsync_WithEmptyName_ThrowsEntityPropertyFormatException(string name)
    {
        var sut = CreateSut();

        Func<Task> act = () => sut.AddPieAsync(new PieDto { Name = name });

        await act.Should().ThrowAsync<EntityPropertyFormatException<Pie>>();
    }

    [Fact]
    public async Task AddPieAsync_WithDuplicateName_ThrowsEntityDuplicationException()
    {
        _pieRepo.GetPieByNameAsync("Apple Pie").Returns(new Pie { Id = 1, Name = "Apple Pie" });
        var sut = CreateSut();

        Func<Task> act = () => sut.AddPieAsync(new PieDto { Name = "Apple Pie" });

        await act.Should().ThrowAsync<EntityDuplicationException<Pie>>();
    }

    [Fact]
    public async Task AddPieAsync_WithValidPie_ReturnsMappedDto()
    {
        var dto = new PieDto { Name = "Cherry Pie" };
        var entity = new Pie { Id = 10, Name = "Cherry Pie" };

        _pieRepo.GetPieByNameAsync("Cherry Pie").Returns((Pie?)null);
        _mapper.Map<Pie>(dto).Returns(entity);
        _pieRepo.AddPieAsync(entity).Returns(1);
        _mapper.Map<PieDto>(entity).Returns(new PieDto { Id = 10, Name = "Cherry Pie" });
        var sut = CreateSut();

        var result = await sut.AddPieAsync(dto);

        result.Should().NotBeNull();
        result.Id.Should().Be(10);
        await _pieRepo.Received(1).AddPieAsync(entity);
    }

    [Fact]
    public async Task GetPiesCountAsync_ReturnsRepositoryCount()
    {
        _pieRepo.GetPiesCountAsync().Returns(42);
        var sut = CreateSut();

        var count = await sut.GetPiesCountAsync();

        count.Should().Be(42);
    }
}
