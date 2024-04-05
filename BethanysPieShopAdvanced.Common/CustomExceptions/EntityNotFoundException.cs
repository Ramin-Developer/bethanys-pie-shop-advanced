namespace BethanysPieShop.Common.CustomExceptions;

public class EntityNotFoundException : Exception
{
    public EntityNotFoundException(string entityType, int entityId)
        : base(EntityValues.NotFoundEntityIdError(entityType, entityId))
    {
        EntityType = entityType;
        EntityName = string.Empty;
        EntityId = entityId;
    }

    public EntityNotFoundException(string entityType, string entityName)
        : base(EntityValues.InvalidEntityNameError(entityType, entityName))
    {
        EntityType = entityType;
        EntityName = entityName;
        EntityId = null;
    }

    public string EntityType { get; set; }

    public string EntityName { get; }

    public int? EntityId { get; }
}
