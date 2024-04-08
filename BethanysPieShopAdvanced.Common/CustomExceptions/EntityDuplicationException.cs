namespace BethanysPieShop.Common.CustomExceptions;

public class EntityDuplicationException<TEntity>(string propertyName, string propertyValue) :
    Exception(EntityValues.DuplicateEntityError(typeof(TEntity).Name, propertyName, propertyValue))
    where TEntity : class
{
    public string EntityType { get; } = typeof(TEntity).Name;

    public string PropertyName { get; } = propertyName;

    public string PropertyValue { get; } = propertyValue;
}
