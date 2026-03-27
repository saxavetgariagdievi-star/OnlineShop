using Microsoft.EntityFrameworkCore;
using OnlineShoppingApi.Data;
using OnlineShoppingApi.Interfaces;
using OnlineShoppingApi.Modules;

namespace OnlineShoppingApi.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task AddAsync(T entity)
        => await _dbSet.AddAsync(entity);

        public void Delete(T entity)
        => _dbSet.Remove(entity);

        public async Task<List<T>> GetAllAsync()
        =>
            await _dbSet.ToListAsync();


        public async Task<T> GetByIdAsync(int id)
        =>
            await _dbSet.FindAsync(id);

        public IQueryable<T> Query()
        {
            return _dbSet.AsQueryable();
        }

        public void Update(T entity)
        =>
            _dbSet.Update(entity);

    }
}