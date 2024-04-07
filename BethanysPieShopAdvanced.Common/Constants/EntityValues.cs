namespace BethanysPieShop.Common.Constants;

public class EntityValues
{
    public static string InvalidEntityIdError(string entityType, int entityId) =>
        $"Entity Type '{entityType}' with ID '{entityId}' is invalid.";

    public static string NotFoundEntityIdError(string entityType, int entityId) =>
        $"Entity Type '{entityType}' with ID '{entityId}' not found.";

    public static string InvalidEntityNameError(string entityType, string entityName) =>
        $"Entity type '{entityType}' with name '{entityName}' not found.";

    public static string DuplicateEntityError(string entityType, string Property, string PropertyValue) =>
    $"Entity property '{Property}' with value '{PropertyValue}' already exists in '{entityType}'.";
}
