﻿using eStore_Admin.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eStore_Admin.Infrastructure.Configurations
{
    public class CompatibleDeviceConfiguration : IEntityTypeConfiguration<CompatibleDevice>
    {
        public void Configure(EntityTypeBuilder<CompatibleDevice> builder)
        {
            builder.HasKey(d => d.Id);
            builder.Property(d => d.Name)
                .HasMaxLength(100);
        }
    }
}