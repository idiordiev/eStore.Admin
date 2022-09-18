using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Configurations
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