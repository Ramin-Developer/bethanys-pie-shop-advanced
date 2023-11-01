namespace BethanysPieShop.Admin.ViewModels;

public class ErrorViewModel
{
    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

    public string? RequestId { get; set; }

    public string? ErrorMessage { get; set; }

    public string? Details { get; set; }
}
