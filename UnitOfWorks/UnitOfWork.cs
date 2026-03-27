namespace OnlineShoppingApi.UnitOfWorks
{
    using System.Linq;
    using OnlineShoppingApi.Data;
    using OnlineShoppingApi.Interfaces;
    using OnlineShoppingApi.Modules;
    using OnlineShoppingApi.Repositories;

    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public IGenericRepository<User> Users { get; }
        public IGenericRepository<Product> Products { get; }
        public IGenericRepository<Category> Categories { get; }
        public IGenericRepository<Brand> Brands { get; }
        public IGenericRepository<Cart> Carts { get; }
        public IGenericRepository<Order> Orders { get; }

        public IGenericRepository<CartItem> CartItems { get; }

        public IGenericRepository<OrderItem> OrderItems { get; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            OrderItems = new GenericRepository<OrderItem>(context);
            CartItems = new GenericRepository<CartItem>(context);
            Users = new GenericRepository<User>(context);
            Products = new GenericRepository<Product>(context);
            Categories = new GenericRepository<Category>(context);
            Brands = new GenericRepository<Brand>(context);
            Carts = new GenericRepository<Cart>(context);
            Orders = new GenericRepository<Order>(context);
        }

        public void Dispose()
            => _context.Dispose();

        public async Task<int> SaveAsync()
            => await _context.SaveChangesAsync();

        public IQueryable<T> Query<T>() where T : class
         => _context.Set<T>().AsQueryable();
    }
}