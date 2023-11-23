namespace BethanysPieShop.Admin.Controllers;

// Todo: Use ValidateId() to check the item IDs. 

public class OrderController(
    ILogger<OrderController> logger,
    IOrderService orderService,
    IPieModelErrorService errorService) : BaseController<OrderController>(logger, errorService)
{
    [HttpGet]
    public async Task<IActionResult> Index(int? orderId, int? orderLineId)
    {
        var orderIndexVm = await BuildOrderIndexViewModel(orderId, orderLineId);

        return View(orderIndexVm);
    }

    [HttpGet]
    public async Task<IActionResult> Details(int orderId)
    {
        if (IsIdValid(orderId) == false)
            return BadRequest(OrderValues.NullIdError);

        var order = await _orderService
            .GetOrderDetailsAsync(orderId);

        return View(order);
    }

    private async Task<OrderIndexViewModel> BuildOrderIndexViewModel(int? orderId, int? orderLineId)
    {
        var orderIndexVm = new OrderIndexViewModel
        {
            OrderDtoList = (await _orderService
            .GetOrdersWithOrderLinesAsync())
            .ToList()
        };

        if (IsIdValid(orderId))
            SetSelectedOrder(orderIndexVm, orderId!.Value);

        if (IsIdValid(orderLineId) && orderIndexVm.OrderLines.Any())
            SetSelectedOrderLine(orderIndexVm, orderLineId!.Value);

        return orderIndexVm;
    }

    private void SetSelectedOrder(OrderIndexViewModel orderIndexVm, int orderId)
    {
        var selectedOrder = orderIndexVm
            .OrderDtoList
            .SingleOrDefault(o => o.Id == orderId);

        if (selectedOrder != null)
        {
            orderIndexVm.OrderLines = selectedOrder.OrderLineDtoList;
            orderIndexVm.SelectedOrderId = orderId;
        }
    }

    private void SetSelectedOrderLine(OrderIndexViewModel orderIndexVm, int orderLineId)
    {
        var selectedOrderLine = orderIndexVm.OrderLines.SingleOrDefault(ol => ol.Id == orderLineId);
        if (selectedOrderLine != null)
        {
            orderIndexVm.Pies = new List<PieDto> { selectedOrderLine.PieDto };
            orderIndexVm.SelectedOrderLineId = orderLineId;
        }
    }

    private readonly IOrderService _orderService = orderService
            ?? throw new ArgumentNullException(nameof(orderService), GeneralValues.ArgumentNullError);
}
