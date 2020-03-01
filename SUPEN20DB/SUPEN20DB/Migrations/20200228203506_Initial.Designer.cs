﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SUPEN20DB.DbContexts;

namespace SUPEN20DB.Migrations
{
    [DbContext(typeof(SUPEN20DbContext))]
<<<<<<< HEAD:SUPEN20DB/SUPEN20DB/Migrations/20200228203506_Initial.Designer.cs
    [Migration("20200228203506_Initial")]
=======
    [Migration("20200225095007_Initial")]
>>>>>>> origin/dev_alicia:SUPEN20DB/SUPEN20DB/Migrations/20200225095007_Initial.Designer.cs
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SUPEN20DB.Entites.Credit", b =>
                {
                    b.Property<Guid>("CreditId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("LastModified")
                        .HasColumnType("datetime2");

                    b.HasKey("CreditId");

                    b.ToTable("Credits");
                });

            modelBuilder.Entity("SUPEN20DB.Entites.Order", b =>
                {
                    b.Property<Guid>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<int>("OrderNumber")
                        .HasColumnType("int");

                    b.Property<int>("OrderStatus")
                        .HasColumnType("int");

                    b.Property<decimal>("Total")
                        .HasColumnType("decimal(18, 2)");

                    b.HasKey("OrderId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("SUPEN20DB.Entites.OrderItem", b =>
                {
                    b.Property<Guid>("OrderItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("OrderItemId");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("SUPEN20DB.Entites.Product", b =>
                {
                    b.Property<Guid>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImgId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProductId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("SUPEN20DB.Entites.OrderItem", b =>
                {
                    b.HasOne("SUPEN20DB.Entites.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId");

                    b.HasOne("SUPEN20DB.Entites.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId");
                });
#pragma warning restore 612, 618
        }
    }
}
