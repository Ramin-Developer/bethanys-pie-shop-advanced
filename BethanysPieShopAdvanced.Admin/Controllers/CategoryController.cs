namespace BethanysPieShop.Admin.Controllers;

[Route("/[controller]/[action]/")]
public class CategoryController(
    ILogger<CategoryController> logger,
    IPieModelErrorService errorService,
    ICategoryService categoryService,
    IMapper mapper) : BaseController<CategoryController>(logger, errorService)
{
    [HttpGet]
    public async Task<IActionResult> IndexAsync(string? successMessage)
    {
        var viewModel = new CategoryListViewModel
        {
            Categories = await _categoryService
                .GetCategoriesAsync()
        };

        if (string.IsNullOrEmpty(successMessage) == false)
            viewModel
                .Categories
                .FirstOrDefault()
                !.SuccessMessage = successMessage;

        return View(viewModel);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> DetailsAsync(int id)
    {
        var selectedCategory = await _categoryService
            .GetCategoryByIdAsync(id);

        var result = _mapper
            .Map<DetailsCategoryDto>(selectedCategory);

        return View(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> EditAsync(int id)
    {
        var selectedCategory = await _categoryService
            .GetCategoryByIdAsync(id);

        if (selectedCategory == null)
        {
            // Handle not found scenario, maybe redirect or show an error message
            return NotFound();
        }

        return View(selectedCategory);
    }

    [HttpPost("{id:int}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditAsync(int id, CategoryDto categoryDto)
    {
        if (id != categoryDto.Id)
        {
            ModelState.AddModelError("", CategoryValues.IdMisMatchError);

            return View(categoryDto);
        }

        if (ModelState.IsValid)
        {
            await _categoryService
                .UpdateCategoryAsync(categoryDto);

            return RedirectToAction(nameof(Index));
        }

        return View(categoryDto);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var selectedCatDto = await _categoryService.GetCategoryByIdAsync(id);

        return View(selectedCatDto);
    }

    [HttpPost("{id:int}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmedAsync(int id)
    {
        await _categoryService.DeleteCategoryAsync(id);

        return RedirectToAction(nameof(IndexAsync),
            new { SuccessMessage = CategoryValues.DeleteSuccessMessage });
    }

    private readonly ICategoryService _categoryService = categoryService
            ?? throw new ArgumentNullException(nameof(categoryService), GeneralValues.ArgumentNullError);
    private readonly IMapper _mapper = mapper;
}
