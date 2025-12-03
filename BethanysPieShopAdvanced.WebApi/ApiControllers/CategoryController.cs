namespace BethanysPieShop.WebApi.ApiControllers;

[Route("api/[controller]")]
[ApiController]
[Produces(GeneralValues.JsonMediaType)]
public class CategoryController(ILogger<Category> logger, ICategoryService categoryService) : ControllerBase
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
    [HttpGet("{id:int}")]
    public async Task<ActionResult<CategoryDto>> GetByIdAsync(int id)
    {
        var categoryDto = await _categoryService
            .GetCategoryByIdAsync(id);
        if (categoryDto is null)
            return NotFound();
        return Ok(categoryDto);
    }

    // Post: /api/category
    [HttpPost]
    public async Task<ActionResult<CategoryDto>> CreateAsync([FromBody] CategoryDto categoryDto)
    {
        if (ModelState.IsValid == false)
            return BadRequest(ModelState);

        categoryDto.Id = 0;
        var newId = await _categoryService.UpdateCategoryAsync(categoryDto);
        if (newId <= 0) return StatusCode(StatusCodes.Status500InternalServerError, "Error creating new category record.");

        var created = await _categoryService.GetCategoryByIdAsync(newId);
        return CreatedAtAction(nameof(GetByIdAsync), new { id = newId }, created);
    }

    // Put: /api/category/{id}
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateAsync(int id, [FromBody] CategoryDto categoryDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        categoryDto.Id = id;
        await _categoryService.UpdateCategoryAsync(categoryDto);
        return NoContent();
    }

    // Delete: /api/category/{id}
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        if (id <= 0)
            return BadRequest(GeneralValues.InvalidIdError);

        var existing = await _categoryService.GetCategoryByIdAsync(id);
        if (existing is null)
            return NotFound();

        await _categoryService.DeleteCategoryAsync(id);
        return NoContent();
    }

    private readonly ILogger<Category> _logger = logger
        ?? throw new ArgumentNullException(nameof(logger), GeneralValues.ArgumentNullError);
    private readonly ICategoryService _categoryService = categoryService
        ?? throw new ArgumentNullException(nameof(categoryService), GeneralValues.ArgumentNullError);
}
