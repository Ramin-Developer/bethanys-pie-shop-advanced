namespace BethanysPieShop.Common.Constants;

public class EntityValues
{
    public static readonly string InvalidEntityIdError =
        //"{entityType} ID '{invalidId}' is invalid.";
        "Entity Type {0} with ID {1} is invalid.";

    public static readonly string NotFoundEntityIdError =
        //"{entityType} with ID '{entityId}' was not found.";
        "Entity Type {0} with ID {1} not found.";

    public static readonly string InvalidEntityNameError =
        //"{entityType} with name '{entityName}' was not found.";
        "Entity type {0} with name {1} not found.";

}
