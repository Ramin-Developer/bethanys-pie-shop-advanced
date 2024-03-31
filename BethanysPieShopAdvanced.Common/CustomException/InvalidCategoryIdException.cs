namespace BethanysPieShop.Common.CustomException;

public class InvalidCategoryIdException : Exception
{
    // Constructor without parameters
    public InvalidCategoryIdException()
    { }

    // Constructor with just a message
    public InvalidCategoryIdException(string message)
        : base(message)
    { }

    // Constructor with a message and inner exception
    public InvalidCategoryIdException(string message, Exception inner)
        : base(message, inner)
    { }

    // Constructor with a message and the invalid category ID
    public InvalidCategoryIdException(string message, int? categoryId)
        : base(message)
    {
        CategoryId = categoryId;
    }

    public int? CategoryId { get; }
}
