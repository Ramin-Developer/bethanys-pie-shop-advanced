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

    private readonly ILogger<Category> _logger = logger;
    private readonly ICategoryService _categoryService = categoryService;
}
