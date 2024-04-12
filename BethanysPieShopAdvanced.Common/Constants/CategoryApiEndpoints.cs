namespace BethanysPieShop.Common.Constants;

public class CategoryApiEndpoints
{
    public const string BaseCategoryEndpoint = "/api/category";

    public const string AllCategoriesEndpoint = BaseCategoryEndpoint;

    public static string UpdateCategoryEndpoint(int pieId) => SingleCategoryEndpoint(pieId);

    public static string SingleCategoryEndpoint(int pieId) => $"{BaseCategoryEndpoint}/{pieId}";

    public const string CreateCategoryEndpoint = BaseCategoryEndpoint;

    public static string DeleteCategoryEndpoint(int pieId) => SingleCategoryEndpoint(pieId);
}
