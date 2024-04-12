
namespace BethanysPieShop.Common.Constants;

public class EntityValues
{
    public static string EntityIdFormatError(string entityType, int entityId) =>
        $"The ID '{entityId}' in entity Type '{entityType}' is not correctly farmatted.";

    public static string EntityNameFormatError(string entityType, string entityName) =>
        $"The name '{entityName}' in entity type '{entityType}' is not correctly farmatted.";

    public static string EntityIdNotFoundError(string entityType, int entityId) =>
        $"The ID '{entityId}' of entity type '{entityType}' was not found.";

    public static string DuplicateEntityError(string entityType, string Property, string PropertyValue) =>
    $"The value '{PropertyValue}' of property '{Property}' already exists in '{entityType}'.";
}
