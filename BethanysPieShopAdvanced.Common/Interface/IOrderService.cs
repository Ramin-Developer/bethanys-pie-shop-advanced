namespace BethanysPieShop.Common.Interface;

public interface IOrderService
{
    Task<List<OrderDto>> GetOrdersWithOrderLinesAsync();

    Task<OrderDto?> GetOrderDetailsAsync(int? orderId);
}
