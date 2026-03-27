using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineShoppingApi.DTOs;
using OnlineShoppingApi.Interfaces;
using OnlineShoppingApi.Modules;
using OnlineShoppingApi.UnitOfWorks;

namespace OnlineShoppingApi.Services
{
    public class BrandService : IBrandService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unit;

        public BrandService(IMapper mapper, IUnitOfWork unit)
        {
            _mapper = mapper;
            _unit = unit;
        }

        // Yeni brand yaratmaq
        public async Task<BrandDto> CreateAsync(CreateBrandDto dto)
        {
            var brand = _mapper.Map<Brand>(dto);
            await _unit.Brands.AddAsync(brand);
            await _unit.SaveAsync();
            return _mapper.Map<BrandDto>(brand);
        }

        // Brand silmək
        public async Task<bool> DeleteAsync(int id)
        {
            var brand = await _unit.Brands.GetByIdAsync(id);
            if (brand == null) return false;

            _unit.Brands.Delete(brand);
            await _unit.SaveAsync();
            return true;
        }

        // Bütün brand-ları gətirmək
        public async Task<List<BrandDto>> GetAllAsync()
        {
            var brands = await _unit.Brands.Query() // <-- Query() istifadə olunur
                .ToListAsync();

            return _mapper.Map<List<BrandDto>>(brands);
        }

        // ID-ə görə brand gətirmək
        public async Task<BrandDto?> GetByIdAsync(int id)
        {
            var brand = await _unit.Brands.Query()
                .FirstOrDefaultAsync(b => b.Id == id);

            if (brand == null) return null;
            return _mapper.Map<BrandDto>(brand);
        }

        // Brand yeniləmək
        public async Task<BrandDto?> UpdateAsync(int id, UpdateBrandDto dto)
        {
            var brand = await _unit.Brands.GetByIdAsync(id);
            if (brand == null) return null;

            _mapper.Map(dto, brand);
            _unit.Brands.Update(brand);
            await _unit.SaveAsync();

            return _mapper.Map<BrandDto>(brand);
        }
    }
}