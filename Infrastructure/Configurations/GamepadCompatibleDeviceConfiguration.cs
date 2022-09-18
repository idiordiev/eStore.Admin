﻿using eStore_Admin.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eStore_Admin.Infrastructure.Configurations
{
    public class GamepadCompatibleDeviceConfiguration : IEntityTypeConfiguration<GamepadCompatibleDevice>
    {
        public void Configure(EntityTypeBuilder<GamepadCompatibleDevice> builder)
        {
            builder.HasKey(g => new { g.GamepadId, g.CompatibleDeviceId });
            builder.HasOne(g => g.Gamepad)
                .WithMany(g => g.CompatibleDevices)
                .HasForeignKey(g => g.GamepadId);
            builder.HasOne(g => g.CompatibleDevice)
                .WithMany(d => d.Gamepads)
                .HasForeignKey(g => g.CompatibleDeviceId);
        }
    }
}