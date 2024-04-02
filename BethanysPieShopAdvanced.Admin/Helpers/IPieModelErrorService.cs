namespace BethanysPieShop.Admin.Helpers;

public interface IPieModelErrorService
{
    void HandleConcurrencyException(
        DbUpdateConcurrencyException ex,
        Pie? pieToUpdate,
        ModelStateDictionary modelState);

    IActionResult HandleException(Exception ex, string logFormat, Dictionary<string, object> args);

    void UpdateModelErrorMessages(
        Pie entityValues,
        Pie databaseValues,
        ModelStateDictionary modelState);
}
