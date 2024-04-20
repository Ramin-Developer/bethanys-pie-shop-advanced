namespace BethanysPieShop.Common.CustomExceptions;

public class EntityNotFoundException<TEntity> :
    Exception where TEntity : class
{
    public EntityNotFoundException(int entityId)
        : base(EntityValues.EntityIdNotFoundError(typeof(TEntity).Name, entityId))
    {
        EntityType = typeof(TEntity).Name;
        EntityId = entityId;
    }

    public EntityNotFoundException(string entityName)
        : base(EntityValues.EntityPropertyFormatError(typeof(TEntity).Name, "Name", entityName))
    {
        EntityType = typeof(TEntity).Name;
        EntityName = entityName;
    }

    public string EntityType { get; set; }

    public string EntityName { get; } = string.Empty;

    public int? EntityId { get; }
}
