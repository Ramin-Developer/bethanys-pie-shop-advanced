namespace BethanysPieShop.Common.Dto;

public class DetailsCategoryDto
{
    public int Id { get; set; }

    [Required]
    [Display(Name = CategoryValues.NameDisplay)]
    [StringLength(CategoryValues.MaxNameLength, ErrorMessage = CategoryValues.InvalidNameLength)]
    public string Name { get; set; } = string.Empty;

    [Display(Name = CategoryValues.DescDisplay)]
    [StringLength(CategoryValues.MaxDescLength, ErrorMessage = CategoryValues.InvalidDesc)]
    public string? Description { get; set; }

    [Required]
    [Display(Name = CategoryValues.DateAddedDisplay)]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = CategoryValues.DateFormatString, ApplyFormatInEditMode = true)]
    public DateTime DateAdded { get; set; }

    public ICollection<PieDto> PieList { get; set; } = [];

    public string? ErrorMessage { get; set; } = string.Empty;

    public string? SuccessMessage { get; set; } = string.Empty;
}
