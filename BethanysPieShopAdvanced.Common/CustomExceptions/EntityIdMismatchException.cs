namespace BethanysPieShop.Common.CustomExceptions;

public class EntityIdMismatchException<TEntity>(int id) :
    Exception(EntityValues.EntityIdMismatchError(typeof(TEntity).Name, id))
    where TEntity : class
{
    public string EntityType { get; } = typeof(TEntity).Name;

    public int Id { get; } = id;
}