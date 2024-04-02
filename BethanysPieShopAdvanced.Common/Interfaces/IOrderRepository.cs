namespace BethanysPieShop.Common.Interfaces;

public interface IOrderRepository
{
    Task<IEnumerable<Order>> GetOrdersWithOrderLinesAsync();
 
    Task<Order?> GetOrderDetailsAsync(int? order);
}
