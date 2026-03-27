using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineShoppingApi.DTOs;
using OnlineShoppingApi.Interfaces;
using OnlineShoppingApi.Modules;
using OnlineShoppingApi.UnitOfWorks;

public class UserService : IUserService
{
    private readonly IUnitOfWork _uow;
    private readonly IJwtService _jwtService;
    private readonly IMapper _mapper;

    public UserService(IUnitOfWork uow, IJwtService jwtService, IMapper mapper)
    {
        _uow = uow;
        _jwtService = jwtService;
        _mapper = mapper;
    }

    public async Task<bool> RegisterAsync(RegisterUserDto dto)
    {
        var existUser = await _uow.Users.Query()
            .FirstOrDefaultAsync(x => x.Email == dto.Email);

        if (existUser != null)
            return false;

        var user = new User
        {
            FullName = dto.UserName,
            Email = dto.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            Role = "User",
            Cart = new Cart(),
            Orders = new List<Order>()
        };

        await _uow.Users.AddAsync(user);
        await _uow.SaveAsync();
        return true;
    }

    public async Task<string?> LoginAsync(LoginUserDto dto)
    {
        var user = await _uow.Users.Query()
            .FirstOrDefaultAsync(x => x.Email == dto.Email);

        if (user == null)
            return null;

        if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            return null;

        return await _jwtService.GenerateTokenAsync(user);
    }

    public async Task<UserDto?> GetByIdAsync(int id)
    {
        var user = await _uow.Users.Query()
            .FirstOrDefaultAsync(x => x.Id == id);

        if (user == null)
            return null;

        return _mapper.Map<UserDto>(user);
    }
}