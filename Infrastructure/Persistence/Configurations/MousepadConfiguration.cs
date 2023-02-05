using eStore_Admin.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eStore_Admin.Infrastructure.Persistence.Configurations;

public class MousepadConfiguration : IEntityTypeConfiguration<Mousepad>
{
    public void Configure(EntityTypeBuilder<Mousepad> builder)
    {
        builder.ToTable("Mousepads");
        builder.Property(m => m.BottomMaterial)
            .HasMaxLength(100);
        builder.Property(m => m.TopMaterial)
            .HasMaxLength(100);
        builder.Property(m => m.Backlight)
            .HasMaxLength(100);
    }
}