namespace BethanysPieShop.Common.CustomExceptions;

public class EntityIdFormatException<TEntity>(int entityId) :
    Exception(EntityValues.EntityIdFormatError(typeof(TEntity).Name, entityId)) where TEntity : class
{
    public string EntityType { get; } = typeof(TEntity).Name;

    public int EntityId { get; } = entityId;
}
