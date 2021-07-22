using AutoMapper;
using Expressway.Model.Domain;
using Expressway.Model.Dto.Rating;
using Expressway.Model.Dto.Ride;
using Expressway.Model.Dto.Seat;
using Expressway.Model.Dto.User;
using Expressway.Model.Dto.Vehicle;
using Expressway.Utility.Encriptors;
using Expressway.Utility.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expressway.Api.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region User

            CreateMap<UserDto, User>()
                .ForMember(dest => dest.DriverRatingsByPassenger, act => act.Ignore())
                .ForMember(dest => dest.PassengerRatingsByDriver, act => act.Ignore())
                .ForMember(dest => dest.PassengerRideRequests, act => act.Ignore())
                .ForMember(dest => dest.DriverVehicles, act => act.Ignore())
                .ForMember(dest => dest.Id, act => act.Ignore())
                .ForMember(dest => dest.Id, act => act.MapFrom(src => Encriptor.DecryptToLong(src.EncriptedId, EncriptObjectType.User)));

            CreateMap<User, UserDto>()
                .ForMember(dest => dest.EncriptedId, act => act.Ignore())
                .ForMember(dest => dest.EncriptedId, act => act.MapFrom(src => Encriptor.EncryptFromLong(src.Id, EncriptObjectType.User)));

            CreateMap<UserLogin, UserLoginDto>()
                .ForMember(dest => dest.EncriptedUserId, act => act.Ignore())
                .ForMember(dest => dest.EncriptedUserId, act => act.MapFrom(src => Encriptor.EncryptFromLong(src.UserId, EncriptObjectType.User)));

            CreateMap<UserLoginDto, UserLogin>()
                .ForMember(dest => dest.User, act => act.Ignore())
                .ForMember(dest => dest.RefreshToken, act => act.Ignore())
                .ForMember(dest => dest.IsActive, act => act.Ignore())
                .ForMember(dest => dest.IsAccountSuspended, act => act.Ignore())
                .ForMember(dest => dest.UserId, act => act.Ignore());

            #endregion

            #region Ride

            CreateMap<RideDto, Ride>()
                .ForMember(dest => dest.Id, act => act.Ignore())
                .ForMember(dest => dest.Id, act => act.MapFrom(src => Encriptor.DecryptToLong(src.EncriptedId, EncriptObjectType.Ride)))
                .ForMember(dest => dest.DriverId, act => act.MapFrom(src => Encriptor.DecryptToLong(src.EncriptedDriverId, EncriptObjectType.User)))
                .ForMember(dest => dest.VehicleId, act => act.MapFrom(src => Encriptor.DecryptToLong(src.EncriptedVehicleId, EncriptObjectType.Vehicle)))
                .ForMember(dest => dest.Seats, act => act.Ignore())
                .ForMember(dest => dest.DriverRatingsByPassenger, act => act.Ignore())
                .ForMember(dest => dest.PassengerRatingsByDriver, act => act.Ignore());


            CreateMap<Ride, RideDto>()
                .ForMember(dest => dest.EncriptedId, act => act.Ignore())
                .ForMember(dest => dest.EncriptedId, act => act.MapFrom(src => Encriptor.EncryptFromLong(src.Id, EncriptObjectType.Ride)))
                .ForMember(dest => dest.EncriptedDriverId, act => act.Ignore())
                .ForMember(dest => dest.EncriptedDriverId, act => act.MapFrom(src => Encriptor.EncryptFromLong(src.Id, EncriptObjectType.User)))
                .ForMember(dest => dest.EncriptedVehicleId, act => act.Ignore())
                .ForMember(dest => dest.EncriptedVehicleId, act => act.MapFrom(src => Encriptor.EncryptFromLong(src.Id, EncriptObjectType.Vehicle)));

            CreateMap<Ride, RideWithInfoDto>()
                .ForMember(dest => dest.EncriptedId, act => act.Ignore())
                .ForMember(dest => dest.EncriptedId, act => act.MapFrom(src => Encriptor.EncryptFromLong(src.Id, EncriptObjectType.Ride)))
                .ForMember(dest => dest.EncriptedDriverId, act => act.Ignore())
                .ForMember(dest => dest.EncriptedDriverId, act => act.MapFrom(src => Encriptor.EncryptFromLong(src.Id, EncriptObjectType.User)))
                .ForMember(dest => dest.DriverNickname, act => act.MapFrom(src => src.DriverVehicle.Driver.Nickname))
                .ForMember(dest => dest.EncriptedVehicleId, act => act.Ignore())
                .ForMember(dest => dest.EncriptedVehicleId, act => act.MapFrom(src => Encriptor.EncryptFromLong(src.Id, EncriptObjectType.Vehicle)))
                .ForMember(dest => dest.VehicleRegisterNumber, act => act.MapFrom(src => src.DriverVehicle.Vehicle.RegisterNumber))
                .ForMember(dest => dest.VehicleModelWithBrand, 
                    act => act.MapFrom(src => 
                        src.DriverVehicle.Vehicle.VehicleModel.VehicleBrand.Name + " - " + src.DriverVehicle.Vehicle.VehicleModel.Name +" (" + src.DriverVehicle.Vehicle.VehicleModel.Year+")"
                        )
                    );

            CreateMap<ExchangePoint, ExchangePointSelectOptionDto>()
                .ForMember(dest => dest.Caption, act => act.Ignore())
                .ForMember(dest => dest.Caption, act => act.MapFrom(src => src.Name));

            #endregion

            #region Seat

            CreateMap<SeatDto, Seat>()
                .ForMember(dest => dest.Ride, act => act.Ignore())
                .ForMember(dest => dest.PassengerRideRequests, act => act.Ignore())
                .ForMember(dest => dest.Id, act => act.Ignore())
                .ForMember(dest => dest.Id, act => act.MapFrom(src => Encriptor.DecryptToLong(src.EncriptedId, EncriptObjectType.Seat)))
                .ForMember(dest => dest.RideId, act => act.Ignore())
                .ForMember(dest => dest.RideId, act => act.MapFrom(src => Encriptor.DecryptToLong(src.EncriptedRideId, EncriptObjectType.Ride)));

            CreateMap<Seat, SeatDto>()
                .ForMember(dest => dest.EncriptedId, act => act.Ignore())
                .ForMember(dest => dest.EncriptedId, act => act.MapFrom(src => Encriptor.EncryptFromLong(src.Id, EncriptObjectType.Seat)))
                .ForMember(dest => dest.EncriptedRideId, act => act.Ignore())
                .ForMember(dest => dest.EncriptedRideId, act => act.MapFrom(src => Encriptor.EncryptFromLong(src.Id, EncriptObjectType.Ride)));
            #endregion

            #region Vehicle Brand

            CreateMap<VehicleBrand, VehicleBrandDto>();

            CreateMap<VehicleBrand, VehicleBrandSelectOptionDto>()
                .ForMember(dest => dest.Caption, act => act.Ignore())
                .ForMember(dest => dest.Caption, act => act.MapFrom(src => src.Name));

            #endregion

            #region Vehicle Type

            CreateMap<VehicleType, VehicleTypeDto>();

            CreateMap<VehicleType, VehicleTypeSelectOptionDto>()
                .ForMember(dest => dest.Caption, act => act.Ignore())
                .ForMember(dest => dest.Caption, act => act.MapFrom(src => src.Name));

            #endregion

            #region Vehicle Model

            CreateMap<VehicleModel, VehicleModelDto>();

            CreateMap<VehicleModel, VehicleModelSelectOptionDto>()
                .ForMember(dest => dest.Caption, act => act.Ignore())
                .ForMember(dest => dest.Caption, act => act.MapFrom(src => src.Name));

            #endregion

            #region Vehicle

            CreateMap<VehicleDto, Vehicle>()
                .ForMember(dest => dest.VehicleModel, act => act.Ignore())
                .ForMember(dest => dest.DriverVehicles, act => act.Ignore())
                .ForMember(dest => dest.Id, act => act.Ignore())
                .ForMember(dest => dest.Id, act => act.MapFrom(src => Encriptor.DecryptToLong(src.EncriptedId, EncriptObjectType.Vehicle)));

            CreateMap<Vehicle, VehicleDto>()
                .ForMember(dest => dest.EncriptedId, act => act.Ignore())
                .ForMember(dest => dest.EncriptedId, act => act.MapFrom(src => Encriptor.EncryptFromLong(src.Id, EncriptObjectType.Vehicle)));

            CreateMap<Vehicle, VehicleSelectOptionDto>()
                .ForMember(dest => dest.EncriptedId, act => act.Ignore())
                .ForMember(dest => dest.EncriptedId, act => act.MapFrom(src => Encriptor.EncryptFromLong(src.Id, EncriptObjectType.Vehicle)))
                .ForMember(dest => dest.Caption, act => act.Ignore())
                .ForMember(dest => dest.Caption, act => act.MapFrom(src => src.RegisterNumber));

            #endregion

            #region Driver Vehicle

            CreateMap<DriverVehicleDto, DriverVehicle>()
                .ForMember(dest => dest.Rides, act => act.Ignore())
                .ForMember(dest => dest.DriverId, act => act.Ignore())
                .ForMember(dest => dest.DriverId, act => act.MapFrom(src => Encriptor.DecryptToLong(src.EncriptedDriverId, EncriptObjectType.User)))
                .ForMember(dest => dest.VehicleId, act => act.Ignore())
                .ForMember(dest => dest.VehicleId, act => act.MapFrom(src => Encriptor.DecryptToLong(src.EncriptedVehicleId, EncriptObjectType.Vehicle)));

            CreateMap<DriverVehicle, DriverVehicleDto>()
                .ForMember(dest => dest.EncriptedId, act => act.Ignore())
                .ForMember(dest => dest.EncriptedId, act => act.MapFrom(src => Encriptor.EncryptFromLong(src.Id, EncriptObjectType.Vehicle)))
                .ForMember(dest => dest.EncriptedDriverId, act => act.Ignore())
                .ForMember(dest => dest.EncriptedDriverId, act => act.MapFrom(src => Encriptor.EncryptFromLong(src.DriverId, EncriptObjectType.User)))
                .ForMember(dest => dest.EncriptedVehicleId, act => act.Ignore())
                .ForMember(dest => dest.EncriptedVehicleId, act => act.MapFrom(src => Encriptor.EncryptFromLong(src.VehicleId, EncriptObjectType.Vehicle)));

            #endregion

            #region Driver Rating By Passenger

            CreateMap<DriverRatingByPassengerDto, DriverRatingByPassenger>()
                .ForMember(dest => dest.Driver, act => act.Ignore())
                .ForMember(dest => dest.Passenger, act => act.Ignore())
                .ForMember(dest => dest.Ride, act => act.Ignore())
                .ForMember(dest => dest.Id, act => act.Ignore())
                .ForMember(dest => dest.Id, act => act.MapFrom(src => Encriptor.DecryptToLong(src.EncriptedId, EncriptObjectType.DriverRatingByPassenger)))
                .ForMember(dest => dest.DriverId, act => act.Ignore())
                .ForMember(dest => dest.DriverId, act => act.MapFrom(src => Encriptor.DecryptToLong(src.EncriptedDriverId, EncriptObjectType.User)))
                .ForMember(dest => dest.PassengerId, act => act.Ignore())
                .ForMember(dest => dest.PassengerId, act => act.MapFrom(src => Encriptor.DecryptToLong(src.EncriptedPassengerId, EncriptObjectType.User)))
                .ForMember(dest => dest.RideId, act => act.Ignore())
                .ForMember(dest => dest.RideId, act => act.MapFrom(src => Encriptor.DecryptToLong(src.EncriptedRideId, EncriptObjectType.Ride)));

            CreateMap<DriverRatingByPassenger, DriverRatingByPassengerDto>()
                .ForMember(dest => dest.EncriptedId, act => act.Ignore())
                .ForMember(dest => dest.EncriptedId, act => act.MapFrom(src => Encriptor.EncryptFromLong(src.Id, EncriptObjectType.DriverRatingByPassenger)))
                .ForMember(dest => dest.EncriptedDriverId, act => act.Ignore())
                .ForMember(dest => dest.EncriptedDriverId, act => act.MapFrom(src => Encriptor.EncryptFromLong(src.Id, EncriptObjectType.User)))
                .ForMember(dest => dest.EncriptedPassengerId, act => act.Ignore())
                .ForMember(dest => dest.EncriptedPassengerId, act => act.MapFrom(src => Encriptor.EncryptFromLong(src.Id, EncriptObjectType.User)))
                .ForMember(dest => dest.EncriptedRideId, act => act.Ignore())
                .ForMember(dest => dest.EncriptedRideId, act => act.MapFrom(src => Encriptor.EncryptFromLong(src.Id, EncriptObjectType.Ride)));

            #endregion

            #region Passenger Rating by Driver

            CreateMap<PassengerRatingByDriverDto, PassengerRatingByDriver>()
                 .ForMember(dest => dest.Driver, act => act.Ignore())
                 .ForMember(dest => dest.Passenger, act => act.Ignore())
                 .ForMember(dest => dest.Ride, act => act.Ignore())
                 .ForMember(dest => dest.Id, act => act.MapFrom(src => Encriptor.DecryptToLong(src.EncriptedId, EncriptObjectType.PassengerRatingByDriver)))
                 .ForMember(dest => dest.DriverId, act => act.MapFrom(src => Encriptor.DecryptToLong(src.EncriptedDriverId, EncriptObjectType.User)))
                 .ForMember(dest => dest.PassengerId, act => act.MapFrom(src => Encriptor.DecryptToLong(src.EncriptedPassengerId, EncriptObjectType.User)))
                 .ForMember(dest => dest.RideId, act => act.MapFrom(src => Encriptor.DecryptToLong(src.EncriptedRideId, EncriptObjectType.Ride)));

            CreateMap<PassengerRatingByDriver, PassengerRatingByDriverDto>()
                .ForMember(dest => dest.EncriptedId, act => act.Ignore())
                .ForMember(dest => dest.EncriptedId, act => act.MapFrom(src => Encriptor.EncryptFromLong(src.Id, EncriptObjectType.DriverRatingByPassenger)))
                .ForMember(dest => dest.EncriptedDriverId, act => act.Ignore())
                .ForMember(dest => dest.EncriptedDriverId, act => act.MapFrom(src => Encriptor.EncryptFromLong(src.Id, EncriptObjectType.User)))
                .ForMember(dest => dest.EncriptedPassengerId, act => act.Ignore())
                .ForMember(dest => dest.EncriptedPassengerId, act => act.MapFrom(src => Encriptor.EncryptFromLong(src.Id, EncriptObjectType.User)))
                .ForMember(dest => dest.EncriptedRideId, act => act.Ignore())
                .ForMember(dest => dest.EncriptedRideId, act => act.MapFrom(src => Encriptor.EncryptFromLong(src.Id, EncriptObjectType.Ride)));

            #endregion
        }
    }
}
