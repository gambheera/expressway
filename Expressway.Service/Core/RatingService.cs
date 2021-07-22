using AutoMapper;
using Expressway.Contracts.Infrastructure;
using Expressway.Contracts.Service;
using Expressway.Model.Domain;
using Expressway.Model.Dto.Rating;
using Expressway.Utility.Encriptors;
using Expressway.Utility.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expressway.Service.Core
{
    public class RatingService : IRatingService
    {
        private IBaseUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public RatingService(IBaseUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<bool> RateDriverByPassengerAsync(DriverRatingByPassengerDto driverRatingByPassengerDto, string encriptedPassengerId)
        {
            try
            {
                // 1. Decript encriptedPassengerId to passengerId
                // 2. Map from driverRatingByPassengerDto to BO
                // 3. Save to database
                // 4. Send result

                // 1 =>
                // long decriptedPassengerId = Encriptor.DecryptToLong(encriptedPassengerId, EncriptObjectType.User);
                driverRatingByPassengerDto.EncriptedPassengerId = encriptedPassengerId;

                // 2 =>
                var driverRatingByPassenger = mapper.Map<DriverRatingByPassenger>(driverRatingByPassengerDto);

                // 3 =>
                await unitOfWork.DriverRatingsByPassenger.AddAsync(driverRatingByPassenger);
                int effectedRowCount = await unitOfWork.CompleteAsync();

                // 4 =>
                return effectedRowCount > 0;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<bool> RatePassengerByDriverAsync(PassengerRatingByDriverDto passengerRatingByDriverDto, string encriptedDriverId)
        {
            try
            {
                // 1. Decript encriptedDriverId to driverId
                // 2. Map from passengerRatingByDriverDto to BO
                // 3. Save to database
                // 4. Send result

                // 1 =>
                long decriptedDriverId = Encriptor.DecryptToLong(encriptedDriverId, EncriptObjectType.User);

                // 2 =>
                var driverRatingByPassenger = mapper.Map<PassengerRatingByDriver>(passengerRatingByDriverDto);

                // 3 =>
                await unitOfWork.PassengerRatingsByDriver.AddAsync(driverRatingByPassenger);
                int effectedRowCount = await unitOfWork.CompleteAsync();

                // 4 =>
                return effectedRowCount > 0;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<RatingDto> GetDriverRatingByPassengersAsync(string encriptedDriverId)
        {
            try
            {
                long decriptedDriverId = Encriptor.DecryptToLong(encriptedDriverId, EncriptObjectType.User);

                var driverRatingByPassengerList = (await unitOfWork.DriverRatingsByPassenger.FindAllAsync(d => d.DriverId == decriptedDriverId)).ToList();

                var ratingDto = new RatingDto()
                {
                    Rating = driverRatingByPassengerList.Average(d => d.Rating),
                    Star1Count = driverRatingByPassengerList.Count(d => d.Rating == 1),
                    Star2Count = driverRatingByPassengerList.Count(d => d.Rating == 2),
                    Star3Count = driverRatingByPassengerList.Count(d => d.Rating == 3),
                    Star4Count = driverRatingByPassengerList.Count(d => d.Rating == 4),
                    Star5Count = driverRatingByPassengerList.Count(d => d.Rating == 5),
                };

                return ratingDto;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<RatingDto> GetPassengerRatingByDriversAsync(string encriptedPassengerId)
        {
            try
            {
                long decriptePassengerId = Encriptor.DecryptToLong(encriptedPassengerId, EncriptObjectType.User);

                var passengerRatingByDriverList = (await unitOfWork.PassengerRatingsByDriver.FindAllAsync(d => d.PassengerId == decriptePassengerId)).ToList();

                var ratingDto = new RatingDto()
                {
                    Rating = passengerRatingByDriverList.Average(d => d.Rating),
                    Star1Count = passengerRatingByDriverList.Count(d => d.Rating == 1),
                    Star2Count = passengerRatingByDriverList.Count(d => d.Rating == 2),
                    Star3Count = passengerRatingByDriverList.Count(d => d.Rating == 3),
                    Star4Count = passengerRatingByDriverList.Count(d => d.Rating == 4),
                    Star5Count = passengerRatingByDriverList.Count(d => d.Rating == 5),
                };

                return ratingDto;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public Task<RatingDto> GetMyRatingAsADriverAsync(string encriptedDriverId)
        {
            return GetDriverRatingByPassengersAsync(encriptedDriverId);
        }

        public Task<RatingDto> GetMyRatingAsPassengerAsync(string encriptedPassengerId)
        {
            return GetPassengerRatingByDriversAsync(encriptedPassengerId);
        }
    }
}
