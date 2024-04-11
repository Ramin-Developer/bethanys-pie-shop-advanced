namespace BethanysPieShop.Common.Constants;

public static class ApiEndPoints
{
    public const string BasePieEndpoint = "/api/pie";

    public const string BaseCategoryEndpoint = "/api/category";

    public const string AllPiesEndpoint = $"{BasePieEndpoint}/getall";

    public const string AllCategoriesEndpoint = $"{BaseCategoryEndpoint}/getall";

    public static string SinglePieEndpoint(int pieId) => $"{BasePieEndpoint}/{pieId}";

    public static string SingleCategoryEndpoint(int pieId) => $"{BaseCategoryEndpoint}/{pieId}";
    
    public const string CreatePieEndpoint = BasePieEndpoint;

    public const string CreateCategoryEndpoint = BaseCategoryEndpoint;

    public static string UpdatePieEndpoint(int pieId) => SinglePieEndpoint(pieId);

    public static string UpdateCategoryEndpoint(int pieId) => SingleCategoryEndpoint(pieId);

    public static string DeletePieEndpoint(int pieId) => SinglePieEndpoint(pieId);

    public static string DeleteCategoryEndpoint(int pieId) => SingleCategoryEndpoint(pieId);
}
