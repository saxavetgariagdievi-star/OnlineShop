using OnlineShoppingApi.DTOs;

namespace OnlineShoppingApi.Interfaces
{
    public interface IBrandService
    {
        Task<List<BrandDto>> GetAllAsync();
        Task<BrandDto?> GetByIdAsync(int id);
        Task<BrandDto> CreateAsync(CreateBrandDto dto);
        Task<BrandDto?> UpdateAsync(int id, UpdateBrandDto dto);
        Task<bool> DeleteAsync(int id);
    }
}