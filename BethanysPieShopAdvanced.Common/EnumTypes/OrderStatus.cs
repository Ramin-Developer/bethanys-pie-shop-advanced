namespace BethanysPieShop.Common.EnumTypes;

public enum OrderStatus
{
    [Display(Name = "Received")]
    Received,

    [Display(Name = "Paid")]
    Paid,

    [Display(Name = "Processing")]
    Processing,

    [Display(Name = "Out For Delivery")]
    OutForDelivery,

    [Display(Name = "Delivered")]
    Delivered
}
