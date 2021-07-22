using AutoMapper;
using Expressway.Contracts.Infrastructure;
using Expressway.Contracts.Repository;
using Expressway.Contracts.Service;
using Expressway.Model.Domain;
using Expressway.Model.Dto;
using Expressway.Model.Dto.Seat;
using Expressway.Model.Dto.Vehicle;
using Expressway.Utility.Encriptors;
using Expressway.Utility.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expressway.Service.Core
{
    public class VehicleService : IVehicleService
    {
        private IBaseUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public VehicleService(IBaseUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<List<VehicleDto>> GetAllAsync()
        {
            try
            {
                var vehicleList = await unitOfWork.Vehicles.GetAllAsync();
                var vehicleDtoList = mapper.Map<List<VehicleDto>>(vehicleList);
                return vehicleDtoList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<VehicleDto> GetByIdAsync(string encriptedId)
        {
            try
            {
                long decriptedId = Encriptor.DecryptToLong(encriptedId, EncriptObjectType.Vehicle);
                var vehicle = await unitOfWork.Vehicles.FindAsync(v => v.Id == decriptedId);
                var vehicleDto = mapper.Map<VehicleDto>(vehicle);
                return vehicleDto;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<VehicleDto> CreateAsync(VehicleDto vehicleDto, string encriptedDriverId)
        {

            // 0. Check Availability
            // 1. Decript 'encriptedSeatId' => Decripted seat Id (long)
            // 2. Convert Dto -> Bo
            // 3. Add ride to the db
            // 4. Fill Relationship tables
            // 5. Commit transaction
            // 6. ride, Driver Vehicle Table Confirmed

            try
            {
                // 0 =>
                var existingVehicle = await unitOfWork.Vehicles.FindIncludingAsync(v => v.RegisterNumber == vehicleDto.RegisterNumber, inc => inc.DriverVehicles);

                // 1 => 
                long decriptedDriverId = Encriptor.DecryptToLong(encriptedDriverId, EncriptObjectType.User);

                // 2 => 
                var vehicleBo = mapper.Map<Vehicle>(vehicleDto);


                await unitOfWork.BeginTransactionAsync();


                // 3 =>
                var createdVehicle = existingVehicle == null ? await unitOfWork.Vehicles.AddAsync(vehicleBo) : existingVehicle;


                // 4 => 
                if(existingVehicle != null && existingVehicle.DriverVehicles.Any(dv => dv.DriverId == decriptedDriverId))
                {
                    // This records are already exist
                    return null;
                }

                var driverVehicleBo = new DriverVehicle()
                {
                    DriverId = decriptedDriverId,
                    VehicleId = createdVehicle.Id
                };

                var createdDriverVehicle = await unitOfWork.DriverVehicles.AddAsync(driverVehicleBo);

                // 5 => 
                await unitOfWork.CompleteAsync();

                unitOfWork.CommitTransaction();

                var vehicleToBeReturned = mapper.Map<VehicleDto>(createdVehicle);

                return vehicleToBeReturned;

            }
            catch (Exception ex)
            {
                unitOfWork.RollbackTransaction();
                throw;
            }
        }

        public async Task<VehicleDto> UpdateAsync(VehicleDto vehicleDto)
        {
            try
            {
                if (vehicleDto.EncriptedId == null) { return null; }

                long vehicleId = Encriptor.DecryptToLong(vehicleDto.EncriptedId, EncriptObjectType.Vehicle);

                Vehicle vehicleBusinessObject = mapper.Map<Vehicle>(vehicleDto);

                await unitOfWork.BeginTransactionAsync();
                Vehicle updatedVehicle = await unitOfWork.Vehicles.UpdateAsync(vehicleBusinessObject, vehicleId);
                await unitOfWork.CompleteAsync();

                unitOfWork.CommitTransaction();

                return mapper.Map<VehicleDto>(updatedVehicle);
            }
            catch (Exception ex)
            {
                unitOfWork.RollbackTransaction();
                throw;
            }
        }

        public async Task<List<VehicleDto>> GetAllByUserAsync(string encriptedUserId)
        {
            try
            {
                long driverId = Encriptor.DecryptToLong(encriptedUserId, EncriptObjectType.User);

                // TODO: Need to get this from one query which executing from db level
                var driverVehicles = await unitOfWork.DriverVehicles.FindByAllIncludingAsync(dv => dv.DriverId == driverId,
                    inc => inc.Vehicle,
                    inc => inc.Vehicle.VehicleModel,
                    inc => inc.Vehicle.VehicleModel.VehicleBrand,
                    inc => inc.Vehicle.VehicleModel.VehicleType);

                var vehicles = from dv in driverVehicles
                               select dv.Vehicle;

                var vehicleDtoList = mapper.Map<List<VehicleDto>>(vehicles);
                return vehicleDtoList;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<List<SeatDto>> GetSeatPlanByVehicleAsync(string encriptedVehicleId)
        {
            // TODO: Need to convert JSON to seat list
            throw new NotImplementedException();
        }

        public async Task<List<VehicleSelectOptionDto>> GetMyVehicleSelectOptionList(string encriptedUserId)
        {
            try
            {
                long driverId = Encriptor.DecryptToLong(encriptedUserId, EncriptObjectType.User);
                var driverVehicleList = (await unitOfWork.DriverVehicles.FindByAllIncludingAsync(dv => dv.DriverId == driverId, inc => inc.Vehicle)).ToList();

                var myVehicleList = driverVehicleList.Select(dv => dv.Vehicle);

                var optionList = mapper.Map<List<VehicleSelectOptionDto>>(myVehicleList);

                return optionList;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<VehicleBrandSelectOptionDto>> GetVehicleBrandSelectOptionList()
        {
            try
            {
                var brandList = await unitOfWork.VehicleBrands.GetAllOrderByAsync(vb => vb.Name);
                var brandDtoList = mapper.Map<List<VehicleBrandSelectOptionDto>>(brandList);
                return brandDtoList;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<List<VehicleTypeSelectOptionDto>> GetVehicleTypeSelectOptionList()
        {
            try
            {
                var TypeList = await unitOfWork.VehicleTypes.GetAllOrderByAsync(vt => vt.Name);
                var typeDtoList = mapper.Map<List<VehicleTypeSelectOptionDto>>(TypeList);
                return typeDtoList;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<VehicleModelSelectOptionDto>> GetVehicleModelSelectOptionList(long brandId)
        {
            try
            {
                var modelList = await unitOfWork.VehicleModels.FindAllOrderByAsync(m => m.VehicleBrandId == brandId, m => m.Name);
                var modelDtoList = mapper.Map<List<VehicleModelSelectOptionDto>>(modelList);
                return modelDtoList;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
