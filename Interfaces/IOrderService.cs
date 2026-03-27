using OnlineShoppingApi.DTOs;

namespace OnlineShoppingApi.Interfaces
{
    public interface IOrderService
    {

        Task<bool> CreateOrderAsync(int userId);
        Task<List<OrderDto>> GetUserOrdersAsync(int userId);
        Task<bool> UpdateOrderStatusAsync(int orderId, string status);
    }
}