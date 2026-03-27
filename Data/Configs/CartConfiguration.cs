
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShoppingApi.Modules;

namespace OnlineShoppingApi.Data.Configurations
{
    public class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.HasOne(x => x.User)
                   .WithOne(u => u.Cart)
                   .HasForeignKey<Cart>(x => x.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}