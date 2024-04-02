namespace BethanysPieShop.Common.Utilities;

public static class JsonSettings
{
    public static JsonSerializerOptions JsonOptions => new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        IgnoreReadOnlyProperties = true,
        WriteIndented = true,
        ReferenceHandler = ReferenceHandler.IgnoreCycles
    };
}
