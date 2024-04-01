namespace BethanysPieShop.Common.CustomException;

public class CategoryNotFoundException : Exception
{
    // Constructor without parameters
    public CategoryNotFoundException()
    { }

    // Constructor with just a categoryName
    public CategoryNotFoundException(string categoryName)
        : base(categoryName)
    {
        CategoryId = null;
        CategoryName = categoryName;
    }

    // Constructor with a categoryName and inner exception
    public CategoryNotFoundException(string message, Exception inner)
        : base(message, inner)
    { }

    // Constructor with a categoryName and the not found category ID
    public CategoryNotFoundException(int? categoryId)
        : base(string.Format(CategoryValues.NonFoundCategoryIdError, categoryId))
    {
        CategoryId = categoryId;
    }

    public int? CategoryId { get; }

    public string CategoryName { get; } = string.Empty;
}
