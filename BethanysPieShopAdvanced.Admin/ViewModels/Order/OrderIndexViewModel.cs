namespace BethanysPieShop.Admin.ViewModels.Order;

public class OrderIndexViewModel
{
    public IEnumerable<OrderDto> OrderDtoList { get; set; } = [];

    public IEnumerable<OrderLineDto> OrderLines { get; set; } = [];

    public IEnumerable<PieDto> Pies { get; set; } = [];

    public int? SelectedOrderId { get; set; }

    public int? SelectedOrderLineId { get; set; }
}
