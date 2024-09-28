﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PrinterShop.Core.Infrastructure.Data;

#nullable disable

namespace PrinterShop.Core.Infrastructure.Migrations
{
    [DbContext(typeof(ShopDbContext))]
    [Migration("20240928150751_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true);

            modelBuilder.Entity("ComponentOrder", b =>
                {
                    b.Property<Guid>("ComponentListModelNumber")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("OrdersId")
                        .HasColumnType("TEXT");

                    b.HasKey("ComponentListModelNumber", "OrdersId");

                    b.HasIndex("OrdersId");

                    b.ToTable("ComponentOrder");
                });

            modelBuilder.Entity("PrinterShop.Core.Domain.Entities.Component", b =>
                {
                    b.Property<Guid>("ModelNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Brand")
                        .HasColumnType("TEXT");

                    b.Property<int>("ComponentType")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Model")
                        .HasColumnType("TEXT");

                    b.Property<float>("Price")
                        .HasColumnType("REAL");

                    b.HasKey("ModelNumber")
                        .HasName("PrimaryKey_ModelNumber");

                    b.ToTable("Components");
                });

            modelBuilder.Entity("PrinterShop.Core.Domain.Entities.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Components")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("PrinterId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("PrinterId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("PrinterShop.Core.Domain.Entities.Printer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("BedLevelingSensor")
                        .HasColumnType("TEXT");

                    b.Property<string>("Brand")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("ComponentModelNumber")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("Extruder")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("HeatingBed")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("MainBoard")
                        .HasColumnType("TEXT");

                    b.Property<string>("Model")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("StepperMotor")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ComponentModelNumber");

                    b.ToTable("Printers");
                });

            modelBuilder.Entity("PrinterShop.Core.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .HasColumnType("TEXT");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserName")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ComponentOrder", b =>
                {
                    b.HasOne("PrinterShop.Core.Domain.Entities.Component", null)
                        .WithMany()
                        .HasForeignKey("ComponentListModelNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PrinterShop.Core.Domain.Entities.Order", null)
                        .WithMany()
                        .HasForeignKey("OrdersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PrinterShop.Core.Domain.Entities.Order", b =>
                {
                    b.HasOne("PrinterShop.Core.Domain.Entities.User", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PrinterShop.Core.Domain.Entities.Printer", "Printer")
                        .WithMany("Orders")
                        .HasForeignKey("PrinterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Printer");
                });

            modelBuilder.Entity("PrinterShop.Core.Domain.Entities.Printer", b =>
                {
                    b.HasOne("PrinterShop.Core.Domain.Entities.Component", null)
                        .WithMany("Printers")
                        .HasForeignKey("ComponentModelNumber");
                });

            modelBuilder.Entity("PrinterShop.Core.Domain.Entities.Component", b =>
                {
                    b.Navigation("Printers");
                });

            modelBuilder.Entity("PrinterShop.Core.Domain.Entities.Printer", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("PrinterShop.Core.Domain.Entities.User", b =>
                {
                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}