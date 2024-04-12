namespace BethanysPieShop.Common.Constants;

public static class ApiPieEndPoints
{
    public const string BasePieEndpoint = "/api/pie";

    public const string AllPiesEndpoint = BasePieEndpoint;

    public static string SinglePieEndpoint(int pieId) => $"{BasePieEndpoint}/{pieId}";

    public const string CreatePieEndpoint = BasePieEndpoint;

    public static string UpdatePieEndpoint(int pieId) => SinglePieEndpoint(pieId);

    public static string DeletePieEndpoint(int pieId) => SinglePieEndpoint(pieId);
}
