namespace BethanysPieShop.Common.Dto;


public class OrderLineDto
{
    [Display(Name = OrderLineValues.OrderLineIdDisplay)]
    public int Id { get; set; }

    [Display(Name = OrderLineValues.OrderIdDisplay)]
    public int OrderId { get; set; }

    [Display(Name = OrderLineValues.PieIdDisplay)]
    public int PieId { get; set; }

    [Display(Name = OrderLineValues.AmountDisplay)]
    [Range(OrderLineValues.MinAmount, OrderLineValues.MaxAmount, ErrorMessage = OrderLineValues.InvalidAmountError)]
    public int Amount { get; set; }

    [Display(Name = OrderLineValues.AmountDisplay)]
    [Range(OrderLineValues.MinPrice, OrderLineValues.MaxPrice, ErrorMessage = OrderLineValues.InvalidPriceError)]
    public decimal Price { get; set; }

    public PieDto PieDto { get; set; } = default!;

    public OrderDto OrderDto { get; set; } = default!;
}
