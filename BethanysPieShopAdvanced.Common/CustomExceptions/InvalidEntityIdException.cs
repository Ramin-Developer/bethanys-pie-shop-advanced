namespace BethanysPieShop.Common.CustomExceptions;

public class InvalidEntityIdException(string entityType, int entityId) :
    Exception(EntityValues.InvalidEntityIdError(entityType, entityId))
{
    public string EntityType { get; } = entityType;

    public int EntityId { get; } = entityId;
}
