namespace BethanysPieShop.Common.Utilities;

public static class CategoryHelper
{
    public static string GetCategoryName(int categoryId) =>
        _categoriyNames.TryGetValue(categoryId, out var name)
        ? name 
        : string.Empty;

    private static readonly Dictionary<int, string> _categoriyNames =
        new ()
        {
            { 1, "Fruit Pies" },
            { 2, "Cheese Cakes" },
            { 3, "Seasonal Pies" }
        };
}
