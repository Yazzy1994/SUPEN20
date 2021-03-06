﻿using Microsoft.EntityFrameworkCore;
using SUPEN20DB.Entites;

namespace SUPEN20DB.DbContexts
{
    public class SUPEN20DbContext : DbContext
    {
        public SUPEN20DbContext(DbContextOptions<SUPEN20DbContext> options) : base(options)
        { }

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }
        public virtual DbSet<Credit> Credits { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) //This is a part of the join table configuration that goes throu the Fluent API for EF Core to be able to map it successfully. 
        {
            modelBuilder.Entity<OrderItem>()
                .HasKey(oi => new { oi.ProductId, oi.OrderId });
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Product)
                .WithMany(p => p.OrderItems)
                .HasForeignKey(oi => oi.ProductId);
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId);
        }
        //HasOne, WithMany and HasForeingnKey are Fluent API methods. 
    }
}