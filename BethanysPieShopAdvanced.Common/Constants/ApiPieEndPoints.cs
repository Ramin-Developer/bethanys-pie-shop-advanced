namespace BethanysPieShop.Common.Constants;

public static class ApiPieEndPoints
{
    public const string BasePieEndpoint = "/api/pie";

    public const string AllPiesEndpoint = BasePieEndpoint;

    public static string SinglePieEndpoint(int id) => $"{BasePieEndpoint}/{id}";

    public const string CreatePieEndpoint = BasePieEndpoint;

    public static string UpdatePieEndpoint(int id) => SinglePieEndpoint(id);

    public static string DeletePieEndpoint(int id) => SinglePieEndpoint(id);
}
