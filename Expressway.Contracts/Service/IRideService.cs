using Expressway.Model.Dto;
using Expressway.Model.Dto.Common;
using Expressway.Model.Dto.Ride;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Expressway.Contracts.Service
{
    public interface IRideService
    {
        Task<List<ExchangePointSelectOptionDto>> GetAllExchangePointsAsync();
        Task<ResponseMessage> CreateRideAsync(RideDto rideDto, string encriptedDriverId);
        Task<ResponseMessage> UpdateRideAsync(RideDto rideDto);
        Task<RideDto> GetByIdAsync(string encriptedId);
        Task<List<RideWithInfoDto>> GetAllAsync();
        Task<ResponseMessage> RequestForRideAsync(RideRequestDto rideRequest, string requestingPassengerEncriptedId);
        Task<ResponseMessage> AcceptRideRequestAsync(string acceptingPassengerEncriptedId, string rideEncriptedId);
        Task<ResponseMessage> RejectRideRequestAsync(string rejectingPassengerEncriptedId, string rideEncriptedId);
        Task<ResponseMessage> CancelByDriverRideRequestAsync(CancelRideByDriverDto cancelRideByDriverDto);
        Task<ResponseMessage> CancelByPassengerRideRequestAsync(string passengerEncriptedId, string rideEncriptedId);
        Task<List<RideWithInfoDto>> GetMyPastRidesAsPassenger(string encriptedUserId, int page, int pageSize);
        Task<List<RideDto>> GetMyPastRidesAsDriver(string encriptedUserId, int page, int pageSize);
        Task<List<RideDto>> GetMyUpcommingRidesAsDriver(string encriptedDriverId);
        Task<List<RideDto>> GetMyUpcommingRidesAsPassenger(string encriptedPassengerId);
        Task<ResponseMessage> MarkRideAsFinished(string encriptedRideId, string encriptedDriverId);
        Task<ResponseMessage> MarkRideAsRiding(string encriptedRideId, string encriptedDriverId);
        Task<RideDto> GetMyNextRideAsDriver(string encriptedDriverId);
        Task<RideDto> GetMyNextRideAPassenger(string encriptedPassengerId);
        Task<long> GetMyCompletedRideCountAsDriver(string encriptedDriverId);
        Task<long> GetMyCompletedRideCountAPassenger(string encriptedPassengerId);
    }

}
