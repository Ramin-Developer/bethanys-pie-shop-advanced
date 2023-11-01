namespace BethanysPieShop.Common.Constant;

public static class PieValues
{
    public const string NameDisplay = "Name";
    public const int MaxNameLength = 100;
    public const string InvalidName = "Pie name cannot be more than 100 characters.";

    public const string ShortDescDisplay = "Short Description";
    public const int MaxShortDescLength = 100;
    public const string InvalidShortDesc = "Short description cannot be more than 100 characters.";

    public const string LongDescDisplay = "Long Description";
    public const int MaxLongDescLength = 1000;
    public const string InvalidLongDesc = "Long description cannot be more than 1000 characters.";

    public const string AllergyInfoDisplay = "Allergy Information";
    public const int MaxAllergyInfoLength = 1000;
    public const string InvalidAllergyInfo = "Allergy information cannot be more than 1000 characters.";

    public const string PriceDisplay = "Price";
    public const string ImageUrlDisplay = "Image URL";
    public const string IsPieOfTheWeekDisplay = "Is Pie Of The Week";
    public const string InStockDisplay = "In Stock";
    public const string CategoryIdDisplay = "Category ID";
    public const string CategoryNameDisplay = "Category Name";

    // Parameterized Constants
    public const string IdInValidFormetError = "The pie ID is not a positive integer: {pieId}";
    public const string DeletedSuccessfullyFormatMsg = "Pie with this ID deleted successfully: {pieId}";
    public const string DeleteFormatError = "Deleting the pie failed due to invalid ID: {pieId}";
    public const string FoundNameFormatError = "Pie with this ID already exists: {pieId}";
    public const string NoCounterpartEnumTypeError = "No counterpart defined for enum type: {enumType}.";

    // Simple Constants
    public const string MissingPieError = "The pie details are missing.";
    public const string NameInvalidError = "Pie name cannot be a null or an empty string.";
    public const string IdMismatchError = "Mismatch between URL id and pie ID."; 
    public const string UpdateTargetNullError = "Pie to update is null.";
    public const string NameDuplicatedError = "Another pie with the given name already exists.";
    public const string UpdateSourceNullError = "Pie source is null.";
    public const string UpdatePieIdNullError = "Pie Id is null.";
    public const string InvalidDataError = "Invalid pie data.";
    public const string DeletedPieError = "The pie was already deleted by another user.";

    public const string NotFoundIdError =
        "The requested pie was not found. " +
        "Please try again or select a different pie.";

    public const string ConcurrencyError =
            "The pie was already modified by another user. " +
            "The database values are now shown. Hit Save again to store these values.";

    public const int DefaultPageSize = 5;
    public const int DefaultPageNumber = 1;

    public const string LongDescriptionValue = "A Happy Birthday with This Pie! " +
        "Icing carrot cake jelly-o cheesecake. Sweet roll marzipan marshmallow toffee brownie brownie candy " +
        "tootsie roll. Chocolate cake gingerbread tootsie roll oat cake pie chocolate bar cookie dragee " +
        "brownie. Lollipop cotton candy cake bear claw oat cake. Dragee candy canes dessert tart. Marzipan " +
        "dragee gummies lollipop jujubes chocolate bar candy canes. Icing gingerbread chupa chups cotton candy " +
        "cookie sweet icing bonbon gummies. Gummies lollipop brownie biscuit danish chocolate cake Danish " +
        "powder cookie macaroon chocolate donut tart Carrot cake dragée croissant lemon drops liquorice lemon " +
        "drops cookie lollipop toffee. Carrot cake carrot cake liquorice sugar plum topping bonbon pie muffin " +
        "jujubes. Jelly pastry wafer tart caramels bear claw. Tiramisu tart pie cake danish lemon drops. " +
        "Brownie cupcake dragee gummies.";
}
