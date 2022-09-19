﻿using System.Reflection;
using eStore_Admin.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace eStore_Admin.Infrastructure.Persistence
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext()
        {
        }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        public DbSet<Backlight> Backlights { get; set; }
        public DbSet<CompatibleDevice> CompatibleDevices { get; set; }
        public DbSet<ConnectionType> ConnectionTypes { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Gamepad> Gamepads { get; set; }
        public DbSet<GamepadCompatibleDevice> GamepadCompatibleDevices { get; set; }
        public DbSet<Goods> Goods { get; set; }
        public DbSet<GoodsInCart> GoodsInCarts { get; set; }
        public DbSet<Keyboard> Keyboards { get; set; }
        public DbSet<KeyboardSize> KeyboardSizes { get; set; }
        public DbSet<KeyboardSwitch> KeyboardSwitches { get; set; }
        public DbSet<KeyboardType> KeyboardTypes { get; set; }
        public DbSet<KeyRollover> KeyRollovers { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<Mouse> Mouses { get; set; }
        public DbSet<Mousepad> Mousepads { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}