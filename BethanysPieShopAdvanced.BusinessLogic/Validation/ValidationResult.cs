namespace BethanysPieShop.BusinessLogic.Validation;

public class ValidationResult
{
    public static ValidationResult Success() => new();

    public static ValidationResult Fail(string errorMessage) => new(errorMessage);

    public bool IsValid => ErrorMessage == string.Empty;

    public string ErrorMessage { get; }

    private ValidationResult(string errorMessage = "") => ErrorMessage = errorMessage;
}