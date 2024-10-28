﻿// <auto-generated />
using GarageMVC.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GarageMVC.Migrations
{
    [DbContext(typeof(GarageMVCContext))]
    partial class GarageMVCContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GarageMVC.Models.Vehicles.Vehicle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FuelType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumberOfWheels")
                        .HasColumnType("int");

                    b.Property<string>("RegistrationNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VehicleType")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("nvarchar(13)");

                    b.HasKey("Id");

                    b.ToTable("Vehicles");

                    b.HasDiscriminator<string>("VehicleType").HasValue("Vehicle");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("GarageMVC.Models.Vehicles.Airplane", b =>
                {
                    b.HasBaseType("GarageMVC.Models.Vehicles.Vehicle");

                    b.Property<int>("NumberOfEngines")
                        .HasColumnType("int");

                    b.HasDiscriminator().HasValue("Airplane");
                });

            modelBuilder.Entity("GarageMVC.Models.Vehicles.Boat", b =>
                {
                    b.HasBaseType("GarageMVC.Models.Vehicles.Vehicle");

                    b.Property<int>("LengthInFeet")
                        .HasColumnType("int");

                    b.HasDiscriminator().HasValue("Boat");
                });

            modelBuilder.Entity("GarageMVC.Models.Vehicles.Bus", b =>
                {
                    b.HasBaseType("GarageMVC.Models.Vehicles.Vehicle");

                    b.Property<int>("NumberOfSeats")
                        .HasColumnType("int");

                    b.HasDiscriminator().HasValue("Bus");
                });

            modelBuilder.Entity("GarageMVC.Models.Vehicles.Car", b =>
                {
                    b.HasBaseType("GarageMVC.Models.Vehicles.Vehicle");

                    b.Property<int>("NumberOfDoors")
                        .HasColumnType("int");

                    b.HasDiscriminator().HasValue("Car");
                });

            modelBuilder.Entity("GarageMVC.Models.Vehicles.Motorcycle", b =>
                {
                    b.HasBaseType("GarageMVC.Models.Vehicles.Vehicle");

                    b.Property<bool>("HasSidecar")
                        .HasColumnType("bit");

                    b.HasDiscriminator().HasValue("Motorcycle");
                });
#pragma warning restore 612, 618
        }
    }
}
