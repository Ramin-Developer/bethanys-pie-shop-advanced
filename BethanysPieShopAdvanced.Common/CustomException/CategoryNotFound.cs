namespace BethanysPieShop.Common.CustomException;

public class CategoryNotFoundException : Exception
{
    public int? CategoryId { get; }

    // Constructor without parameters
    public CategoryNotFoundException()
    {}

    // Constructor with just a message
    public CategoryNotFoundException(string message)
        : base(message)
    {}

    // Constructor with a message and inner exception
    public CategoryNotFoundException(string message, Exception inner)
        : base(message, inner)
    {}

    // Constructor with a message and the not found category ID
    public CategoryNotFoundException(string message, int? categoryId)
        : base(message)
    {
        CategoryId = categoryId;
    }
}
