namespace BethanysPieShop.Admin.Controllers;

public abstract class BaseController<T>(ILogger logger, IPieModelErrorService errorService)
    : Controller where T : Controller
{
    protected IActionResult HandleError(Exception ex, string methodName, PieEditViewModel pieEditVm = null!)
    {
        IActionResult HandleInvalidException(InvalidPieException invalidPieEx)
        {
            ModelState.AddModelError(string.Empty, invalidPieEx.UserFriendlyMessage);
            
            return View(pieEditVm);
        }

        var actionResult = _errorService.HandleException(ex, GeneralValues.GenericLogFormatError,
            new Dictionary<string, object> { { "methodName", methodName } });

        return ex switch
        {
            PieNotFoundException notFoundEx => NotFound(notFoundEx.UserFriendlyMessage),
            InvalidPieException invalidEx => HandleInvalidException(invalidEx),
            CustomHandledException customEx => RedirectToAction("Error", nameof(HomeController), new
            {
                ErrorMessage = $"Error: {methodName} - {customEx.Message}",
                Details = customEx.UserFriendlyMessage
            }),
            _ => actionResult
        };
    }

    protected IActionResult HandleValidation(ValidationResult validationResult) =>
        validationResult.IsValid == false
            ? NotFound(validationResult.ErrorMessage)
            : null!;

    protected ValidationResult ValidateId(int? id) =>
        id.HasValue == false || id.Value <= 0
        ? ValidationResult.Fail(GeneralValues.IdInValidError)
        : ValidationResult.Success();

    public bool IsIdValid(int? id) => id.HasValue && id.Value > 0;

    protected readonly ILogger _logger = logger
            ?? throw new ArgumentNullException(nameof(logger), GeneralValues.ArgumentNullError);
    
    protected readonly IPieModelErrorService _errorService = errorService
            ?? throw new ArgumentNullException(nameof(errorService), GeneralValues.ArgumentNullError);
}
