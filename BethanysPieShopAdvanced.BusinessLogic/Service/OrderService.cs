namespace BethanysPieShop.BusinessLogic.Service;

// Todo: Add validation rules to the methods.
public class OrderService(IOrderRepository orderRepo, IMapper mapper) : IOrderService
{
    public async Task<List<OrderDto>> GetOrdersWithOrderLinesAsync()
    {
        var result = (await
            _orderRepo
            .GetOrdersWithOrderLinesAsync())
            .ToList();

        return _mapper
            .Map<List<OrderDto>>(result);
    }

    public async Task<OrderDto?> GetOrderDetailsAsync(int? orderId)
    {
        var result = await _orderRepo.GetOrderDetailsAsync(orderId);

        return _mapper.Map<OrderDto>(result);
    }

    private readonly IOrderRepository _orderRepo = orderRepo
            ?? throw new ArgumentNullException(nameof(orderRepo), GeneralValues.ArgumentNullError);

    private readonly IMapper _mapper = mapper
            ?? throw new ArgumentNullException(nameof(mapper), GeneralValues.ArgumentNullError);
}
