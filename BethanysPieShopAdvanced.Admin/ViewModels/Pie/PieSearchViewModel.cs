namespace BethanysPieShop.Admin.ViewModels.Pie;

public class PieSearchViewModel
{
    public IEnumerable<PieDto>? Pies { get; set; }

    public IEnumerable<SelectListItem>? Categories { get; set; }

    public string? SearchQuery { get; set; }

    public int? SearchCategory { get; set; }

    public bool IsNullOrEmpty => Pies == null || Pies.Any() == false;
}
