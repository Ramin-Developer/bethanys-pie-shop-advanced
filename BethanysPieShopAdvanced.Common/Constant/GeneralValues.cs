using System.Net.Http.Headers;

namespace BethanysPieShop.Common.Constant;

public static class GeneralValues
{
    public const string DynamicLogEventMessage = "Dynamic log event occurred: {Details}";
    public const string IdInValidError = "The ID parameter must have a value and it must be a positive integer.";
    public const string OccurredError = "An error occurred.";
    public const string ArgumentNullError = "Argument cannot be null.";
    public const string OperationError = "Operation type must be: 'Create', 'Update' or 'Delete'.";
    public const string GenericLogFormatError = "Something went wrong in: {methodName}";
    public const string InternalServerError = "An unhandled exception occurred. Please try again later.";

    public const string PieValidationError =
        "There was a problem validating the pie details. " +
        "Please check the inputs and try again.";

    public const string DatabaseName = "BethanysPieShopAdvancedDb";

    public const string ConnectionStringKey = "PieShopDbContextConnection";

    public const string JsonMediaType = "application/json";
}
