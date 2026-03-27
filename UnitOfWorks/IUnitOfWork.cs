namespace OnlineShoppingApi.UnitOfWorks
{
    using OnlineShoppingApi.Interfaces;
    using OnlineShoppingApi.Modules;
    using OnlineShoppingApi.Repositories;

    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<User> Users { get; }
        IGenericRepository<Product> Products { get; }
        IGenericRepository<Category> Categories { get; }
        IGenericRepository<Brand> Brands { get; }
        IGenericRepository<CartItem> CartItems { get; }
        IGenericRepository<OrderItem> OrderItems { get; }
        IGenericRepository<Cart> Carts { get; }
        IGenericRepository<Order> Orders { get; }

        Task<int> SaveAsync();
        IQueryable<T> Query<T>() where T : class;
    }
}
