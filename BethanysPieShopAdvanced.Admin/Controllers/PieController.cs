namespace BethanysPieShop.Admin.Controllers;

[Route("/[controller]/[action]/")]
public class PieController(
    ILogger<PieController> logger,
    IPieModelErrorService errorService,
    IMapper mapper,
    IPieService pieService,
    IPieHelperService pieHelperService) : BaseController<PieController>(logger, errorService)
{
    [HttpGet]
    public async Task<IActionResult> IndexAsync()
    {
        var pies = await _pieService
            .GetPiesAsync();

        return View(pies);
    }

    [HttpGet("{pageNumber:int?}")]
    public async Task<IActionResult> IndexPagingAsync(int? pageNumber)
    {
        var paginatedList = await _pieHelperService
            .CreatePaginatedSortedViewModel(pageNumber, PieGroupOption.Paging, PieSortOption.IdAsc);

        return View(paginatedList);
    }

    [HttpGet("{pageNumber:int?}/{sortOption}")]
    public async Task<IActionResult> IndexPagingSortingAsync(int? pageNumber, PieSortOption sortOption)
    {
        var paginatedList = await _pieHelperService
            .CreatePaginatedSortedViewModel(pageNumber, PieGroupOption.PagingSorting, sortOption);

        return View(paginatedList);
    }

    [HttpGet("{searchCategory:int?}/{searchQuery}")]
    public async Task<IActionResult> SearchAsync(int? searchCategory, string? searchQuery)
    {
        var viewModel = await _pieHelperService
            .CreatePieSearchViewModelAsync(searchQuery, searchCategory);

        return View(viewModel);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> DetailsAsync(int id)
    {
        var validationResult = ValidateId(id);
        var errorResult = HandleValidation(validationResult);
        if (errorResult != null)
            return errorResult;

        var pieDto = await _pieService
            .GetPieByIdAsync(id);

        var viewModel = new PieDetailsViewModel
        {
            PieDto = pieDto,
        };

        if (pieDto == null)
        {
            _logger.LogError(PieValues.NotFoundIdError);
            viewModel.ErrorMessage = $"{PieValues.NotFoundIdError}";

            return View(viewModel);
        }

        return View(viewModel);
    }

    [HttpGet]
    public async Task<IActionResult> AddAsync()
    {
        var pieAddViewModel = await _pieHelperService
            .CreatePieAddViewModelAsync();

        return View(pieAddViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddAsync(PieAddViewModel pieAddViewModel)
    {
        if (ModelState.IsValid)
        {
            // Convert PieAddViewModel directly to PieDto
            var pieDto = _mapper
                .Map<PieDto>(pieAddViewModel);

            await _pieService.AddPieAsync(pieDto);

            return RedirectToAction(nameof(Index));
        }

        await _pieHelperService
            .PopulateCategorySelectListAsync(pieAddViewModel);

        return View(pieAddViewModel);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> UpdateAsync(int id)
    {
        var validationResult = ValidateId(id);
        if (validationResult.IsValid == false)
        {
            var result = await _pieHelperService
                .CreatePieEditViewModelAsync(null, null, validationResult.ErrorMessage);

            return View(result);
        }

        var pieToUpdate = await _pieService
            .GetPieByIdAsync(id);

        if (pieToUpdate == null)
        {
            var result = await _pieHelperService
                .CreatePieEditViewModelAsync(null, null, PieValues.UpdateTargetNullError);

            return View(result);
        }

        var pieEditViewModel = await _pieHelperService
            .CreatePieEditViewModelAsync(pieToUpdate, pieToUpdate.CategoryId);

        return View(pieEditViewModel);
    }

    [HttpPost("{id:int}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateAsync(int id, PieEditViewModel pieEditViewModel)
    {
        if (id != pieEditViewModel.PieDto!.Id)
        {
            ModelState.AddModelError("", "There is an ID mismatch.");
            return View(pieEditViewModel);
        }

        if (ModelState.IsValid)
        {
            await _pieService.UpdatePieAsync(pieEditViewModel.PieDto!);

            return RedirectToAction(nameof(Index));
        }

        return View(pieEditViewModel);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var validationResult = ValidateId(id);
        var errorResult = HandleValidation(validationResult);
        if (errorResult != null)
            return errorResult;

        var selectedPie = await
            _pieService
            .GetPieByIdAsync(id);

        var viewModel = new PieDeleteViewModel
        {
            PieDto = selectedPie,
            ErrorMessage = string.Empty
        };

        return View(viewModel);
    }

    [HttpPost("{id:int}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmedAsync(int id)
    {
        var validationResult = ValidateId(id);
        var errorResult = HandleValidation(validationResult);
        if (errorResult != null)
            return errorResult;

        try
        {
            await _pieService
                .DeletePieAsync(id);

            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            // For debugging purposes only. Don't expose the actual error in production.
            return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    private void ValidatePieEditViewModel(PieEditViewModel pieEditVm)
    {
        _ = pieEditVm.PieDto
            ?? throw new InvalidPieException(PieValues.MissingPieError);

        if (ModelState.IsValid == false)
        {
            throw new InvalidPieException(PieValues.InvalidDataError);
        }
    }

    private readonly IPieService _pieService = pieService
            ?? throw new ArgumentNullException(nameof(pieService), GeneralValues.ArgumentNullError);

    private readonly IMapper _mapper = mapper
            ?? throw new ArgumentNullException(nameof(mapper), GeneralValues.ArgumentNullError);

    private readonly IPieHelperService _pieHelperService = pieHelperService
            ?? throw new ArgumentNullException(nameof(pieHelperService), GeneralValues.ArgumentNullError);
}
