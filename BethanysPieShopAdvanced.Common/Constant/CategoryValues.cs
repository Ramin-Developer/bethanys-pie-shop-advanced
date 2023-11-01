namespace BethanysPieShop.Common.Constant;

public static class CategoryValues
{
    public const string NameDisplay = "Name";
    public const int MaxNameLength = 100;
    public const string InvalidNameLength = "Category name cannot be more than 100 characters.";

    public const string DescDisplay = "Description";
    public const int MaxDescLength = 1000;
    public const string InvalidDesc = "Description cannot be more than 1000 characters.";

    public const string DateAddedDisplay = "Date Added";
    public const string DateFormatString = "{0:yyyy-MM-dd}";

    public const string InvalidRequestError = "Invalid Request.";
    public const string IdNullError = "ID is required for delete operation.";
    public const string IdInvalidError = "Category ID must be present, as well as being a positive whole number.";
    public const string NameDuplicatedError = "Another category with the given name already exists.";
    public const string UpdateTargetNullError = "Category to update is null.";
    public const string UpdateLogError = "There was a problem updating the category. Please try again.";

    public const string InvalidCategoryId = "Category ID was not provided or is invalid.";

    public const string NonExistantCategoryIdError = "The following category ID was not found: {categoryId}";

    public const string NonEmptyError =
        "There are some pies in this category. Delete all of them before deleting the category.";

    public const string AddLogFormatError =
        "Adding the category failed in {methodName} with the message: {message}";

    public const string DeleteSuccessMessage = "Category deleted Successfully.";
    public const string CategoryNotFoundError = "No Category with the given name found.";
}
