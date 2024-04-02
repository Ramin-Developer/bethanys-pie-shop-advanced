namespace BethanysPieShop.Common.Constants;

public static class OrderValues
{
    public const string Status = "Order Status";

    public const string FirstNameDisplay = "First Name";
    public const int MaxFirstNameLength = 100;
    public const string InvalidFirstName = "First name cannot be more than 100 characters.";

    public const string LastNameDisplay = "Last Name";
    public const int MaxLastNameLength = 100;
    public const string InvalidLastName = "Last name cannot be more than 100 characters.";

    public const string AddressLineOneDisplay = "Address Line 1";
    public const int MaxAddressLineOneLength = 100;
    public const string InvalidAddressLineOne = "Address Line 1 cannot be more than 100 characters.";

    public const string AddressLineTwoDisplay = "Address Line 2";
    public const int MaxAddressLineTwoLength = 100;
    public const string InvalidAddressLineTwo = "Address Line 2 cannot be more than 100 characters.";

    public const string ZipCodeDisplay = "Zip Code";
    public const int MinZipCodeLength = 4;
    public const int MaxZipCodeLength = 10;
    public const string InvalidZipCodeLength = "Zip code must be in the interval [4, 10] chracters.";

    public const string CityDisplay = "City";
    public const int MaxCityLength = 100;
    public const string InvalidCityLength = "City cannot be more than 100 characters.";

    public const string StateDisplay = "State";
    public const int MaxStateLength = 100;
    public const string InvalidStateLength = "State cannot be more than 100 characters.";

    public const string CountryDisplay = "Country";
    public const int MaxCountryLength = 100;
    public const string InvalidCountryLength = "Country cannot be more than 100 characters.";

    public const string PhoneNumberDisplay = "Phone Number";
    public const int MaxPhoneNumberLength = 25;
    public const string InvalidPhoneNumberLength = "Phone number cannot be more than 25 characters.";

    public const string EmailDisplay = "Email";
    public const int MaxEmailLength = 25;
    public const string InvalidEmailLength = "Email cannot be more than 100 characters.";

    public const string OrderTotalDisplay = "Order Total";
    public const string OrderPlacedDisplay = "Order Placed";
    public const string OrderDateFormatDisplay = "{0:yyyy-MM-dd}";

    public const string GeneralError = "There was an error retrieving the order details.";
    public const string NullIdError = "Order ID cannot be null.";
    public const string DetailsFormatError = "Error retrieving details for order with ID: {orderId}";
}
