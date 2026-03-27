using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineShoppingApi.DTOs;
using OnlineShoppingApi.Interfaces;
using OnlineShoppingApi.Modules;
using OnlineShoppingApi.UnitOfWorks;

namespace OnlineShoppingApi.Services
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unit;

        public ProductService(IMapper mapper, IUnitOfWork unit)
        {
            _mapper = mapper;
            _unit = unit;
        }

        // Yeni məhsul yaratmaq
        public async Task<ProductDto> CreateAsync(CreateProductDto dto)
        {
            var product = _mapper.Map<Product>(dto);

            // Category və Brand əlavə et
            product.Category = await _unit.Categories.GetByIdAsync(dto.CategoryId);
            product.Brand = await _unit.Brands.GetByIdAsync(dto.BrandId);

            await _unit.Products.AddAsync(product);
            await _unit.SaveAsync();

            return _mapper.Map<ProductDto>(product);
        }

        // Məhsulu silmək
        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _unit.Products.GetByIdAsync(id);
            if (product == null) return false;

            _unit.Products.Delete(product);
            await _unit.SaveAsync();
            return true;
        }

        // Bütün məhsulları gətirmək
        public async Task<List<ProductDto>> GetAllAsync()
        {
            var products = await _unit.Products.Query()
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .ToListAsync();

            return _mapper.Map<List<ProductDto>>(products);
        }

        // ID-ə görə məhsulu gətirmək
        public async Task<ProductDto?> GetByIdAsync(int id)
        {
            var product = await _unit.Products.Query()
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null) return null;
            return _mapper.Map<ProductDto>(product);
        }

        // Məhsulu yeniləmək
        public async Task<ProductDto?> UpdateAsync(int id, UpdateProductDto dto)
        {
            var product = await _unit.Products.GetByIdAsync(id);
            if (product == null) return null;

            // DTO məlumatını məhsula tətbiq et
            _mapper.Map(dto, product);

            // Category və Brand update
            product.Category = await _unit.Categories.GetByIdAsync(dto.CategoryId);
            product.Brand = await _unit.Brands.GetByIdAsync(dto.BrandId);

            _unit.Products.Update(product);
            await _unit.SaveAsync();

            return _mapper.Map<ProductDto>(product);
        }
    }
}