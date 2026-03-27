using OnlineShoppingApi.Modules;

namespace OnlineShoppingApi.Interfaces
{
    public interface IJwtService
    {
        Task<string> GenerateTokenAsync(User user);
    }
}