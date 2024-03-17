namespace BethanysPieShop.Common.Constant;

public static class ApiEndPoints
{
    public const string BasePieEndpoint = "/api/pie";

    public const string AllPiesEndpoint = $"{BasePieEndpoint}/getall";

    public static string SinglePieEndpoint(int pieId) => $"{BasePieEndpoint}/{pieId}";
    
    public const string CreatePieEndpoint = BasePieEndpoint;

    public static string UpdatePieEndpoint(int pieId) => SinglePieEndpoint(pieId);

    public static string DeletePieEndpoint(int pieId) => SinglePieEndpoint(pieId);
}
