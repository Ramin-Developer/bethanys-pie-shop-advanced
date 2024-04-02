namespace BethanysPieShop.Admin.CustomExceptions;

public class CustomHandledException(
    string customMessage,
    string userFriendlyMessage,
    Exception innerException = null!) : Exception(customMessage, innerException)
{
    public string UserFriendlyMessage { get; set; } = userFriendlyMessage;
}
