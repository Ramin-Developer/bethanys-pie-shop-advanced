namespace BethanysPieShop.Common.Constants;

public static class PieValues
{
    public const string NameDisplay = "Name";
    public const int MaxNameLength = 100;
    public const string InvalidName = "Pie name cannot be more than 100 characters.";
    public const string InvalidCategoryId = "A pie's category ID must be a positive whole number.";

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

    // Pies' Names
    public const string CaramelPopcornCheeseCake = "Caramel Popcorn Cheese Cake";
    public const string ChocolateCheeseCake = "Chocolate Cheese Cake";
    public const string PistacheCheeseCake = "Pistache Cheese Cake";
    public const string PecanPie = "Pecan Pie";
    public const string BirthdayPie = "Birthday Pie";
    public const string ApplePie = "Apple Pie";
    public const string BlueBerryCheeseCake = "BlueBerry Cheese Cake";
    public const string CheeseCake = "Cheese Cake";
    public const string CherryPie = "Cherry Pie";
    public const string ChristmasApplePie = "Christmas Apple Pie";
    public const string CranberryPie = "Cranberry Pie";
    public const string PeachPie = "Peach Pie";
    public const string PumpkinPie = "Pumpkin Pie";
    public const string RhubarbPie = "Rhubarb Pie";
    public const string StrawberryPie = "Strawberry Pie";
    public const string StrawberryCheeseCake = "Strawberry Cheese Cake";

    // Pies' Short Descriptions
    public const string CaramelPopcornCheeseCakeShortDesc = "The Ultimate Cheese Cake";
    public const string ChocolateCheeseCakeShortDesc = "The Chocolate Lover's Dream";
    public const string PistacheCheeseCakeShortDesc = "We're Going Nuts over This One";
    public const string PecanPieShortDesc = "More Pecan than You Can Handle!";
    public const string BirthdayPieShortDesc = "A Happy Birthday with This Pie!";
    public const string ApplePieShortDesc = "Our Famous Apple Pies!";
    public const string BlueBerryCheeseCakeShortDesc = "You'll Love It!";
    public const string CheeseCakeShortDesc = "Plain Cheese Cake, Plain Pleasure";
    public const string CherryPieShortDesc = "A Summer Classic!";
    public const string ChristmasApplePieShortDesc = "Happy Holidays with This Pie!";
    public const string CranberryPieShortDesc = "A Christmas Favorite";
    public const string PeachPieShortDesc = "Sweet as Peach";
    public const string PumpkinPieShortDesc = "Our Halloween Favorite";
    public const string RhubarbPieShortDesc = "My God, So Sweet!";
    public const string StrawberryPieShortDesc = "Our Delicious Strawberry Pie!";
    public const string StrawberryCheeseCakeShortDesc = "You'll Love It!";

    // Images and ImageThumnails of all Pies:

    // 1) CaramelPopcorn
    public const string CaramelPopcornCheeseCakeImage =
        "https://gillcleerenpluralsight.blob.core.windows.net/files/bethanyspieshop/cheesecakes/caramelpopcorncheesecake.jpg";

    public const string CaramelPopcornCheeseCakeImageThumbnail =
        "https://gillcleerenpluralsight.blob.core.windows.net/files/bethanyspieshop/cheesecakes/caramelpopcorncheesecakesmall.jpg";

    // 2) ChocolateCheeseCake
    public const string ChocolateCheeseCakeImage =
        "https://gillcleerenpluralsight.blob.core.windows.net/files/bethanyspieshop/cheesecakes/chocolatecheesecake.jpg";

    public const string ChocolateCheeseCakeImageThumbnail =
        "https://gillcleerenpluralsight.blob.core.windows.net/files/bethanyspieshop/cheesecakes/chocolatecheesecakesmall.jpg";

    // 3) PistacheCheeseCake
    public const string PistacheCheeseCakeImage =
        "https://gillcleerenpluralsight.blob.core.windows.net/files/bethanyspieshop/cheesecakes/pistachecheesecake.jpg";

    public const string PistacheCheeseCakeImageThumbnail =
        "https://gillcleerenpluralsight.blob.core.windows.net/files/bethanyspieshop/cheesecakes/pistachecheesecakesmall.jpg";

    // 4) PecanPie
    public const string PecanPieImage =
        "https://gillcleerenpluralsight.blob.core.windows.net/files/bethanyspieshop/fruitpies/pecanpie.jpg";

    public const string PecanPieImageThumbnail =
        "https://gillcleerenpluralsight.blob.core.windows.net/files/bethanyspieshop/fruitpies/pecanpiesmall.jpg";

    // 5) BirthdayPie
    public const string BirthdayPieImage =
        "https://gillcleerenpluralsight.blob.core.windows.net/files/bethanyspieshop/seasonal/birthdaypie.jpg";

    public const string BirthdayPieImageThumbnail =
        "https://gillcleerenpluralsight.blob.core.windows.net/files/bethanyspieshop/seasonal/birthdaypiesmall.jpg";

