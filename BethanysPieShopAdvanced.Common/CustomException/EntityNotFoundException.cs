namespace BethanysPieShop.Common.CustomException;

public class EntityNotFoundException : Exception
{
    public EntityNotFoundException(string entityType, int entityId)
        : base(string.Format(EntityValues.NotFoundEntityIdError, entityType, entityId))
    {
        EntityType = entityType;
        EntityName = string.Empty;
        EntityId = entityId;
    }

    // Constructor with a categoryName and inner exception
    public EntityNotFoundException(string entityType, string entityName)
        : base(string.Format(EntityValues.NotFoundEntityIdError, entityType, entityName))
    {
        EntityType = entityType;
        EntityName = entityName;
        EntityId = null;
    }

    public string EntityType { get; set; }

    public string EntityName { get; }

    public int? EntityId { get; }
}
