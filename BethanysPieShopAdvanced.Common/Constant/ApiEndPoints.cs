namespace BethanysPieShop.Common.Constant;

public static class ApiEndPoints
{
    public static string BasePieUrl = "api/pie";

    public static string AllPiesUrl = "api/pie";

    public static string SinglePieUrl(int pieId) => $"api/pie/{pieId}";
}