    // 6) ApplePie
    public const string ApplePieImage =
        "https://gillcleerenpluralsight.blob.core.windows.net/files/applepie.jpg";

    public const string ApplePieImageThumbnail =
        "https://gillcleerenpluralsight.blob.core.windows.net/files/applepiesmall.jpg";

    // 7) BlueBerryCheeseCake
    public const string BlueBerryCheeseCakeImage =
        "https://gillcleerenpluralsight.blob.core.windows.net/files/bethanyspieshop/cheesecakes/blueberrycheesecake.jpg";

    public const string BlueBerryCheeseCakeImageThumbnail =
        "https://gillcleerenpluralsight.blob.core.windows.net/files/bethanyspieshop/cheesecakes/blueberrycheesecakesmall.jpg";

    // 8) CheeseCake
    public const string CheeseCakeImage =
        "https://gillcleerenpluralsight.blob.core.windows.net/files/bethanyspieshop/cheesecakes/cheesecake.jpg";

    public const string CheeseCakeImageThumbnail =
        "https://gillcleerenpluralsight.blob.core.windows.net/files/bethanyspieshop/cheesecakes/cheesecakesmall.jpg";

    // 9) CherryPie
    public const string CherryPieImage =
        "https://gillcleerenpluralsight.blob.core.windows.net/files/cherrypie.jpg";

    public const string CherryPieImageThumbnail =
        "https://gillcleerenpluralsight.blob.core.windows.net/files/cherrypiesmall.jpg";

    // 10) ChristmasApplePie
    public const string ChristmasApplePieImage =
        "https://gillcleerenpluralsight.blob.core.windows.net/files/christmasapplepie.jpg";

    public const string ChristmasApplePieImageThumbnail =
        "https://gillcleerenpluralsight.blob.core.windows.net/files/christmasapplepiesmall.jpg";

    // 11) CranberryPie
    public const string CranberryPieImage =
        "https://gillcleerenpluralsight.blob.core.windows.net/files/cranberrypie.jpg";

    public const string CranberryPieImageThumbnail =
        "https://gillcleerenpluralsight.blob.core.windows.net/files/cranberrypiesmall.jpg";

    // 12) PeachPie
    public const string PeachPieImage =
        "https://gillcleerenpluralsight.blob.core.windows.net/files/peachpie.jpg";

    public const string PeachPieImageThumbnail =
        "https://gillcleerenpluralsight.blob.core.windows.net/files/peachpiesmall.jpg";

    // 13) PumpkinPie
    public const string PumpkinPieImage =
        "https://gillcleerenpluralsight.blob.core.windows.net/files/pumpkinpie.jpg";

    public const string PumpkinPieImageThumbnail =
        "https://gillcleerenpluralsight.blob.core.windows.net/files/pumpkinpiesmall.jpg";

    // 14) RhubarbPie
    public const string RhubarbPieImage =
        "https://gillcleerenpluralsight.blob.core.windows.net/files/rhubarbpiesmall.jpg";

    public const string RhubarbPieImageThumbnail =
        "https://gillcleerenpluralsight.blob.core.windows.net/files/rhubarbpie.jpg";

    // 15) StrawberryPie
    public const string StrawberryPieImage =
        "https://gillcleerenpluralsight.blob.core.windows.net/files/strawberrypie.jpg";

    public const string StrawberryPieImageThumbnail =
        "https://gillcleerenpluralsight.blob.core.windows.net/files/strawberrypiesmall.jpg";

    // 16) StrawberryCheeseCake
    public const string StrawberryCheeseCakeImage =
        "https://gillcleerenpluralsight.blob.core.windows.net/files/strawberrycheesecake.jpg";

    public const string StrawberryCheeseCakeImageThumbnail =
        "https://gillcleerenpluralsight.blob.core.windows.net/files/strawberrycheesecakesmall.jpg";

    public const string LongDescription = "A Happy Birthday with This Pie! " +
        "Icing carrot cake jelly-o cheesecake. Sweet roll marzipan marshmallow toffee brownie brownie candy " +
        "tootsie roll. Chocolate cake gingerbread tootsie roll oat cake pie chocolate bar cookie dragee " +
        "brownie. Lollipop cotton candy cake bear claw oat cake. Dragee candy canes dessert tart. Marzipan " +
        "dragee gummies lollipop jujubes chocolate bar candy canes. Icing gingerbread chupa chups cotton candy " +
        "cookie sweet icing bonbon gummies. Gummies lollipop brownie biscuit danish chocolate cake Danish " +
        "powder cookie macaroon chocolate donut tart Carrot cake dragée croissant lemon drops liquorice lemon " +
        "drops cookie lollipop toffee. Carrot cake carrot cake liquorice sugar plum topping bonbon pie muffin " +
        "jujubes. Jelly pastry wafer tart caramels bear claw. Tiramisu tart pie cake danish lemon drops. " +
        "Brownie cupcake dragee gummies.";

    public const string LongDesc = "Long Desc";
}
