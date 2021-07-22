using Expressway.Model.Dto.Rating;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Expressway.Contracts.Service
{
    public interface IRatingService
    {
        Task<bool> RateDriverByPassengerAsync(DriverRatingByPassengerDto driverRatingByPassengerDto, string encriptedPassengerId);
        Task<bool> RatePassengerByDriverAsync(PassengerRatingByDriverDto passengerRatingByDriverDto, string encriptedDriverId);
        Task<RatingDto> GetDriverRatingByPassengersAsync(string encriptedDriverId);
        Task<RatingDto> GetPassengerRatingByDriversAsync(string encriptedPassengerId);
        Task<RatingDto> GetMyRatingAsADriverAsync(string encriptedDriverId);
        Task<RatingDto> GetMyRatingAsPassengerAsync(string encriptedPassengerId);
    }
}
