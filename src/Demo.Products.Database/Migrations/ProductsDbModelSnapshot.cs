﻿// <auto-generated />
using System;
using Demo.Products.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Demo.Products.Database.Migrations
{
    [DbContext(typeof(ProductsDb))]
    partial class ProductsDbModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Demo.Products.Models.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)");

                    b.Property<decimal>("DeliveryPrice")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18, 2)");

                    b.HasKey("Id")
                        .HasName("PK__Product");

                    b.ToTable("Product", (string)null);
                });

            modelBuilder.Entity("Demo.Products.Models.ProductOption", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id")
                        .HasName("PK__ProductOption");

                    b.ToTable("ProductOption", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
