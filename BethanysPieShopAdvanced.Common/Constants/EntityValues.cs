namespace BethanysPieShop.Common.Constants;

public static class EntityValues
{
    public static string EntityPropertyFormatError(string entityType, string propertyName, string propertyValue) =>
        $"The property '{entityType}.{propertyName}' has format error: '{propertyValue}'";

    public static string EntityIdNotFoundError(string entityType, int entityId) =>
        $"Entity type '{entityType}' with ID '{entityId}' was not found.";

    public static string DuplicateEntityError(string entityType, string Property, string PropertyValue) =>
    $"The value '{PropertyValue}' of property '{Property}' already exists in '{entityType}'.";
}
