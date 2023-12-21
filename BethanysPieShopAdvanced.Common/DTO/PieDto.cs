namespace BethanysPieShop.Common.Dto;

public class PieDto
{
    public int Id { get; set; }

    [Display(Name = PieValues.NameDisplay)]
    [StringLength(PieValues.MaxNameLength, ErrorMessage = PieValues.InvalidName)]
    public string Name { get; set; } = string.Empty;

    [Display(Name = PieValues.ShortDescDisplay)]
    [StringLength(PieValues.MaxShortDescLength, ErrorMessage = PieValues.InvalidShortDesc)]
    public string? ShortDescription { get; set; }

    [Display(Name = PieValues.LongDescDisplay)]
    [StringLength(PieValues.MaxLongDescLength, ErrorMessage = PieValues.InvalidLongDesc)]
    public string? LongDescription { get; set; }

    [Display(Name = PieValues.AllergyInfoDisplay)]
    [StringLength(PieValues.MaxAllergyInfoLength, ErrorMessage = PieValues.InvalidAllergyInfo)]
    public string? AllergyInformation { get; set; }

    [Display(Name = PieValues.PriceDisplay)]
    public decimal Price { get; set; }

    [Display(Name = PieValues.ImageUrlDisplay)]
    public string? ImageUrl { get; set; }

    [Display(Name = "Image Thumbnail URL")]
    public string? ImageThumbnailUrl { get; set; }

    [Display(Name = PieValues.IsPieOfTheWeekDisplay)]
    public bool IsPieOfTheWeek { get; set; }

    [Display(Name = PieValues.InStockDisplay)]
    public bool InStock { get; set; }

    [Display(Name = PieValues.CategoryIdDisplay)]
    public int CategoryId { get; set; }

    [Display(Name = PieValues.CategoryNameDisplay)]
    public string CategoryName { get; set; } = string.Empty;
}
