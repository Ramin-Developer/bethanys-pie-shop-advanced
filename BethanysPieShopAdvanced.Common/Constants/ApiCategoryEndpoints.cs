namespace BethanysPieShop.Common.Constants;

public class ApiCategoryEndpoints
{
    public const string BaseCategoryEndpoint = "/api/category";

    public const string AllCategoriesEndpoint = BaseCategoryEndpoint;

    public static string SingleCategoryIdEndpoint(int id) => $"{BaseCategoryEndpoint}/{id}";

    public const string CreateCategoryEndpoint = BaseCategoryEndpoint;

    public static string UpdateCategoryEndpoint(int id) => SingleCategoryIdEndpoint(id);

    public static string DeleteCategoryEndpoint(int id) => SingleCategoryIdEndpoint(id);
}
