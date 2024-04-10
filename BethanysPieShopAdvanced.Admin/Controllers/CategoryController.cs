namespace BethanysPieShop.Admin.Controllers;

[Produces("application/json")]
[Route("/[controller]/[action]/")]
public class CategoryController(
    ILogger<CategoryController> logger,
    IPieModelErrorService errorService,
    ICategoryService categoryService,
    IMapper mapper) : BaseController<CategoryController>(logger, errorService)
{
    [HttpGet]
    public async Task<IActionResult> Index(string? successMessage)
    {
        var viewModel = new CategoryListViewModel
        {
            Categories = [.. (await _categoryService
                .GetCategoriesAsync())]
        };

        if (string.IsNullOrEmpty(successMessage) == false)
            viewModel
                .Categories
                .FirstOrDefault()
                !.SuccessMessage = successMessage;

        return View(viewModel);
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var selectedCategory = await _categoryService
            .GetCategoryByIdAsync(id);

        var result = _mapper
            .Map<DetailsCategoryDto>(selectedCategory);

        return View(result);
    }

    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Add(
        [Bind("Name", "Description", "DateAdded")] CategoryDto catDto)
    {
        if (ModelState.IsValid)
        {
            await _categoryService
                .AddCategoryAsync(catDto);

            return RedirectToAction(nameof(Index));
        }

        return View(catDto);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        var selectedCategory = await _categoryService
            .GetCategoryByIdAsync(id!.Value);

        return View(selectedCategory);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(CategoryDto catDto)
    {
        try
        {
            if (ModelState.IsValid == false)
                return View(catDto);

            await _categoryService
                .UpdateCategoryAsync(catDto);

            return RedirectToAction(nameof(Index));
        }
        catch
        {
            ModelState.AddModelError("", CategoryValues.UpdateLogError);

            return View(catDto);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var selectedCatDto = await _categoryService.GetCategoryByIdAsync(id);

        return View(selectedCatDto);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _categoryService.DeleteCategoryAsync(id);
        
        return RedirectToAction(nameof(Index), new { SuccessMessage = CategoryValues.DeleteSuccessMessage });
    }

    private readonly ICategoryService _categoryService = categoryService
            ?? throw new ArgumentNullException(nameof(categoryService), GeneralValues.ArgumentNullError);
    private readonly IMapper _mapper = mapper;
}
