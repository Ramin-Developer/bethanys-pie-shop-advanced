namespace BethanysPieShop.Common.Entities;

public class Ingredient
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    [StringLength(100, ErrorMessage = "The amount should be no longer than 100 characters")]
    public string Amount { get; set; } = string.Empty;
}
