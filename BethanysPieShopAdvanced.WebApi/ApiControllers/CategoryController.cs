namespace BethanysPieShop.WebApi.ApiControllers;

[Route("api/[controller]")]
[ApiController]
[Produces(GeneralValues.JsonMediaType)]
public class CategoryController(ILogger<Category> logger, ICategoryService categoryService) : Controller
{
    // Get: /api/category
    [HttpGet]
    public async Task<ActionResult<List<CategoryDto>>> GetAllAsync()
    {
        var categoryDtoList = await _categoryService
            .GetCategoriesAsync();

        return Ok(categoryDtoList);
    }

    // Get: /api/category/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryDto>> GetByIdAsync(int id)
    {
        var categoryDto = await _categoryService
            .GetCategoryByIdAsync(id);

        return Ok(categoryDto);
    }

    private readonly ILogger<Category> _logger = logger;
    private readonly ICategoryService _categoryService = categoryService;
}
