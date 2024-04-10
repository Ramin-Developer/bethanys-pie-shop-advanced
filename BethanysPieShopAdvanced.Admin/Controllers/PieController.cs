namespace BethanysPieShop.Admin.Controllers;

[Produces("application/json")]
[Route("/[controller]/[action]/")]
public class PieController(
    ILogger<PieController> logger,
    IPieModelErrorService errorService,
    IMapper mapper,
    IPieService pieService,
    IPieHelperService pieHelperService) : BaseController<PieController>(logger, errorService)
{
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var pies = await _pieService
            .GetPiesAsync();

        return View(pies);
    }

    [HttpGet]
    public async Task<IActionResult> IndexPaging(int? pageNumber)
    {
        var paginatedList = await _pieHelperService
            .CreatePaginatedSortedViewModel(pageNumber, PieGroupOption.Paging, PieSortOption.IdAsc);

        return View(paginatedList);
    }

    [HttpGet]
    public async Task<IActionResult> IndexPagingSorting(int? pageNumber, PieSortOption sortOption)
    {
        var paginatedList = await _pieHelperService
            .CreatePaginatedSortedViewModel(pageNumber, PieGroupOption.PagingSorting, sortOption);

        return View(paginatedList);
    }

    [HttpGet]
    public async Task<IActionResult> Search(int? searchCategory, string? searchQuery)
    {
        var viewModel = await _pieHelperService
            .CreatePieSearchViewModelAsync(searchQuery, searchCategory);

        return View(viewModel);
    }

    [HttpGet]
    public async Task<IActionResult> Details(int? id)
    {
        var validationResult = ValidateId(id);
        var errorResult = HandleValidation(validationResult);
        if (errorResult != null)
            return errorResult;

        var pieDto = await _pieService
            .GetPieByIdAsync(id!.Value);

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
    public async Task<IActionResult> Add()
    {
        var pieAddViewModel = await _pieHelperService
            .CreatePieAddViewModelAsync();

        return View(pieAddViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Add(PieAddViewModel pieAddViewModel)
    {
        if (ModelState.IsValid)
        {
            // Convert PieAddViewModel directly to PieDto
            var pieDto = _mapper
                .Map<PieDto>(pieAddViewModel);

            await _pieService.AddPieAsync(pieDto);

            return RedirectToAction(nameof(Index));
        }

        // There were error(s) in the submission, please correct them and try again.
        await _pieHelperService
            .PopulateCategorySelectListAsync(pieAddViewModel);

        return View(pieAddViewModel);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        var validationResult = ValidateId(id);
        if (validationResult.IsValid == false)
        {
            var result = await _pieHelperService
                .CreatePieEditViewModelAsync(null, null, validationResult.ErrorMessage);

            return View(result);
        }

        var pieToUpdate = await _pieService
            .GetPieByIdAsync(id!.Value);

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

    [HttpPost]
    public async Task<IActionResult> Edit(PieEditViewModel pieEditViewModel)
    {
        if (ModelState.IsValid)
        {
            await _pieService.UpdatePieAsync(pieEditViewModel.PieDto!);

            return RedirectToAction(nameof(Index));
        }

        return View(pieEditViewModel);
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int? id)
    {
        var validationResult = ValidateId(id);
        var errorResult = HandleValidation(validationResult);
        if (errorResult != null)
            return errorResult;

        var selectedPie = await
            _pieService
            .GetPieByIdAsync(id!.Value);

        var viewModel = new PieDeleteViewModel
        {
            PieDto = selectedPie,
            ErrorMessage = string.Empty
        };

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var validationResult = ValidateId(id);
        var errorResult = HandleValidation(validationResult);
        if (errorResult != null)
            return errorResult;

        try
        {
            await _pieService
                .DeletePieAsync(id!.Value);

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
