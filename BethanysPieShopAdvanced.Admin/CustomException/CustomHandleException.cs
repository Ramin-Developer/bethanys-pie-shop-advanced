namespace BethanysPieShop.Admin.CustomException;

public class CustomHandledException : Exception
{
    public CustomHandledException(
        string customMessage,
        string userFriendlyMessage,
        Exception innerException = null!) : base(customMessage, innerException)
    {
        UserFriendlyMessage = userFriendlyMessage;
    }

    public string UserFriendlyMessage { get; set; }
}
