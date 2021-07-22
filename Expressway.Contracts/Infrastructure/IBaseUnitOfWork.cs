using Expressway.Contracts.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Expressway.Contracts.Infrastructure
{
    public interface IBaseUnitOfWork : IDisposable
    {
        IVehicleBrandRepository VehicleBrands { get; }
        IVehicleModelRepository VehicleModels { get; }
        IVehicleTypeRepository VehicleTypes { get; }
        IVehicleRepository Vehicles { get; }
        IUserRepository Users { get; }
        IRideRepository Rides { get; }
        ISeatRepository Seats { get; }
        IUserLoginRepository UserLogins { get; }
        IPassengerRideRequestRepository PassengerRideRequests { get; }
        IExchangePointRepository ExchangePoints { get; }

        IDriverVehicleRepository DriverVehicles { get; }
        IDriverRatingByPassengerRepository DriverRatingsByPassenger { get; }
        IPassengerRatingByDriverRepository PassengerRatingsByDriver { get; }
        IMobileNoVerificationRepository MobileNoVerifications { get; }




        Task<int> CompleteAsync();

        Task BeginTransactionAsync();

        void CommitTransaction();

        void RollbackTransaction();
    }
}
