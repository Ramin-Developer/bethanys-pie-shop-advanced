namespace BethanysPieShop.UnitTests.Services;

public class CategoryServiceTests
{
    private readonly ICategoryRepository _categoryRepo = Substitute.For<ICategoryRepository>();
    private readonly IMapper _mapper = Substitute.For<IMapper>();

    private CategoryService CreateSut() => new(_categoryRepo, _mapper);

    [Fact]
    public async Task GetCategoriesAsync_ReturnsMappedList()
    {
        var categories = new List<Category>
        {
            new() { Id = 2, Name = "Cheese Cakes" },
            new() { Id = 1, Name = "Fruit Pies" },
        };
        _categoryRepo.GetCategoriesAsync().Returns(categories);
        _mapper.Map<List<CategoryDto>>(Arg.Any<object>())
            .Returns(
            [
                new CategoryDto { Id = 1, Name = "Fruit Pies" },
                new CategoryDto { Id = 2, Name = "Cheese Cakes" },
            ]);
        var sut = CreateSut();

        var result = await sut.GetCategoriesAsync();

        result.Should().HaveCount(2);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public async Task GetCategoryByIdAsync_WithNonPositiveId_ThrowsEntityPropertyFormatException(int id)
    {
        var sut = CreateSut();

        Func<Task> act = () => sut.GetCategoryByIdAsync(id);

        await act.Should().ThrowAsync<EntityPropertyFormatException<Category>>();
    }

    [Fact]
    public async Task GetCategoryByIdAsync_WhenNotFound_ThrowsEntityNotFoundException()
    {
        _categoryRepo.GetCategoryByIdAsync(5).Returns((Category?)null);
        var sut = CreateSut();

        Func<Task> act = () => sut.GetCategoryByIdAsync(5);

        await act.Should().ThrowAsync<EntityNotFoundException<Category>>();
    }

    [Fact]
    public async Task GetCategoryByIdAsync_WhenExists_ReturnsMappedDto()
    {
        var category = new Category { Id = 5, Name = "Seasonal Pies" };
        _categoryRepo.GetCategoryByIdAsync(5).Returns(category);
        _mapper.Map<CategoryDto>(category).Returns(new CategoryDto { Id = 5, Name = "Seasonal Pies" });
        var sut = CreateSut();

        var result = await sut.GetCategoryByIdAsync(5);

        result.Should().NotBeNull();
        result!.Id.Should().Be(5);
    }

    [Fact]
    public async Task FindCategoryByTypeAsync_WhenNotFound_ThrowsEntityNotFoundException()
    {
        _categoryRepo.FindCategoryByTypeAsync(CategoryType.FruitPies).Returns((Category?)null);
        var sut = CreateSut();

        Func<Task> act = () => sut.FindCategoryByTypeAsync(CategoryType.FruitPies);

        await act.Should().ThrowAsync<EntityNotFoundException<Category>>();
    }

    [Fact]
    public async Task FindCategoryByTypeAsync_WhenExists_ReturnsMappedDto()
    {
        var category = new Category { Id = 1, Name = "Fruit Pies" };
        _categoryRepo.FindCategoryByTypeAsync(CategoryType.FruitPies).Returns(category);
        _mapper.Map<CategoryDto>(category).Returns(new CategoryDto { Id = 1, Name = "Fruit Pies" });
        var sut = CreateSut();

        var result = await sut.FindCategoryByTypeAsync(CategoryType.FruitPies);

        result.Should().NotBeNull();
        result!.Name.Should().Be("Fruit Pies");
    }

    [Fact]
    public async Task GetCategioryCountAsync_ReturnsRepositoryCount()
    {
        _categoryRepo.GetCategoriesCountAsync().Returns(7);
        var sut = CreateSut();

        var count = await sut.GetCategioryCountAsync();

        count.Should().Be(7);
    }

    [Fact]
    public async Task UpdateCategoryAsync_WithNull_ThrowsArgumentNullException()
    {
        var sut = CreateSut();

        Func<Task> act = () => sut.UpdateCategoryAsync(null!);

        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task DeleteCategoryAsync_WhenCategoryNotFound_ThrowsEntityNotFoundException()
    {
        _categoryRepo.GetCategoryByIdAsync(99).Returns((Category?)null);
        var sut = CreateSut();

        Func<Task> act = () => sut.DeleteCategoryAsync(99);

        await act.Should().ThrowAsync<EntityNotFoundException<Category>>();
    }
}
