namespace BethanysPieShop.Common.Constant;

public static class ApiEndPoints
{
    public static string BaseEmployeeUrl = "api/pie";

    public static string SinglePieeUrl(int pieId) => $"api/pie/{pieId}";
}
