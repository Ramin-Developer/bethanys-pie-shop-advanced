namespace BethanysPieShop.Common.Interfaces;

public interface IOrderService
{
    Task<List<OrderDto>> GetOrdersWithOrderLinesAsync();

    Task<OrderDto?> GetOrderDetailsAsync(int orderId);
}
