namespace BethanysPieShop.Admin.Controllers;

[Route("/[controller]/[action]/")]
public class CategoryController(
    ILogger<CategoryController> logger,
    IPieModelErrorService errorService,
    ICategoryService categoryService,
    IMapper mapper) : BaseController<CategoryController>(logger, errorService)
{
    // Todo: Ask ChatGPT how to configure method attribute for this one.
    [HttpGet]
    public async Task<IActionResult> IndexAsync(string? successMessage)
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

    [HttpGet("{id:int}")]
    public async Task<IActionResult> DetailsAsync(int id)
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
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddAsync(
        [Bind("Name", "Description", "DateAdded")] CategoryDto catDto)
    {
        if (ModelState.IsValid)
        {
            await _categoryService
                .AddCategoryAsync(catDto);

            return RedirectToAction(nameof(IndexAsync));
        }

        return View(catDto);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> EditAsync(int id)
    {
        var selectedCategory = await _categoryService
            .GetCategoryByIdAsync(id);

        return View(selectedCategory);
    }

    [HttpPost]
    public async Task<IActionResult> EditAsync(CategoryDto catDto)
    {
        try
        {
            if (ModelState.IsValid == false)
                return View(catDto);

            await _categoryService
                .UpdateCategoryAsync(catDto);

            return RedirectToAction(nameof(IndexAsync));
        }
        catch
        {
            ModelState.AddModelError("", CategoryValues.UpdateLogError);

            return View(catDto);
        }
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

        return RedirectToAction(nameof(IndexAsync), new { SuccessMessage = CategoryValues.DeleteSuccessMessage });
    }

    private readonly ICategoryService _categoryService = categoryService
            ?? throw new ArgumentNullException(nameof(categoryService), GeneralValues.ArgumentNullError);
    private readonly IMapper _mapper = mapper;
}
