namespace BethanysPieShop.Admin.CustomExceptions;

public class InvalidPieException(string validationMessage, Exception innerException = null!)
    : CustomHandledException(validationMessage, GeneralValues.PieValidationError, innerException)
{}
