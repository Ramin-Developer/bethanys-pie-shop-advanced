namespace BethanysPieShop.Common.CustomExceptions;

public class EntityPropertyFormatException<TEntity>(string propertyName, string propertyValue) : Exception(
        EntityValues.EntityPropertyFormatError(
            typeof(TEntity).Name,
            propertyName,
            propertyValue.ToString())) where TEntity : class
{
    public string EntityType { get; } = typeof(TEntity).Name;

    public string PropertyName { get; } = propertyName;

    public object PropertyValue { get; } = propertyValue;
}
