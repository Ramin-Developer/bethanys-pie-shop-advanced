namespace BethanysPieShop.Common.CustomExceptions;

public class CategoryNotEmptyException(int id) :
    Exception(CategoryValues.CategoryToDeleteNotEmpty(id))
{
    public int CategoryId { get; } = id;
}
