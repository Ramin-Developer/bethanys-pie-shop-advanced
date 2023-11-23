namespace BethanysPieShop.Common.DTO;

public class OrderDto
{
    public int Id { get; set; }

    public ICollection<OrderLineDto> OrderLineDtoList { get; set; } = new List<OrderLineDto>();

    [Display(Name = OrderValues.Status)]
    public OrderStatus OrderStatus { get; set; }

    [Required]
    [Display(Name = OrderValues.FirstNameDisplay)]
    [StringLength(OrderValues.MaxFirstNameLength, ErrorMessage = OrderValues.InvalidFirstName)]
    public string FirstName { get; set; } = string.Empty;

    [Display(Name = OrderValues.LastNameDisplay)]
    [StringLength(OrderValues.MaxLastNameLength, ErrorMessage = OrderValues.InvalidLastName)]
    public string LastName { get; set; } = string.Empty;

    [Required]
    [Display(Name = OrderValues.AddressLineOneDisplay)]
    [StringLength(OrderValues.MaxAddressLineOneLength, ErrorMessage = OrderValues.InvalidAddressLineOne)]
    public string AddressLine1 { get; set; } = string.Empty;

    [Display(Name = OrderValues.AddressLineTwoDisplay)]
    [StringLength(OrderValues.MaxAddressLineTwoLength, ErrorMessage = OrderValues.InvalidAddressLineTwo)]
    public string? AddressLine2 { get; set; }

    [Required]
    [Display(Name = OrderValues.ZipCodeDisplay)]
    [StringLength(OrderValues.MaxZipCodeLength, MinimumLength = OrderValues.MinZipCodeLength,
     ErrorMessage = OrderValues.InvalidZipCodeLength)]
    public string ZipCode { get; set; } = string.Empty;

    [Required]
    [Display(Name = OrderValues.CityDisplay)]
    [StringLength(OrderValues.MaxCityLength, ErrorMessage = OrderValues.InvalidCityLength)]
    public string City { get; set; } = string.Empty;

    [Required]
    [Display(Name = OrderValues.StateDisplay)]
    [StringLength(OrderValues.MaxStateLength, ErrorMessage = OrderValues.InvalidStateLength)]
    public string? State { get; set; }

    [Required]
    [Display(Name = OrderValues.CountryDisplay)]
    [StringLength(OrderValues.MaxCountryLength, ErrorMessage = OrderValues.InvalidCountryLength)]
    public string Country { get; set; } = string.Empty;

    [Required]
    [Display(Name = OrderValues.PhoneNumberDisplay)]
    [StringLength(OrderValues.MaxPhoneNumberLength, ErrorMessage = OrderValues.InvalidPhoneNumberLength)]
    public string PhoneNumber { get; set; } = string.Empty;

    [Required]
    [Display(Name = OrderValues.EmailDisplay)]
    [StringLength(OrderValues.MaxEmailLength, ErrorMessage = OrderValues.InvalidEmailLength)]
    public string Email { get; set; } = string.Empty;

    [Display(Name = OrderValues.OrderTotalDisplay)]
    [ScaffoldColumn(false)]
    public decimal OrderTotal { get; set; }

    [Required]
    [Display(Name = OrderValues.OrderPlacedDisplay)]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = OrderValues.OrderDateFormatDisplay, ApplyFormatInEditMode = true)]
    public DateTime OrderPlaced { get; set; }
}
