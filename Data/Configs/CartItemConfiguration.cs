
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShoppingApi.Modules;

namespace OnlineShoppingApi.Data.Configurations
{
    public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.HasOne(x => x.Cart)
                   .WithMany(c => c.CartItems)
                   .HasForeignKey(x => x.CartId);

            builder.HasOne(x => x.Product)
                   .WithMany(p => p.CartItems)
                   .HasForeignKey(x => x.ProductId);
        }
    }
}