namespace BethanysPieShop.Common.CustomException;

public class NotFoundCategoryIdException : Exception
{
    // Constructor without parameters
    public NotFoundCategoryIdException()
    { }

    // Constructor with just a message
    public NotFoundCategoryIdException(string message)
        : base(message)
    { }

    // Constructor with a message and inner exception
    public NotFoundCategoryIdException(string message, Exception inner)
        : base(message, inner)
    { }

    // Constructor with a message and the not found category ID
    public NotFoundCategoryIdException(int? categoryId)
        : base(string.Format(CategoryValues.NonFoundCategoryIdError, categoryId))
    {
        CategoryId = categoryId;
    }

    public string CategoryName { get; } = string.Empty;

    public int? CategoryId { get; } 
}
