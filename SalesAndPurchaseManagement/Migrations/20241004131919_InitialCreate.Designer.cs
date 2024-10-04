﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SalesAndPurchaseManagement.Data;

#nullable disable

namespace SalesAndPurchaseManagement.Migrations
{
    [DbContext(typeof(SAPManagementContext))]
    [Migration("20241004131919_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.33")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("SalesAndPurchaseManagement.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryId"), 1L, 1);

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("SalesAndPurchaseManagement.Models.Color", b =>
                {
                    b.Property<int>("ColorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ColorId"), 1L, 1);

                    b.Property<string>("ColorName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ColorId");

                    b.ToTable("Colors");
                });

            modelBuilder.Entity("SalesAndPurchaseManagement.Models.CountryOfOrigin", b =>
                {
                    b.Property<int>("CountryOfOriginId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CountryOfOriginId"), 1L, 1);

                    b.Property<string>("CountryName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("CountryOfOriginId");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("SalesAndPurchaseManagement.Models.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CustomerId"), 1L, 1);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.HasKey("CustomerId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("SalesAndPurchaseManagement.Models.Employee", b =>
                {
                    b.Property<int>("EmployeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmployeeId"), 1L, 1);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("EmployeeName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("bit");

                    b.Property<int>("JobId")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EmployeeId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("JobId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("SalesAndPurchaseManagement.Models.Feature", b =>
                {
                    b.Property<int>("FeatureId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FeatureId"), 1L, 1);

                    b.Property<string>("FeatureName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("FeatureId");

                    b.ToTable("Features");
                });

            modelBuilder.Entity("SalesAndPurchaseManagement.Models.Job", b =>
                {
                    b.Property<int>("JobId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("JobId"), 1L, 1);

                    b.Property<string>("JobTitle")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal>("Salary")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("JobId");

                    b.ToTable("Jobs");
                });

            modelBuilder.Entity("SalesAndPurchaseManagement.Models.Manufacturer", b =>
                {
                    b.Property<int>("ManufacturerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ManufacturerId"), 1L, 1);

                    b.Property<string>("ManufacturerName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("ManufacturerId");

                    b.ToTable("Manufacturers");
                });

            modelBuilder.Entity("SalesAndPurchaseManagement.Models.Material", b =>
                {
                    b.Property<int>("MaterialId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaterialId"), 1L, 1);

                    b.Property<string>("MaterialName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("MaterialId");

                    b.ToTable("Materials");
                });

            modelBuilder.Entity("SalesAndPurchaseManagement.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductId"), 1L, 1);

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int>("CharacteristicId")
                        .HasColumnType("int");

                    b.Property<int>("ColorId")
                        .HasColumnType("int");

                    b.Property<int>("CountryOfOriginId")
                        .HasColumnType("int");

                    b.Property<int>("FeatureId")
                        .HasColumnType("int");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ManufacturerId")
                        .HasColumnType("int");

                    b.Property<int>("MaterialId")
                        .HasColumnType("int");

                    b.Property<string>("Notes")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal>("PurchasePrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<decimal>("SellingPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ShapeId")
                        .HasColumnType("int");

                    b.Property<int>("SizeId")
                        .HasColumnType("int");

                    b.Property<int>("WarrantyPeriod")
                        .HasColumnType("int");

                    b.HasKey("ProductId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("CharacteristicId");

                    b.HasIndex("ColorId");

                    b.HasIndex("CountryOfOriginId");

                    b.HasIndex("FeatureId");

                    b.HasIndex("ManufacturerId");

                    b.HasIndex("MaterialId");

                    b.HasIndex("ShapeId");

                    b.HasIndex("SizeId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("SalesAndPurchaseManagement.Models.PurchaseInvoice", b =>
                {
                    b.Property<int>("PurchaseInvoiceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PurchaseInvoiceId"), 1L, 1);

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("InvoiceDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("SupplierId")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("PurchaseInvoiceId");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("SupplierId");

                    b.ToTable("PurchaseInvoices");
                });

            modelBuilder.Entity("SalesAndPurchaseManagement.Models.PurchaseInvoiceDetail", b =>
                {
                    b.Property<int>("PurchaseInvoiceDetailId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PurchaseInvoiceDetailId"), 1L, 1);

                    b.Property<decimal>("Discount")
                        .HasColumnType("decimal(5,2)");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("PurchaseInvoiceId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("PurchaseInvoiceDetailId");

                    b.HasIndex("ProductId");

                    b.HasIndex("PurchaseInvoiceId");

                    b.ToTable("PurchaseInvoiceDetails");
                });

            modelBuilder.Entity("SalesAndPurchaseManagement.Models.SalesAndPurchaseManagement.Models.Characteristic", b =>
                {
                    b.Property<int>("CharacteristicId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CharacteristicId"), 1L, 1);

                    b.Property<string>("CharacteristicName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("CharacteristicId");

                    b.ToTable("Characteristic");
                });

            modelBuilder.Entity("SalesAndPurchaseManagement.Models.SalesInvoice", b =>
                {
                    b.Property<int>("SalesInvoiceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SalesInvoiceId"), 1L, 1);

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("InvoiceDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("SalesInvoiceId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("SalesInvoices");
                });

            modelBuilder.Entity("SalesAndPurchaseManagement.Models.SalesInvoiceDetail", b =>
                {
                    b.Property<int>("SalesInvoiceDetailId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SalesInvoiceDetailId"), 1L, 1);

                    b.Property<decimal>("Discount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("SalesInvoiceId")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("SalesInvoiceDetailId");

                    b.HasIndex("ProductId");

                    b.HasIndex("SalesInvoiceId");

                    b.ToTable("SalesInvoiceDetails");
                });

            modelBuilder.Entity("SalesAndPurchaseManagement.Models.Shape", b =>
                {
                    b.Property<int>("ShapeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ShapeId"), 1L, 1);

                    b.Property<string>("ShapeName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("ShapeId");

                    b.ToTable("Shapes");
                });

            modelBuilder.Entity("SalesAndPurchaseManagement.Models.Size", b =>
                {
                    b.Property<int>("SizeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SizeId"), 1L, 1);

                    b.Property<string>("SizeName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("SizeId");

                    b.ToTable("Sizes");
                });

            modelBuilder.Entity("SalesAndPurchaseManagement.Models.Supplier", b =>
                {
                    b.Property<int>("SupplierId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SupplierId"), 1L, 1);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("SupplierName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("SupplierId");

                    b.ToTable("Suppliers");
                });

            modelBuilder.Entity("SalesAndPurchaseManagement.Models.Employee", b =>
                {
                    b.HasOne("SalesAndPurchaseManagement.Models.Job", "Job")
                        .WithMany("Employees")
                        .HasForeignKey("JobId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Job");
                });

            modelBuilder.Entity("SalesAndPurchaseManagement.Models.Product", b =>
                {
                    b.HasOne("SalesAndPurchaseManagement.Models.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SalesAndPurchaseManagement.Models.SalesAndPurchaseManagement.Models.Characteristic", "Characteristic")
                        .WithMany()
                        .HasForeignKey("CharacteristicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SalesAndPurchaseManagement.Models.Color", "Color")
                        .WithMany("Products")
                        .HasForeignKey("ColorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SalesAndPurchaseManagement.Models.CountryOfOrigin", "Country")
                        .WithMany("Products")
                        .HasForeignKey("CountryOfOriginId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SalesAndPurchaseManagement.Models.Feature", "Feature")
                        .WithMany("Products")
                        .HasForeignKey("FeatureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SalesAndPurchaseManagement.Models.Manufacturer", "Manufacturer")
                        .WithMany("Products")
                        .HasForeignKey("ManufacturerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SalesAndPurchaseManagement.Models.Material", "Material")
                        .WithMany("Products")
                        .HasForeignKey("MaterialId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SalesAndPurchaseManagement.Models.Shape", "Shape")
                        .WithMany("Products")
                        .HasForeignKey("ShapeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SalesAndPurchaseManagement.Models.Size", "Size")
                        .WithMany("Products")
                        .HasForeignKey("SizeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Characteristic");

                    b.Navigation("Color");

                    b.Navigation("Country");

                    b.Navigation("Feature");

                    b.Navigation("Manufacturer");

                    b.Navigation("Material");

                    b.Navigation("Shape");

                    b.Navigation("Size");
                });

            modelBuilder.Entity("SalesAndPurchaseManagement.Models.PurchaseInvoice", b =>
                {
                    b.HasOne("SalesAndPurchaseManagement.Models.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SalesAndPurchaseManagement.Models.Supplier", "Supplier")
                        .WithMany("PurchaseInvoices")
                        .HasForeignKey("SupplierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("Supplier");
                });

            modelBuilder.Entity("SalesAndPurchaseManagement.Models.PurchaseInvoiceDetail", b =>
                {
                    b.HasOne("SalesAndPurchaseManagement.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SalesAndPurchaseManagement.Models.PurchaseInvoice", "PurchaseInvoice")
                        .WithMany()
                        .HasForeignKey("PurchaseInvoiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("PurchaseInvoice");
                });

            modelBuilder.Entity("SalesAndPurchaseManagement.Models.SalesInvoice", b =>
                {
                    b.HasOne("SalesAndPurchaseManagement.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SalesAndPurchaseManagement.Models.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("SalesAndPurchaseManagement.Models.SalesInvoiceDetail", b =>
                {
                    b.HasOne("SalesAndPurchaseManagement.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SalesAndPurchaseManagement.Models.SalesInvoice", "SalesInvoice")
                        .WithMany()
                        .HasForeignKey("SalesInvoiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("SalesInvoice");
                });

            modelBuilder.Entity("SalesAndPurchaseManagement.Models.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("SalesAndPurchaseManagement.Models.Color", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("SalesAndPurchaseManagement.Models.CountryOfOrigin", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("SalesAndPurchaseManagement.Models.Feature", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("SalesAndPurchaseManagement.Models.Job", b =>
                {
                    b.Navigation("Employees");
                });

            modelBuilder.Entity("SalesAndPurchaseManagement.Models.Manufacturer", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("SalesAndPurchaseManagement.Models.Material", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("SalesAndPurchaseManagement.Models.Shape", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("SalesAndPurchaseManagement.Models.Size", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("SalesAndPurchaseManagement.Models.Supplier", b =>
                {
                    b.Navigation("PurchaseInvoices");
                });
#pragma warning restore 612, 618
        }
    }
}
