namespace BethanysPieShop.Common.Constant;

public static class OrderLineValues
{
    public const string OrderLineIdDisplay = "Order Line ID";
    public const string OrderIdDisplay = "Order ID";
    public const string PieIdDisplay = "Pie ID";

    public const string AmountDisplay = "Quantity";
    public const int MinAmount = 1;
    public const int MaxAmount = int.MaxValue;
    public const string InvalidAmountError = "Amount must be a positive value.";

    public const string PriceDisplay = "Unit Price";
    public const double MinPrice = 0;
    public const double MaxPrice = (double)decimal.MaxValue;
    public const string InvalidPriceError = "Price must be a non-negative value.";

    public static string InvalidPriceErrorModified => $"Price must be a double in the interval: [{MinPrice}, {MaxPrice}].";
}
