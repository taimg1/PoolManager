﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PoolMS.Core;

#nullable disable

namespace PoolMS.Core.Migrations
{
    [DbContext(typeof(PoolContext))]
    [Migration("20240203073213_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PoolMS.Core.Entities.Payment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<DateTime>("PaymentDay")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Payment");
                });

            modelBuilder.Entity("PoolMS.Core.Entities.Pool", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("MaxCapacity")
                        .HasColumnType("int");

                    b.Property<int>("SizeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SizeId");

                    b.ToTable("Pool");
                });

            modelBuilder.Entity("PoolMS.Core.Entities.PoolSize", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Height")
                        .HasColumnType("int");

                    b.Property<int>("Length")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Width")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("PoolSize");
                });

            modelBuilder.Entity("PoolMS.Core.Entities.Reservation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("PoolId")
                        .HasColumnType("int");

                    b.Property<int>("SubscriptionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PoolId");

                    b.HasIndex("SubscriptionId");

                    b.ToTable("Reservation");
                });

            modelBuilder.Entity("PoolMS.Core.Entities.SubType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("SubType");
                });

            modelBuilder.Entity("PoolMS.Core.Entities.Subscription", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("SubTypeId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SubTypeId");

                    b.HasIndex("UserId");

                    b.ToTable("Subscription");
                });

            modelBuilder.Entity("PoolMS.Core.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Fname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("PoolMS.Core.Entities.Visit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("ReservationId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("StayTime")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ReservationId");

                    b.ToTable("Visit");
                });

            modelBuilder.Entity("PoolMS.Core.Entities.Payment", b =>
                {
                    b.HasOne("PoolMS.Core.Entities.User", "User")
                        .WithMany("Payments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("PoolMS.Core.Entities.Pool", b =>
                {
                    b.HasOne("PoolMS.Core.Entities.PoolSize", "Size")
                        .WithMany("Pools")
                        .HasForeignKey("SizeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Size");
                });

            modelBuilder.Entity("PoolMS.Core.Entities.Reservation", b =>
                {
                    b.HasOne("PoolMS.Core.Entities.Pool", "Pool")
                        .WithMany("Reservations")
                        .HasForeignKey("PoolId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PoolMS.Core.Entities.Subscription", "Subscription")
                        .WithMany("Reservations")
                        .HasForeignKey("SubscriptionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pool");

                    b.Navigation("Subscription");
                });

            modelBuilder.Entity("PoolMS.Core.Entities.Subscription", b =>
                {
                    b.HasOne("PoolMS.Core.Entities.SubType", "SubType")
                        .WithMany("Subscriptions")
                        .HasForeignKey("SubTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PoolMS.Core.Entities.User", "User")
                        .WithMany("Subscriptions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SubType");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PoolMS.Core.Entities.Visit", b =>
                {
                    b.HasOne("PoolMS.Core.Entities.Reservation", "Reservation")
                        .WithMany("Visits")
                        .HasForeignKey("ReservationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Reservation");
                });

            modelBuilder.Entity("PoolMS.Core.Entities.Pool", b =>
                {
                    b.Navigation("Reservations");
                });

            modelBuilder.Entity("PoolMS.Core.Entities.PoolSize", b =>
                {
                    b.Navigation("Pools");
                });

            modelBuilder.Entity("PoolMS.Core.Entities.Reservation", b =>
                {
                    b.Navigation("Visits");
                });

            modelBuilder.Entity("PoolMS.Core.Entities.SubType", b =>
                {
                    b.Navigation("Subscriptions");
                });

            modelBuilder.Entity("PoolMS.Core.Entities.Subscription", b =>
                {
                    b.Navigation("Reservations");
                });

            modelBuilder.Entity("PoolMS.Core.Entities.User", b =>
                {
                    b.Navigation("Payments");

                    b.Navigation("Subscriptions");
                });
#pragma warning restore 612, 618
        }
    }
}