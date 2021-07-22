using Expressway.Model.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Expressway.Database.Context
{
    public class ExpresswayContext : DbContext
    {
        public ExpresswayContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<ExchangePoint> ExchangePoints { get; set; }
        public DbSet<VehicleBrand> VehicleBrands { get; set; }
        public DbSet<VehicleType> VehicleTypes { get; set; }
        public DbSet<VehicleModel> VehicleModels { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserLogin> UserLogins { get; set; }
        public DbSet<DriverVehicle> DriverVehicles { get; set; }
        public DbSet<Ride> Rides { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<PassengerRideRequest> PassengerRideRequests { get; set; }
        public DbSet<DriverRatingByPassenger> DriverRatingsByPassenger { get; set; }
        public DbSet<PassengerRatingByDriver> PassengerRatingsByDriver { get; set; }
        public DbSet<MobileNoVerification> MobileNoVerifications { get; set; }

        // TODO: Add all entities to here

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            #region PRIMARY KEYS

            modelBuilder.Entity<ExchangePoint>().HasKey(p => new { p.Id });
            modelBuilder.Entity<VehicleBrand>().HasKey(p => new { p.Id });
            modelBuilder.Entity<VehicleType>().HasKey(p => new { p.Id });
            modelBuilder.Entity<VehicleModel>().HasKey(p => new { p.Id });
            modelBuilder.Entity<Vehicle>().HasKey(p => new { p.Id });
            modelBuilder.Entity<User>().HasKey(p => new { p.Id });
            modelBuilder.Entity<User>().HasAlternateKey(p => new { p.MobileNo });
            modelBuilder.Entity<UserLogin>().HasKey(p => new { p.UserId });
            // modelBuilder.Entity<UserLogin>().HasAlternateKey(p => new { p.Id });
            modelBuilder.Entity<DriverVehicle>().HasKey(p => new { p.DriverId, p.VehicleId });
            modelBuilder.Entity<Ride>().HasKey(p => new { p.Id });
            modelBuilder.Entity<Seat>().HasKey(p => new { p.Id, p.RideId });
            modelBuilder.Entity<PassengerRideRequest>().HasKey(p => new { p.RideId, p.PassengerId });
            modelBuilder.Entity<DriverRatingByPassenger>().HasKey(p => new { p.DriverId, p.PassengerId, p.RideId });
            modelBuilder.Entity<PassengerRatingByDriver>().HasKey(p => new { p.PassengerId, p.DriverId, p.RideId });
            modelBuilder.Entity<Vehicle>().HasAlternateKey(p => new { p.RegisterNumber });
            modelBuilder.Entity<MobileNoVerification>().HasKey(p => new { p.Id });

            #endregion

            #region FOREIGN KEYS

            modelBuilder.Entity<UserLogin>().HasOne(dv => dv.User).WithOne(u => u.UserLogin).HasForeignKey<User>(v => v.Id);

            modelBuilder.Entity<VehicleModel>().HasOne(v => v.VehicleBrand).WithMany(b => b.VehicleModels).HasForeignKey(v => v.VehicleBrandId);
            modelBuilder.Entity<VehicleModel>().HasOne(v => v.VehicleType).WithMany(b => b.VehicleModels).HasForeignKey(v => v.VehicleTypeId);

            modelBuilder.Entity<Vehicle>().HasOne(v => v.VehicleModel).WithMany(m => m.Vehicles).HasForeignKey(v => v.VehicleModelId);

            modelBuilder.Entity<DriverVehicle>().HasOne(dv => dv.Vehicle).WithMany(b => b.DriverVehicles).HasForeignKey(v => v.VehicleId);
            modelBuilder.Entity<DriverVehicle>().HasOne(dv => dv.Driver).WithMany(b => b.DriverVehicles).HasForeignKey(v => v.DriverId);

            modelBuilder.Entity<Ride>().HasOne(dv => dv.DriverVehicle).WithMany(b => b.Rides).HasForeignKey(v => new { v.DriverId, v.VehicleId });
            modelBuilder.Entity<Ride>().HasOne(dv => dv.EntryPoint).WithMany(b => b.EntryRides).HasForeignKey(v => v.EntryPointId);
            modelBuilder.Entity<Ride>().HasOne(dv => dv.ExitPoint).WithMany(b => b.ExitRides).HasForeignKey(v => v.ExitPointId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Seat>().HasOne(dv => dv.Ride).WithMany(b => b.Seats).HasForeignKey(v => v.RideId);

            modelBuilder.Entity<PassengerRideRequest>().HasOne(dv => dv.Passenger).WithMany(b => b.PassengerRideRequests).HasForeignKey(v => v.PassengerId).OnDelete(DeleteBehavior.Restrict); ;
            // modelBuilder.Entity<PassengerRideRequest>().HasOne(dv => dv.Seat).WithMany(b => b.PassengerRideRequests).HasForeignKey(v => new { v.SeatId, v.RideId }).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PassengerRatingByDriver>().HasOne(dv => dv.Driver).WithMany().HasForeignKey(v => v.PassengerId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<PassengerRatingByDriver>().HasOne(dv => dv.Ride).WithMany(b => b.PassengerRatingsByDriver).HasForeignKey(v => v.RideId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<PassengerRatingByDriver>().HasOne(dv => dv.Passenger).WithMany().HasForeignKey(v => v.DriverId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DriverRatingByPassenger>().HasOne(dv => dv.Driver).WithMany().HasForeignKey(v => v.DriverId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<DriverRatingByPassenger>().HasOne(dv => dv.Ride).WithMany(b => b.DriverRatingsByPassenger).HasForeignKey(v => v.RideId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<DriverRatingByPassenger>().HasOne(dv => dv.Passenger).WithMany().HasForeignKey(v => v.PassengerId).OnDelete(DeleteBehavior.Restrict);

            #endregion
        }

        public virtual void Save()
        {
            base.SaveChanges();
        }

        public override int SaveChanges()
        {
            //TrackChanges();
            return SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            //TrackChanges();
            return await base.SaveChangesAsync(cancellationToken);
        }

        public async Task BeginTransactionAsync()
        {
            await Database.BeginTransactionAsync();
            //await Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable);
        }

        public void CommitTransaction()
        {
            //await context.SaveChangesAsync();
            Database.CommitTransaction();
        }

        public void RollbackTransaction()
        {
            Database.RollbackTransaction();
        }
    }
}
