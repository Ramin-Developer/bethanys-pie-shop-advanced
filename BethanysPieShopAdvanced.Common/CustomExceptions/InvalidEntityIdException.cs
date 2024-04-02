namespace BethanysPieShop.Common.CustomExceptions;

public class InvalidEntityIdException(string entityType, int invalidId) :
    Exception(string.Format(EntityValues.InvalidEntityIdError, invalidId, invalidId))
{
    public int InvalidId { get; } = invalidId;

    public string EntityType { get; } = entityType;
}
