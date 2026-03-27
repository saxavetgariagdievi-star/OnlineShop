using AutoMapper;
using OnlineShoppingApi.DTOs;
using OnlineShoppingApi.Modules;

namespace OnlineShoppingApi.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Brand
            CreateMap<Brand, BrandDto>();
            CreateMap<CreateBrandDto, Brand>();
            CreateMap<UpdateBrandDto, Brand>();

            // Category
            CreateMap<Category, CategoryDto>();
            CreateMap<CreateCategoryDto, Category>();
            CreateMap<UpdateCategoryDto, Category>();

            // Product
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.CategoryName,
                    opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : "Təyin edilməyib"))
                .ForMember(dest => dest.BrandName,
                    opt => opt.MapFrom(src => src.Brand != null ? src.Brand.Name : "Təyin edilməyib"));

            CreateMap<CreateProductDto, Product>();
            CreateMap<UpdateProductDto, Product>();

            // User
            CreateMap<User, UserDto>();
            CreateMap<RegisterUserDto, User>();

            // CartItem
            CreateMap<CartItem, CartItemDto>()
                .ForMember(dest => dest.ProductName,
                    opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.Price,
                    opt => opt.MapFrom(src => src.Product.Price));

            // Cart
            CreateMap<Cart, CartDto>()
                .ForMember(dest => dest.Items,
                    opt => opt.MapFrom(src => src.CartItems)) // <--- burada əlavə olundu
                .ForMember(dest => dest.TotalPrice,
                    opt => opt.MapFrom(src => src.CartItems != null
                        ? src.CartItems.Sum(x => x.Product != null ? x.Product.Price * x.Quantity : 0)
                        : 0));

            // OrderItem
            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(dest => dest.ProductName,
                    opt => opt.MapFrom(src => src.Product.Name));

            // Order
            CreateMap<Order, OrderDto>()
                .ForMember(dest => dest.Items,
                    opt => opt.MapFrom(src => src.OrderItems))
                .ForMember(dest => dest.Status,
                    opt => opt.MapFrom(src => src.Status.ToString()));
        }
    }
}