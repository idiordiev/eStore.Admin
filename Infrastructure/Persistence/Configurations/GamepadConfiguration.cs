﻿using eStore_Admin.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eStore_Admin.Infrastructure.Persistence.Configurations
{
    public class GamepadConfiguration : IEntityTypeConfiguration<Gamepad>
    {
        public void Configure(EntityTypeBuilder<Gamepad> builder)
        {
            builder.ToTable("Gamepads");
            builder.HasOne(g => g.Feedback)
                .WithMany(f => f.Gamepads)
                .HasForeignKey(g => g.FeedbackId);
            builder.HasOne(g => g.ConnectionType)
                .WithMany()
                .HasForeignKey(g => g.ConnectionTypeId);
        }
    }
}