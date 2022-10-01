using eStore_Admin.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eStore_Admin.Infrastructure.Persistence.Configurations
{
    public class ShoppingCartConfiguration : IEntityTypeConfiguration<ShoppingCart>
    {
        public void Configure(EntityTypeBuilder<ShoppingCart> builder)
        {
            builder.HasKey(c => c.Id);
            builder.HasOne(sc => sc.Customer)
                .WithOne(c => c.ShoppingCart)
                .HasForeignKey<ShoppingCart>(sc => sc.CustomerId);
            builder.HasMany(sc => sc.Goods)
                .WithMany(g => g.ShoppingCarts);
        }
    }
}