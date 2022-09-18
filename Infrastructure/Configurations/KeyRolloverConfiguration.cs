﻿using eStore_Admin.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eStore_Admin.Infrastructure.Configurations
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