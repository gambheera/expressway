using AutoMapper;
using Expressway.Contracts.Infrastructure;
using Expressway.Contracts.Service;
using Expressway.Model.Domain;
using Expressway.Model.Dto;
using Expressway.Model.Dto.Common;
using Expressway.Model.Dto.Ride;
using Expressway.Model.Enums;
using Expressway.Utility.Encriptors;
using Expressway.Utility.Enums;
//using Expressway.Model.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expressway.Service.Core
{
    public class RideService : IRideService
    {
        private IBaseUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public RideService(IBaseUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<List<ExchangePointSelectOptionDto>> GetAllExchangePointsAsync()
        {
            try
            {
                var exchangePointList = await unitOfWork.ExchangePoints.GetAllAsync();

                var exchangePointDtoList = mapper.Map<List<ExchangePointSelectOptionDto>>(exchangePointList);

                return exchangePointDtoList;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<List<RideWithInfoDto>> GetAllAsync()
        {
            try
            {
                var rideList = await unitOfWork.Rides.FindByAllIncludingAsync(
                    r => r.RideState == RideState.Pending,
                        inc => inc.DriverVehicle,
                        inc => inc.DriverVehicle.Driver,
                        inc => inc.DriverVehicle.Vehicle,
                        inc => inc.DriverVehicle.Vehicle.VehicleModel,
                        inc => inc.DriverVehicle.Vehicle.VehicleModel.VehicleBrand
                    );

                var rideDtoList = mapper.Map<List<RideWithInfoDto>>(rideList);
                return rideDtoList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<RideDto> GetByIdAsync(string encriptedId)
        {
            try
            {
                long decriptedId = Encriptor.DecryptToLong(encriptedId, EncriptObjectType.Ride);
                var ride = await unitOfWork.Rides.FindAsync(u => u.Id == decriptedId);
                var rideDto = mapper.Map<RideDto>(ride);
                return rideDto;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ResponseMessage> CreateRideAsync(RideDto rideDto, string encriptedDriverId)
        {
            // 0. Firstly create repositories and encripted ids for exchange points
            // 1. Decript 'encriptedDriverId' => Decripted driver Id (long)
            // 2. Convert Dto -> Bo
            // 3. Add Vehicle to the db
            // 4. Return status

            try
            {
                // 1 =>
                long decriptedDriverId = Encriptor.DecryptToLong(encriptedDriverId, EncriptObjectType.User);
                long decriptedVehicleId = Encriptor.DecryptToLong(rideDto.EncriptedVehicleId, EncriptObjectType.Vehicle);

                // 2 =>
                var driverVehicle = await unitOfWork.DriverVehicles.FindAsync(dv => dv.DriverId == decriptedDriverId && dv.VehicleId == decriptedVehicleId);
                var rideBo = mapper.Map<Ride>(rideDto);
                rideBo.DriverId = decriptedDriverId;
                rideBo.VehicleId = decriptedVehicleId;
                rideBo.DriverVehicle = driverVehicle;

                // 3 =>
                var createdRide = await unitOfWork.Rides.AddAsync(rideBo);
                int effectedRowCount = await unitOfWork.CompleteAsync();

                // 4 =>
                return new ResponseMessage()
                {
                    IsError = effectedRowCount < 1,
                    Message = effectedRowCount > 0 ? "Successfully Created" : "Something went wrong. Please try again"
                };
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public async Task<ResponseMessage> UpdateRideAsync(RideDto rideDto)
        {
            try
            {
                long rideId = Encriptor.DecryptToLong(rideDto.EncriptedId, EncriptObjectType.Ride);

                int acceptedRequestCount = (await unitOfWork.PassengerRideRequests.FindAllAsync(p => p.RideId == rideId && p.RequestStatus == RequestStatus.Approved)).Count();

                if (acceptedRequestCount < 1)
                {
                    return new ResponseMessage { IsError = true, Message = "Cannot edit. You have approved requests for this ride." };
                }

                var domainObject = mapper.Map<Ride>(rideDto);

                var updatedRide = await unitOfWork.Rides.UpdateAsync(domainObject, rideId);
                await unitOfWork.CompleteAsync();


                return new ResponseMessage() { IsError = false, Message = "Successfully updated." };

            }
            catch (Exception ex)
            {
                unitOfWork.RollbackTransaction();
                throw;
            }

        }

        public async Task<ResponseMessage> RequestForRideAsync(RideRequestDto rideRequest, string requestingPassengerEncriptedId)
        {
            try
            {
                if (rideRequest.RequestingSeatCount < 1)
                {
                    return new ResponseMessage()
                    {
                        IsError = true,
                        Message = "Sorry. Your request is not valid"
                    };
                }

                long decriptedRideId = Encriptor.DecryptToLong(rideRequest.RideEncriptedId, EncriptObjectType.Ride);

                var ride = await unitOfWork.Rides.FindIncludingAsync(r => r.Id == decriptedRideId, inc => inc.PassengerRideRequests);
                var totalAvailableSeats = ride.PassengerRideRequests
                                            .Where(p => p.RequestStatus == RequestStatus.Approved)
                                            .Sum(p => p.RequestingSeatCount);

                if ((ride.AvailableSeatCount - totalAvailableSeats) < rideRequest.RequestingSeatCount)
                {
                    return new ResponseMessage()
                    {
                        IsError = true,
                        Message = "Sorry. Requested seats not available at the moment"
                    };
                }

                long decriptedPassengerId = Encriptor.DecryptToLong(requestingPassengerEncriptedId, EncriptObjectType.User);

                var passengerRideRequest = new PassengerRideRequest()
                {
                    PassengerId = decriptedPassengerId,
                    RideId = decriptedRideId,
                    RequestedTime = DateTime.UtcNow,
                    RequestStatus = RequestStatus.Pending,
                    RequestingSeatCount = rideRequest.RequestingSeatCount
                };

                await unitOfWork.PassengerRideRequests.AddAsync(passengerRideRequest);

                int effectedRowCount = await unitOfWork.CompleteAsync();

                return new ResponseMessage()
                {
                    IsError = effectedRowCount != 1,
                    Message = effectedRowCount == 1 ? "Requested successfully." : "Something went wrong. Please try again"
                };
            }

            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<ResponseMessage> AcceptRideRequestAsync(string acceptingPassengerEncriptedId, string rideEncriptedId)
        {
            return await RespondToRideRequest(acceptingPassengerEncriptedId, rideEncriptedId, RequestStatus.Approved);
        }

        public async Task<ResponseMessage> RejectRideRequestAsync(string rejectingPassengerEncriptedId, string rideEncriptedId)
        {
            return await RespondToRideRequest(rejectingPassengerEncriptedId, rideEncriptedId, RequestStatus.Rejected);
        }

        public async Task<ResponseMessage> CancelByDriverRideRequestAsync(CancelRideByDriverDto cancelRideByDriverDto)
        {
            return await RespondToRideRequest(cancelRideByDriverDto.PassengerEncriptedId, cancelRideByDriverDto.RideEncriptedId, RequestStatus.CancelledByDriver);
        }

        public async Task<ResponseMessage> CancelByPassengerRideRequestAsync(string passengerEncriptedId, string rideEncriptedId)
        {
            return await RespondToRideRequest(passengerEncriptedId, rideEncriptedId, RequestStatus.CancelledByPassenger);
        }

        public async Task<List<RideWithInfoDto>> GetMyPastRidesAsPassenger(string encriptedUserId, int page, int pageSize)
        {
            try
            {
                long decriptedPassengerId = Encriptor.DecryptToLong(encriptedUserId, EncriptObjectType.User);

                var pastRideList = await unitOfWork.Rides.FindAllByPageAsync(r =>
                                                        r.PassengerRideRequests.Where(p => p.PassengerId == decriptedPassengerId).Any(),
                                                        r => r.Id,
                                                        false,
                                                        page,
                                                        pageSize);

                var pastRideDtoList = mapper.Map<List<RideWithInfoDto>>(pastRideList);
                return pastRideDtoList;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<List<RideDto>> GetMyPastRidesAsDriver(string encriptedUserId, int page, int pageSize)
        {
            long decriptedDriverId = Encriptor.DecryptToLong(encriptedUserId, EncriptObjectType.User);

            var pastRideList = await unitOfWork.Rides.FindAllByPageAsync(r => r.DriverId == decriptedDriverId, r => r.Id, false, page, pageSize);

            var pastRideDtoList = mapper.Map<List<RideDto>>(pastRideList);
            return pastRideDtoList;
        }

        public async Task<List<RideDto>> GetMyUpcommingRidesAsDriver(string encriptedDriverId)
        {
            try
            {
                long decriptedDriverId = Encriptor.DecryptToLong(encriptedDriverId, EncriptObjectType.User);

                var upcommingRideList = await unitOfWork.Rides.FindByAllIncludingAsync(r => r.DriverId == decriptedDriverId && r.StartingTime > DateTime.UtcNow, inc => inc.PassengerRideRequests);

                var pastRideDtoList = mapper.Map<List<RideDto>>(upcommingRideList.ToList());
                return pastRideDtoList;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<List<RideDto>> GetMyUpcommingRidesAsPassenger(string encriptedPassengerId)
        {
            try
            {
                long decriptedPassengerId = Encriptor.DecryptToLong(encriptedPassengerId, EncriptObjectType.User);

                var pastRideList = await unitOfWork.Rides.FindAllAsync(r =>
                                                                            r.PassengerRideRequests.Where(p => p.PassengerId == decriptedPassengerId)
                                                                            .Any() && r.StartingTime > DateTime.UtcNow);

                var pastRideDtoList = mapper.Map<List<RideDto>>(pastRideList);
                return pastRideDtoList;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public Task<ResponseMessage> MarkRideAsFinished(string encriptedRideId, string encriptedDriverId)
        {
            return ChangeRideState(encriptedRideId, encriptedDriverId, RideState.Finished);
        }

        public Task<ResponseMessage> MarkRideAsRiding(string encriptedRideId, string encriptedDriverId)
        {
            return ChangeRideState(encriptedRideId, encriptedDriverId, RideState.OnTheWay);
        }

        public async Task<RideDto> GetMyNextRideAsDriver(string encriptedDriverId)
        {
            try
            {
                long driverId = Encriptor.DecryptToLong(encriptedDriverId, EncriptObjectType.User);
                var nextRideList = (await unitOfWork.Rides.FindAllAsync(r => r.StartingTime > DateTime.UtcNow && r.DriverId == driverId));
                var nextRide = nextRideList.OrderByDescending(r => r.StartingTime).FirstOrDefault();
                var nextRideDto = mapper.Map<RideDto>(nextRide);
                return nextRideDto;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<RideDto> GetMyNextRideAPassenger(string encriptedPassengerId)
        {
            try
            {
                long passengerId = Encriptor.DecryptToLong(encriptedPassengerId, EncriptObjectType.User);
                var nextRideList = await unitOfWork.PassengerRideRequests
                    .FindByAllIncludingAsync(prr => 
                        prr.RequestStatus == RequestStatus.Approved && 
                        prr.PassengerId == passengerId &&
                        prr.Ride.StartingTime > DateTime.UtcNow &&
                        (prr.Ride.RideState == RideState.Pending || prr.Ride.RideState == RideState.OnTheWay),
                        inc => inc.Ride);

                var nextRide = nextRideList.OrderByDescending(r => r.Ride.StartingTime).FirstOrDefault();
                var rideDto = mapper.Map<RideDto>(nextRide);

                return rideDto;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<long> GetMyCompletedRideCountAsDriver(string encriptedDriverId)
        {
            try
            {
                long driverId = Encriptor.DecryptToLong(encriptedDriverId, EncriptObjectType.User);
                long rideCount = await unitOfWork.Rides.CountAsync(r => r.DriverId == driverId && r.RideState == RideState.Finished);
                return rideCount;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<long> GetMyCompletedRideCountAPassenger(string encriptedPassengerId)
        {
            try
            {
                long passengerId = Encriptor.DecryptToLong(encriptedPassengerId, EncriptObjectType.User);
                long rideCount = await unitOfWork.PassengerRideRequests
                    .CountAsync(prr => 
                        prr.PassengerId == passengerId && 
                        prr.RequestStatus == RequestStatus.Approved && 
                        prr.Ride.RideState == RideState.Finished);


                return rideCount;
            }
            catch (Exception)
            {

                throw;
            }
        }

        #region Private Methods

        private async Task<ResponseMessage> RespondToRideRequest(string passengerEncriptedId, string rideEncriptedId, RequestStatus requestingRequestStatus)
        {
            try
            {
                try
                {
                    long decriptedRideId = Encriptor.DecryptToLong(rideEncriptedId, EncriptObjectType.Ride);
                    long decriptedPassengerId = Encriptor.DecryptToLong(passengerEncriptedId, EncriptObjectType.User);

                    var rideRequest = await unitOfWork.PassengerRideRequests
                                        .FindIncludingAsync(p =>
                                                                p.RideId == decriptedRideId &&
                                                                p.PassengerId == decriptedPassengerId &&
                                                                p.RequestStatus == RequestStatus.Pending);

                    if (rideRequest == null)
                    {
                        return new ResponseMessage()
                        {
                            IsError = true,
                            Message = "Failed to accept. Please try again."
                        };
                    }

                    rideRequest.RequestStatus = requestingRequestStatus;

                    var resultObject = unitOfWork.PassengerRideRequests.UpdateAsync(rideRequest, rideRequest.Id);
                    await unitOfWork.CompleteAsync();

                    if (resultObject == null)
                    {
                        return new ResponseMessage()
                        {
                            IsError = true,
                            Message = "Failed to accept. Please try again."
                        };
                    }

                    return new ResponseMessage()
                    {
                        IsError = false,
                        Message = "Accepted successfully."
                    };
                }
                catch (Exception ex)
                {

                    throw;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private async Task<ResponseMessage> ChangeRideState(string encriptedRideId, string encriptedDriverId, RideState requestingRideStatus)
        {
            try
            {
                long decriptedRideId = Encriptor.DecryptToLong(encriptedRideId, EncriptObjectType.Ride);
                long decriptedDriverId = Encriptor.DecryptToLong(encriptedDriverId, EncriptObjectType.User);

                var existingRide = await unitOfWork.Rides.FindAsync(r => r.Id == decriptedRideId);

                if (existingRide.DriverId == decriptedDriverId)
                {
                    return new ResponseMessage()
                    {
                        IsError = true,
                        Message = "Failed to complete. Driver details are not matching."
                    };
                }

                existingRide.RideState = requestingRideStatus;

                var updatedObject = await unitOfWork.Rides.UpdateAsync(existingRide, existingRide.Id);

                if (updatedObject == null)
                {
                    return new ResponseMessage()
                    {
                        IsError = true,
                        Message = "Failed to complete. Please try again."
                    };
                }

                return new ResponseMessage()
                {
                    IsError = false,
                    Message = "Successfully completed"
                };
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        #endregion
    }
}

