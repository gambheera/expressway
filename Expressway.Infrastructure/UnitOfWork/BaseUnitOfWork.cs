using Expressway.Contracts.Infrastructure;
using Expressway.Contracts.Repository;
using Expressway.Database.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Expressway.Infrastructure.UnitOfWork
{
    public class BaseUnitOfWork : IBaseUnitOfWork
    {
        private readonly ExpresswayContext context;

        public IVehicleBrandRepository VehicleBrands { get; private set; }
        public IVehicleModelRepository VehicleModels { get; private set; }
        public IVehicleTypeRepository VehicleTypes { get; private set; }
        public IVehicleRepository Vehicles { get; private set; }
        public IUserRepository Users { get; private set; }
        public IRideRepository Rides { get; private set; }
        public ISeatRepository Seats { get; private set; }
        public IUserLoginRepository UserLogins { get; private set; }
        public IPassengerRideRequestRepository PassengerRideRequests { get; private set; }
        public IExchangePointRepository ExchangePoints { get; private set; }

        public IDriverVehicleRepository DriverVehicles { get; private set; }
        public IDriverRatingByPassengerRepository DriverRatingsByPassenger { get; private set; }
        public IPassengerRatingByDriverRepository PassengerRatingsByDriver { get; private set; }
        public IMobileNoVerificationRepository MobileNoVerifications { get; private set; }



        public BaseUnitOfWork(
            ExpresswayContext expresswayContext,
            IVehicleBrandRepository vehicleBrands,
            IVehicleModelRepository vehicleModels,
            IVehicleTypeRepository vehicleTypes,
            IVehicleRepository vehicles,
            IUserRepository users,
            IRideRepository rides,
            IPassengerRideRequestRepository passengerRideRequests,
            IUserLoginRepository userLogins,
            IExchangePointRepository exchangePoints,
            IDriverVehicleRepository driverVehicles,
            IDriverRatingByPassengerRepository driverRatingsByPassenger,
            IPassengerRatingByDriverRepository passengerRatingsByDriver,
            IMobileNoVerificationRepository mobileNoVerifications
            )
        {
            context = expresswayContext;
            Vehicles = vehicles;
            Users = users;
            Rides = rides;
            PassengerRideRequests = passengerRideRequests;
            UserLogins = userLogins;
            ExchangePoints = exchangePoints;
            DriverVehicles = driverVehicles;
            DriverRatingsByPassenger = driverRatingsByPassenger;
            PassengerRatingsByDriver = passengerRatingsByDriver;
            MobileNoVerifications = mobileNoVerifications;
            VehicleBrands = vehicleBrands;
            VehicleModels = vehicleModels;
            VehicleTypes = vehicleTypes;
        }

        public async Task<int> CompleteAsync()
        {
            return await context.SaveChangesAsync();
        }

        public async Task BeginTransactionAsync()
        {
            await context.BeginTransactionAsync();
        }

        public void CommitTransaction()
        {
            //await context.SaveChangesAsync();
            context.CommitTransaction();
        }

        public void RollbackTransaction()
        {
            context.RollbackTransaction();
        }

        public void Dispose()
        {
            //context.Dispose();
        }
    }


}
