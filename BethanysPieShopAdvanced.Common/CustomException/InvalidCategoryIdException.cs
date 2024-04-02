namespace BethanysPieShop.Common.CustomException;

public class InvalidCategoryIdException : Exception
{
    // Constructor with a message and the invalid category ID
    public InvalidCategoryIdException(int invalidId)
        : base(string.Format(CategoryValues.InvalidCategoryIdError, invalidId))
    {
        InvalidId = invalidId;
    }

    // Constructor with a message and inner exception
    public InvalidCategoryIdException(string message, Exception inner = null!)
        : base(message, inner)
    {
        InvalidId = -1;
    }

    public int InvalidId { get; }
}
