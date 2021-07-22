using Expressway.Communication.Core;
using Expressway.Contracts.Communication;
using Expressway.Contracts.Infrastructure;
using Expressway.Contracts.Repository;
using Expressway.Contracts.Service;
using Expressway.Infrastructure.GenericRepository;
using Expressway.Infrastructure.UnitOfWork;
using Expressway.Repository.Core;
using Expressway.Service.Core;
using Expressway.Service.Helper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expressway.Api
{
    public class DependancyConfigs
    {
        public static void Inject(IServiceCollection services)
        {
            services.TryAdd(ServiceDescriptor.Scoped(typeof(IGenericRepository<>), typeof(GenericRepository<>)));

            services.AddScoped<IBaseUnitOfWork, BaseUnitOfWork>();

            services.AddScoped<IVehicleService, VehicleService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IEncriptedIdService, EncriptedIdService>();
            services.AddScoped<IMobileVerificationService, MobileVerificationService>();
            services.AddScoped<IRatingService, RatingService>();

            services.AddScoped<ISmsHandler, SmsHandler>();

            services.AddScoped<IVehicleBrandRepository, VehicleBrandRepository>();
            services.AddScoped<IVehicleTypeRepository, VehicleTypeRepository>();
            services.AddScoped<IVehicleModelRepository, VehicleModelRepository>();
            services.AddScoped<IVehicleRepository, VehicleRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRideService, RideService>();
            services.AddScoped<IRideRepository, RideRepository>();
            services.AddScoped<IDriverVehicleRepository, DriverVehicleRepository>();
            services.AddScoped<IExchangePointRepository, ExchangePointRepository>();
            services.AddScoped<IPassengerRatingByDriverRepository, PassengerRatingByDriverRepository>();
            services.AddScoped<IDriverRatingByPassengerRepository, DriverRatingByPassengerRepository>();
            services.AddScoped<IMobileNoVerificationRepository, MobileNoVerificationRepository>();
            services.AddScoped<IUserLoginRepository, UserLoginRepository>();
            services.AddScoped<IPassengerRideRequestRepository, PassengerRideRequestRepository>();

        }
    }
}
