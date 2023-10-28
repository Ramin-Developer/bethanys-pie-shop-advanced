namespace BethanysPieShop.Admin.CustomException;

public class InvalidPieException : CustomHandledException
{
    public InvalidPieException(string validationMessage, Exception innerException = null!)
        : base(validationMessage, GeneralValues.PieValidationError, innerException)
    { }
}
