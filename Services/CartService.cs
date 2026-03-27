using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineShoppingApi.DTOs;
using OnlineShoppingApi.Interfaces;
using OnlineShoppingApi.Modules;
using OnlineShoppingApi.UnitOfWorks;

namespace OnlineShoppingApi.Services
{
    public class CartService : ICartService
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;

        public CartService(IUnitOfWork unit, IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }

        // İstifadəçinin səbətini gətirir
        public async Task<CartDto> GetUserCartAsync(int userId)
        {
            var cart = await _unit.Carts.Query()
          .Include(c => c.CartItems)
        .ThenInclude(ci => ci.Product)
            .ThenInclude(p => p.Category)
             .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
            {
                // Əgər cart yoxdursa, yeni cart yaradılır
                cart = new Cart { UserId = userId };
                await _unit.Carts.AddAsync(cart);
                await _unit.SaveAsync();
            }

            return _mapper.Map<CartDto>(cart);
        }

        // Məhsulu səbətə əlavə edir
        public async Task<CartDto?> AddToCartAsync(int userId, int productId, int quantity)
        {
            var cart = await _unit.Carts.Query()
                .Include(c => c.CartItems)
                .ThenInclude(c => c.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
            {
                cart = new Cart { UserId = userId };
                await _unit.Carts.AddAsync(cart);
            }

            var cartItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);
            if (cartItem != null)
            {
                cartItem.Quantity += quantity;
            }
            else
            {
                cart.CartItems.Add(new CartItem
                {
                    ProductId = productId,
                    Quantity = quantity
                });
            }

            await _unit.SaveAsync();
            return _mapper.Map<CartDto>(cart);
        }

        // Məhsulu səbətdən silir
        public async Task<bool> RemoveFromCartAsync(int userId, int productId)
        {
            var cart = await _unit.Carts.Query()
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null) return false;

            var item = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);
            if (item == null) return false;

            _unit.CartItems.Delete(item);
            await _unit.SaveAsync();
            return true;
        }

        // İstifadəçinin səbətini boşaldır
        public async Task<bool> ClearCartAsync(int userId)
        {
            var cart = await _unit.Carts.Query()
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null) return false;

            foreach (var item in cart.CartItems)
            {
                _unit.CartItems.Delete(item);
            }

            await _unit.SaveAsync();
            return true;
        }
    }
}