namespace BethanysPieShop.Common.CustomExceptions;

public class InvalidEntityNameException(string entityType, string entityName) :
    Exception(EntityValues.InvalidEntityNameError(entityType, entityName))
{
    public string EntityType { get; } = entityType;

    public string EntityName { get; } = entityName;
}
