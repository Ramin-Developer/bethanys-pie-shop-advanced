namespace BethanysPieShop.Common.CustomExceptions;

public class InvalidEntityIdException(string entityType, int invalidId) :
    Exception(string.Format(EntityValues.InvalidEntityIdError, entityType, invalidId))
{
    public string EntityType { get; } = entityType;

    public int InvalidId { get; } = invalidId;
}
