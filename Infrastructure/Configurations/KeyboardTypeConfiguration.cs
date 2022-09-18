using eStore_Admin.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eStore_Admin.Infrastructure.Configurations
{
    public class KeyboardTypeConfiguration : IEntityTypeConfiguration<KeyboardType>
    {
        public void Configure(EntityTypeBuilder<KeyboardType> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Name)
                .HasMaxLength(100);
        }
    }
}