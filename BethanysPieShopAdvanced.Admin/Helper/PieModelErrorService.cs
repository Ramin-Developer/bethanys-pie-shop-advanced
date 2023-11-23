namespace BethanysPieShop.Admin.Helper;

public class PieModelErrorService(ILogger<PieModelErrorService> logger) : IPieModelErrorService
{
    public IActionResult HandleException(Exception ex, string logFormat, Dictionary<string, object> args)
    {
        // Log with structured logging
        _logger.LogError(ex, GeneralValues.OccurredError);

        // Convert named placeholders to indexed placeholders
        var indexedFormat = logFormat;
        var values = new List<object>();
        int index = 0;

        foreach (var kvp in args)
        {
            // The first pair of curly braces '{}' is related to string interpolation.
            // To include a literal brace in an interpolated string, you double it up.
            // So, {{ is a way to say "I really want to include a '{' in the final string."
            indexedFormat = indexedFormat.Replace($"{{{kvp.Key}}}", $"{{{index++}}}");
            values.Add(kvp.Value);
        }

        // Log the structured message separately
        var details = string.Format(indexedFormat, values.ToArray());
        _logger.LogError(GeneralValues.DynamicLogEventMessage, details);

        var errorVm = new ErrorViewModel
        {
            ErrorMessage = ex.Message,
            Details = string.Format(indexedFormat, values.ToArray())
        };

        return new ObjectResult(errorVm)
        {
            StatusCode = (int)HttpStatusCode.InternalServerError
        };
    }

    public void HandleConcurrencyException(
        DbUpdateConcurrencyException ex,
        Pie? pieToUpdate,
        ModelStateDictionary modelState)
    {
        var exceptionPie = ex.Entries.Single();
        var entityValues = (Pie)exceptionPie.Entity;
        var databasePie = exceptionPie.GetDatabaseValues();

        if (databasePie == null)
        {
            modelState.AddModelError(string.Empty, PieValues.DeletedPieError);
            return;
        }

        var databaseValues = (Pie)databasePie.ToObject();
        UpdateModelErrorMessages(entityValues, databaseValues, modelState);

        modelState.AddModelError(string.Empty, PieValues.ConcurrencyError);
        pieToUpdate!.RowVersion = databaseValues.RowVersion;

        modelState.Remove("Pie.RowVersion");
    }

    public void UpdateModelErrorMessages(
        Pie entityValues,
        Pie databaseValues,
        ModelStateDictionary modelState)
    {
        if (databaseValues.Name != entityValues.Name)
            modelState.AddModelError("Pie.Name", $"Current value: {databaseValues.Name}");

        if (databaseValues.Price != entityValues.Price)
            modelState.AddModelError("Pie.Price", $"Current value: {databaseValues.Price:c}");

        if (databaseValues.ShortDescription != entityValues.ShortDescription)
            modelState.AddModelError("Pie.ShortDescription", $"Current value: {databaseValues.ShortDescription}");

        if (databaseValues.LongDescription != entityValues.LongDescription)
            modelState.AddModelError("Pie.LongDescription", $"Current value: {databaseValues.LongDescription}");

        if (databaseValues.AllergyInformation != entityValues.AllergyInformation)
            modelState.AddModelError("Pie.AllergyInformation", $"Current value: {databaseValues.AllergyInformation}");

        if (databaseValues.ImageThumbnailUrl != entityValues.ImageThumbnailUrl)
            modelState.AddModelError("Pie.ImageThumbnailUrl", $"Current value: {databaseValues.ImageThumbnailUrl}");

        if (databaseValues.ImageUrl != entityValues.ImageUrl)
            modelState.AddModelError("Pie.ImageUrl", $"Current value: {databaseValues.ImageUrl}");

        if (databaseValues.IsPieOfTheWeek != entityValues.IsPieOfTheWeek)
            modelState.AddModelError("Pie.IsPieOfTheWeek", $"Current value: {databaseValues.IsPieOfTheWeek}");

        if (databaseValues.InStock != entityValues.InStock)
            modelState.AddModelError("Pie.InStock", $"Current value: {databaseValues.InStock}");

        if (databaseValues.CategoryId != entityValues.CategoryId)
            modelState.AddModelError("Pie.CategoryId", $"Current value: {databaseValues.CategoryId}");
    }

    private readonly ILogger<PieModelErrorService> _logger = logger
            ?? throw new ArgumentNullException(nameof(logger), GeneralValues.ArgumentNullError);
}
