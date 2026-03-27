using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineShoppingApi.DTOs;
using OnlineShoppingApi.Interfaces;
using OnlineShoppingApi.Modules;
using OnlineShoppingApi.UnitOfWorks;

namespace OnlineShoppingApi.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;

        public OrderService(IUnitOfWork unit, IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }

        // Yeni order yaratmaq (cart-dan order-ə çevirir)
        public async Task<bool> CreateOrderAsync(int userId)
        {
            // İstifadəçinin cart-ini yükləyirik
            var cart = await _unit.Carts.Query()
                .Include(c => c.CartItems)
                    .ThenInclude(ci => ci.Product)
                        .ThenInclude(p => p.Category)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null || !cart.CartItems.Any()) return false;

            var order = new Order
            {
                UserId = userId,
                Status = OrderStatus.Pending,
                OrderItems = cart.CartItems.Select(ci => new OrderItem
                {
                    ProductId = ci.ProductId,
                    Quantity = ci.Quantity
                }).ToList()
            };

            await _unit.Orders.AddAsync(order);

            // Cart-i təmizləyirik
            foreach (var item in cart.CartItems)
                _unit.CartItems.Delete(item);

            await _unit.SaveAsync();
            return true;
        }

        // İstifadəçinin bütün order-larını gətirmək
        public async Task<List<OrderDto>> GetUserOrdersAsync(int userId)
        {
            var orders = await _unit.Orders.Query()
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                        .ThenInclude(p => p.Category)
                .Where(o => o.UserId == userId)
                .ToListAsync();

            return _mapper.Map<List<OrderDto>>(orders);
        }

        // Order statusunu yeniləmək
        public async Task<bool> UpdateOrderStatusAsync(int orderId, string status)
        {
            var order = await _unit.Orders.GetByIdAsync(orderId);
            if (order == null) return false;

            if (!Enum.TryParse<OrderStatus>(status, true, out var parsedStatus))
                return false; // səhv status verilibsə false qaytar

            order.Status = parsedStatus;
            _unit.Orders.Update(order);
            await _unit.SaveAsync();
            return true;
        }
    }
}