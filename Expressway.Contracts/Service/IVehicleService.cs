using Expressway.Model.Dto;
using Expressway.Model.Dto.Seat;
using Expressway.Model.Dto.Vehicle;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Expressway.Contracts.Service
{
    public interface IVehicleService
    {
        Task<VehicleDto> CreateAsync(VehicleDto vehicleDto, string encriptedDriverId);
        Task<VehicleDto> UpdateAsync(VehicleDto vehicleDto);
        Task<VehicleDto> GetByIdAsync(string encriptedId);
        Task<List<VehicleDto>> GetAllAsync();
        Task<List<VehicleDto>> GetAllByUserAsync(string encriptedUserId);
        Task<List<SeatDto>> GetSeatPlanByVehicleAsync(string encriptedVehicleId);
        Task<List<VehicleSelectOptionDto>> GetMyVehicleSelectOptionList(string encriptedUserId);
        Task<List<VehicleBrandSelectOptionDto>> GetVehicleBrandSelectOptionList();
        Task<List<VehicleTypeSelectOptionDto>> GetVehicleTypeSelectOptionList();
        Task<List<VehicleModelSelectOptionDto>> GetVehicleModelSelectOptionList(long brandId);

    }
}
