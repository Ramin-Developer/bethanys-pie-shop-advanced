namespace BethanysPieShop.Admin.Controllers;

// Todo: Use ValidateId() to check the item IDs. 

public class CategoryController : BaseController<CategoryController>
{
    public CategoryController(
        ILogger<CategoryController> logger,
        ICategoryService categoryService,
        IPieModelErrorService errorService) : base(logger, errorService)
    {
        _catService = categoryService
            ?? throw new ArgumentNullException(nameof(categoryService), GeneralValues.ArgumentNullError);
    }

    [HttpGet]
    public async Task<IActionResult> Index(string? successMessage)
    {
        var viewModel = new CategoryListViewModel
        {
            Categories = (await _catService
                .GetCategoriesAsync())
                .ToList()
        };

        if (string.IsNullOrEmpty(successMessage) == false)
            viewModel.Categories.FirstOrDefault()!.SuccessMessage = successMessage;

        return View(viewModel);
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        if (IsIdValid(id) == false)
            return HandleCategoryNotFound(nameof(Details), id);

        var selectedCategory = await _catService
            .GetCategoryByIdAsync(id);

        if (selectedCategory == null)
            return HandleCategoryNotFound(nameof(Details), id);

        return View(selectedCategory);
    }

    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Add([Bind("Name", "Description", "DateAdded")] CategoryDto catDto)
    {
        if (ModelState.IsValid)
        {
            await _catService
                .AddCategoryAsync(catDto);

            return RedirectToAction(nameof(Index));
        }

        return View(catDto);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if (IsIdValid(id) == false)
            return HandleCategoryNotFound(nameof(Edit), id);

        var selectedCategory = await _catService
            .GetCategoryByIdAsync(id!.Value);

        if (selectedCategory == null)
            return HandleCategoryNotFound(nameof(Edit), id);

        return View(selectedCategory);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(CategoryDto catDto)
    {
        try
        {
            if (ModelState.IsValid == false)
                return View(catDto);

            await _catService
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
        var selectedCatDto = await _catService
            .GetCategoryByIdAsync(id);

        selectedCatDto ??= new CategoryDto
        {
            ErrorMessage = CategoryValues.CategoryNotFoundError
        };

        return View(selectedCatDto);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int? id)
    {
        if (IsIdValid(id) == false)
        {
            var errorDto = new CategoryDto
            {
                ErrorMessage = CategoryValues.CategoryNotFoundError
            };

            return View(errorDto);
        }

        var selectedCat = await _catService
            .GetCategoryByIdAsync(id!.Value);

        if (selectedCat == null)
        {
            var errorDto = new CategoryDto
            {
                ErrorMessage = $"{CategoryValues.NonExistantCategoryIdError} {id.Value}"
            };

            return View(errorDto);
        }

        await _catService
            .DeleteCategoryAsync(id.Value);

        return RedirectToAction(nameof(Index),
            new
            {
                SuccessMessage = CategoryValues.DeleteSuccessMessage
            });
    }

    private IActionResult HandleCategoryNotFound(string methodName, int? categoryId)
    {
        var details = (categoryId.HasValue == false || categoryId.Value <= 0)
            ? CategoryValues.InvalidCategoryId
            : CategoryValues.NonExistantCategoryIdError.Replace("{categoryId}", categoryId.Value.ToString());

        var catErrorDto = new CategoryDto
        {
            ErrorMessage = $"Error in {methodName}: {details}",
        };

        return View("Delete", catErrorDto);
    }

    private readonly ICategoryService _catService;
}
