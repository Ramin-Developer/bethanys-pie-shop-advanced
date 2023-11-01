namespace BethanysPieShop.Admin.ViewModels.Order;

public class OrderIndexViewModel
{
    public IEnumerable<OrderDto> OrderDtoList { get; set; } = new List<OrderDto>();

    public IEnumerable<OrderLineDto> OrderLines { get; set; } = new List<OrderLineDto>();

    public IEnumerable<PieDto> Pies { get; set; } = new List<PieDto>();

    public int? SelectedOrderId { get; set; }

    public int? SelectedOrderLineId { get; set; }
}
