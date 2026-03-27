using OnlineShoppingApi.DTOs;

namespace OnlineShoppingApi.Interfaces
{
    public interface IUserService
    {

        Task<bool> RegisterAsync(RegisterUserDto dto);
        Task<string?> LoginAsync(LoginUserDto dto);
        Task<UserDto?> GetByIdAsync(int id);
    }
}