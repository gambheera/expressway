﻿// <auto-generated />
using System;
using Expressway.Database.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Expressway.Database.Migrations
{
    [DbContext(typeof(ExpresswayContext))]
    [Migration("20200224101129_tryCount")]
    partial class tryCount
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Expressway.Model.Domain.DriverRatingByPassenger", b =>
                {
                    b.Property<long>("DriverId");

                    b.Property<long>("PassengerId");

                    b.Property<long>("RideId");

                    b.Property<string>("Comment");

                    b.Property<long>("Id");

                    b.Property<int>("Rating");

                    b.Property<DateTime>("Time");

                    b.HasKey("DriverId", "PassengerId", "RideId");

                    b.HasIndex("Id");

                    b.HasIndex("PassengerId");

                    b.HasIndex("RideId");

                    b.ToTable("DriverRatingsByPassenger");
                });

            modelBuilder.Entity("Expressway.Model.Domain.DriverVehicle", b =>
                {
                    b.Property<long>("DriverId");

                    b.Property<long>("VehicleId");

                    b.Property<long>("Id");

                    b.HasKey("DriverId", "VehicleId");

                    b.HasIndex("VehicleId");

                    b.ToTable("DriverVehicles");
                });

            modelBuilder.Entity("Expressway.Model.Domain.ExchangePoint", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<long>("Latitude");

                    b.Property<long>("Longitude");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("ExchangePoints");
                });

            modelBuilder.Entity("Expressway.Model.Domain.MobileNoVerification", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DeviceUniqueId");

                    b.Property<bool>("HasMobileNoVerifiedSuccessfully");

                    b.Property<bool>("HasVerificationCodeRequested");

                    b.Property<bool>("HasVerificationCodeSentSuccessfully");

                    b.Property<bool>("IsSuspended");

                    b.Property<string>("MobileNo");

                    b.Property<DateTime>("MobileNoSuccessfullyVerifiedTime");

                    b.Property<string>("SentVerificationCode");

                    b.Property<int>("TryCount");

                    b.Property<DateTime>("VerificationCodeRequestedTime");

                    b.Property<DateTime>("VerificationCodeSuccessfullySentTime");

                    b.Property<string>("VerificationUniqueIdGivenByMobile");

                    b.HasKey("Id");

                    b.ToTable("MobileNoVerifications");
                });

            modelBuilder.Entity("Expressway.Model.Domain.PassengerRatingByDriver", b =>
                {
                    b.Property<long>("PassengerId");

                    b.Property<long>("DriverId");

                    b.Property<long?>("RideId");

                    b.Property<string>("Comment");

                    b.Property<long>("Id");

                    b.Property<int>("Rating");

                    b.Property<DateTime>("Time");

                    b.HasKey("PassengerId", "DriverId", "RideId");

                    b.HasIndex("DriverId");

                    b.HasIndex("Id");

                    b.HasIndex("RideId");

                    b.ToTable("PassengerRatingsByDriver");
                });

            modelBuilder.Entity("Expressway.Model.Domain.PassengerRideRequest", b =>
                {
                    b.Property<long>("SeatId");

                    b.Property<long>("RideId");

                    b.Property<long>("PassengerId");

                    b.Property<long>("Id");

                    b.Property<int>("RequestStatus");

                    b.Property<DateTime>("RequestedTime");

                    b.HasKey("SeatId", "RideId", "PassengerId");

                    b.HasIndex("PassengerId");

                    b.ToTable("PassengerRideRequests");
                });

            modelBuilder.Entity("Expressway.Model.Domain.Ride", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AproximateTimeDuration");

                    b.Property<long>("DriverId");

                    b.Property<long>("EntryPointId");

                    b.Property<long>("ExitPointId");

                    b.Property<int>("RideState");

                    b.Property<DateTime>("StartingTime");

                    b.Property<long>("VehicleId");

                    b.HasKey("Id");

                    b.HasIndex("EntryPointId");

                    b.HasIndex("ExitPointId");

                    b.HasIndex("VehicleId", "DriverId");

                    b.ToTable("Rides");
                });

            modelBuilder.Entity("Expressway.Model.Domain.Seat", b =>
                {
                    b.Property<long>("Id");

                    b.Property<long>("RideId");

                    b.Property<bool>("IsLeftCorner");

                    b.Property<bool>("IsRightCorner");

                    b.Property<int>("RowIndex");

                    b.HasKey("Id", "RideId");

                    b.HasIndex("RideId");

                    b.ToTable("Seats");
                });

            modelBuilder.Entity("Expressway.Model.Domain.SeatPlanTemplate", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("PlanJson");

                    b.HasKey("Id");

                    b.ToTable("SeatPlanTemplate");
                });

            modelBuilder.Entity("Expressway.Model.Domain.User", b =>
                {
                    b.Property<long>("Id");

                    b.Property<string>("ExtraMobileNo");

                    b.Property<string>("MobileNo")
                        .IsRequired();

                    b.Property<string>("Name");

                    b.Property<string>("Nic");

                    b.Property<string>("Nickname");

                    b.Property<int>("UserMode");

                    b.HasKey("Id");

                    b.HasAlternateKey("MobileNo");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Expressway.Model.Domain.UserLogin", b =>
                {
                    b.Property<long>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AuthKey");

                    b.Property<long>("Id");

                    b.Property<bool>("IsAccountSuspended");

                    b.Property<bool>("IsActive");

                    b.HasKey("UserId");

                    b.HasAlternateKey("Id");

                    b.ToTable("UserLogins");
                });

            modelBuilder.Entity("Expressway.Model.Domain.Vehicle", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("RegisterNumber")
                        .IsRequired();

                    b.Property<long>("VehicleModelId");

                    b.HasKey("Id");

                    b.HasAlternateKey("RegisterNumber");

                    b.HasIndex("VehicleModelId");

                    b.ToTable("Vehicles");
                });

            modelBuilder.Entity("Expressway.Model.Domain.VehicleBrand", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("VehicleBrands");
                });

            modelBuilder.Entity("Expressway.Model.Domain.VehicleModel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<long>("VehicleBrandId");

                    b.Property<long>("VehicleTypeId");

                    b.Property<int>("Year");

                    b.HasKey("Id");

                    b.HasIndex("VehicleBrandId");

                    b.HasIndex("VehicleTypeId");

                    b.ToTable("VehicleModels");
                });

            modelBuilder.Entity("Expressway.Model.Domain.VehicleType", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<long?>("SeatPlanTemplateId");

                    b.HasKey("Id");

                    b.HasIndex("SeatPlanTemplateId");

                    b.ToTable("VehicleTypes");
                });

            modelBuilder.Entity("Expressway.Model.Domain.DriverRatingByPassenger", b =>
                {
                    b.HasOne("Expressway.Model.Domain.User", "Driver")
                        .WithMany()
                        .HasForeignKey("DriverId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Expressway.Model.Domain.User")
                        .WithMany("DriverRatingsByPassenger")
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Expressway.Model.Domain.User", "Passenger")
                        .WithMany()
                        .HasForeignKey("PassengerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Expressway.Model.Domain.Ride", "Ride")
                        .WithMany("DriverRatingsByPassenger")
                        .HasForeignKey("RideId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Expressway.Model.Domain.DriverVehicle", b =>
                {
                    b.HasOne("Expressway.Model.Domain.User", "Driver")
                        .WithMany("DriverVehicles")
                        .HasForeignKey("DriverId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Expressway.Model.Domain.Vehicle", "Vehicle")
                        .WithMany("DriverVehicles")
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Expressway.Model.Domain.PassengerRatingByDriver", b =>
                {
                    b.HasOne("Expressway.Model.Domain.User", "Passenger")
                        .WithMany()
                        .HasForeignKey("DriverId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Expressway.Model.Domain.User")
                        .WithMany("PassengerRatingsByDriver")
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Expressway.Model.Domain.User", "Driver")
                        .WithMany()
                        .HasForeignKey("PassengerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Expressway.Model.Domain.Ride", "Ride")
                        .WithMany("PassengerRatingsByDriver")
                        .HasForeignKey("RideId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Expressway.Model.Domain.PassengerRideRequest", b =>
                {
                    b.HasOne("Expressway.Model.Domain.User", "Passenger")
                        .WithMany("PassengerRideRequests")
                        .HasForeignKey("PassengerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Expressway.Model.Domain.Seat", "Seat")
                        .WithMany("PassengerRideRequests")
                        .HasForeignKey("SeatId", "RideId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Expressway.Model.Domain.Ride", b =>
                {
                    b.HasOne("Expressway.Model.Domain.ExchangePoint", "EntryPoint")
                        .WithMany("EntryRides")
                        .HasForeignKey("EntryPointId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Expressway.Model.Domain.ExchangePoint", "ExitPoint")
                        .WithMany("ExitRides")
                        .HasForeignKey("ExitPointId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Expressway.Model.Domain.DriverVehicle", "DriverVehicle")
                        .WithMany("Rides")
                        .HasForeignKey("VehicleId", "DriverId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Expressway.Model.Domain.Seat", b =>
                {
                    b.HasOne("Expressway.Model.Domain.Ride", "Ride")
                        .WithMany("Seats")
                        .HasForeignKey("RideId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Expressway.Model.Domain.User", b =>
                {
                    b.HasOne("Expressway.Model.Domain.UserLogin", "UserLogin")
                        .WithOne("User")
                        .HasForeignKey("Expressway.Model.Domain.User", "Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Expressway.Model.Domain.Vehicle", b =>
                {
                    b.HasOne("Expressway.Model.Domain.VehicleModel", "VehicleModel")
                        .WithMany("Vehicles")
                        .HasForeignKey("VehicleModelId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Expressway.Model.Domain.VehicleModel", b =>
                {
                    b.HasOne("Expressway.Model.Domain.VehicleBrand", "VehicleBrand")
                        .WithMany("VehicleModels")
                        .HasForeignKey("VehicleBrandId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Expressway.Model.Domain.VehicleType", "VehicleType")
                        .WithMany("VehicleModels")
                        .HasForeignKey("VehicleTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Expressway.Model.Domain.VehicleType", b =>
                {
                    b.HasOne("Expressway.Model.Domain.SeatPlanTemplate", "SeatPlanTemplate")
                        .WithMany("VehicleTypes")
                        .HasForeignKey("SeatPlanTemplateId");
                });
#pragma warning restore 612, 618
        }
    }
}
