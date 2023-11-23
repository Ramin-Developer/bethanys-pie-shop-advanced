namespace BethanysPieShop.Admin.CustomException;

public class CustomHandledException(
    string customMessage,
    string userFriendlyMessage,
    Exception innerException = null!) : Exception(customMessage, innerException)
{
    public string UserFriendlyMessage { get; set; } = userFriendlyMessage;
}
