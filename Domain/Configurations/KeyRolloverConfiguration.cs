using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Configurations
{
    public class KeyRolloverConfiguration : IEntityTypeConfiguration<KeyRollover>
    {
        public void Configure(EntityTypeBuilder<KeyRollover> builder)
        {
            builder.HasKey(k => k.Id);
            builder.Property(k => k.Name)
                .HasMaxLength(100);
        }
    }
}