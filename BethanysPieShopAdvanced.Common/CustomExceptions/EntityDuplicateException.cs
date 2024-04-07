namespace BethanysPieShop.Common.CustomExceptions;

public class EntityDuplicateException(string entityType, string propertyName, string propertyValue) :
    Exception(EntityValues.DuplicateEntityError(entityType, propertyName, propertyValue))
{
    public string EntityType { get; } = entityType;

    public string PropertyName { get; } = propertyName;

    public string PropertyValue { get; } = propertyValue;
}
