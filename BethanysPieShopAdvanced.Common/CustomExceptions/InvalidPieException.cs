namespace BethanysPieShop.Common.CustomExceptions;

public class InvalidPieException(string validationMessage, Exception innerException = null!)
    : CustomHandledException(validationMessage, GeneralValues.PieValidationError, innerException)
{}
