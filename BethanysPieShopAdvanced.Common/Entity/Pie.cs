namespace BethanysPieShop.Common.Entity;

public class Pie
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? ShortDescription { get; set; }

    public string? LongDescription { get; set; }

    public string? AllergyInformation { get; set; }

    public decimal Price { get; set; }

    public string? ImageUrl { get; set; }

    public string? ImageThumbnailUrl { get; set; }

    public bool IsPieOfTheWeek { get; set; }

    public bool InStock { get; set; }

    public int CategoryId { get; set; }

    public Category? Category { get; set; }

    public ICollection<Ingredient> Ingredients { get; set; } = new List<Ingredient>();

    public byte[]? RowVersion { get; set; }
}
