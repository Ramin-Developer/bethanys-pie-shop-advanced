namespace BethanysPieShop.Common.CustomExceptions;

public class InvalidEntityNameException<TEntity>(string entityName) :
    Exception(EntityValues.InvalidEntityNameError(typeof(TEntity).Name, entityName)) where TEntity : class
{
    public string EntityType { get; } = typeof(TEntity).Name;

    public string EntityName { get; } = entityName;
}
