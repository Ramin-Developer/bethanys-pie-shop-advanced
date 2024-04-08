namespace BethanysPieShop.Common.CustomExceptions;

public class CategoryNotEmptyException : Exception
{
    public CategoryNotEmptyException() : base(CategoryValues.CategoryToDeleteNotEmpty)
    {
    }

    public int CategoryId { get; }
}
