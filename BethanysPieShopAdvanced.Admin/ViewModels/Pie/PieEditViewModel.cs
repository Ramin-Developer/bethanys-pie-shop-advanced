namespace BethanysPieShop.Admin.ViewModels.Pie;

public class PieEditViewModel
{
    public IEnumerable<SelectListItem>? Categories { get; set; } = default!;

    public PieDto? PieDto { get; set; }

    public string? ErrorMessage { get; set; }
}
