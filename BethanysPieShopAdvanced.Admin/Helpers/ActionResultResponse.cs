namespace BethanysPieShop.Admin.Helpers;

public class ActionResultResponse
{
    public bool IsError { get; set; }

    public string ErrorMessage { get; set; } = string.Empty;
    
    public string ErrorDetails { get; set; } = string.Empty;
}
