namespace BethanysPieShop.Common.DTO;

// Todo: In DTOs used for creation remove the Id property.
// Todo: Consider creating several DTOs with descriptive names, e.g. AddCategoryDto.

public class CategoryDto
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

    public ICollection<PieDto> PieDtoList { get; set; } = new List<PieDto>();

    public string? ErrorMessage { get; set; } = string.Empty;

    public string? SuccessMessage { get; set; } = string.Empty;
}
