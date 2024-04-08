namespace BethanysPieShop.Common.CustomExceptions;

public class InvalidEntityIdException<TEntity>(int entityId) :
    Exception(EntityValues.InvalidEntityIdError(typeof(TEntity).Name, entityId)) where TEntity : class
{
    public string EntityType { get; } = typeof(TEntity).Name;

    public int EntityId { get; } = entityId;
}
