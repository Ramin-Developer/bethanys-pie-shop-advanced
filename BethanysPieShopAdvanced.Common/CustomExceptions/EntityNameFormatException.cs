namespace BethanysPieShop.Common.CustomExceptions;

public class EntityNameFormatException<TEntity>(string entityName) :
    Exception(EntityValues.EntityNameFormatError(typeof(TEntity).Name, entityName)) where TEntity : class
{
    public string EntityType { get; } = typeof(TEntity).Name;

    public string EntityName { get; } = entityName;
}
