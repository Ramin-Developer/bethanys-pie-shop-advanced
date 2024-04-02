namespace BethanysPieShop.Common.CustomException;

public class CategoryNotFoundException : Exception
{
    // Constructor with a categoryName and the not found category ID
    public CategoryNotFoundException(int categoryId)
        : base(string.Format(CategoryValues.NotFoundCategoryIdError, categoryId))
    {
        CategoryId = categoryId;
        CategoryName = string.Empty;
    }

    // Constructor with just a categoryName
    public CategoryNotFoundException(string categoryName)
        : base(string.Format(CategoryValues.InvalidCategoryNameError, categoryName))
    {
        CategoryId = null;
        CategoryName = categoryName;
    }

    // Constructor with a categoryName and inner exception
    public CategoryNotFoundException(string message, Exception inner = null!)
        : base(message, inner)
    {
        CategoryId = null;
        CategoryName = string.Empty;
    }

    public int? CategoryId { get; }

    public string CategoryName { get; }
}
