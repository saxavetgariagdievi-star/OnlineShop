using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineShoppingApi.DTOs;
using OnlineShoppingApi.Interfaces;
using OnlineShoppingApi.Modules;
using OnlineShoppingApi.UnitOfWorks;

namespace OnlineShoppingApi.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _ofWork;

        public CategoryService(IMapper mapper, IUnitOfWork ofWork)
        {
            _mapper = mapper;
            _ofWork = ofWork;
        }

        public async Task<CategoryDto> CreateAsync(CreateCategoryDto dto)
        {
            var categories = _mapper.Map<Category>(dto);
            await _ofWork.Categories.AddAsync(categories);
            await _ofWork.SaveAsync();
            return _mapper.Map<CategoryDto>(categories);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var categories = await _ofWork.Categories.GetByIdAsync(id);
            if (categories == null) return false;
            _ofWork.Categories.Delete(categories);
            await _ofWork.SaveAsync();
            return false;
        }

        public async Task<List<CategoryDto>> GetAllAsync()
        {
            var categories = await _ofWork.Categories.Query().Include(c => c.Products).ToListAsync();
            return _mapper.Map<List<CategoryDto>>(categories);
        }

        public async Task<CategoryDto?> GetByIdAsync(int id)
        {
            var categories = await _ofWork.Categories.Query()
            .Include(c => c.Products).FirstOrDefaultAsync(x => x.Id == id);
            if (categories == null) return null;
            return _mapper.Map<CategoryDto>(categories);
        }

        public async Task<CategoryDto?> UpdateAsync(int id, UpdateCategoryDto dto)
        {
            var categories = await _ofWork.Categories.GetByIdAsync(id);
            if (categories == null) return null;
            _mapper.Map(dto, categories);
            _ofWork.Categories.Update(categories);
            await _ofWork.SaveAsync();
            return _mapper.Map<CategoryDto>(categories);
        }
    }
}