namespace BethanysPieShop.Admin.CustomException;

public class InvalidPieException(string validationMessage, Exception innerException = null!)
    : CustomHandledException(validationMessage, GeneralValues.PieValidationError, innerException)
{}
