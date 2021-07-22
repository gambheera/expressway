using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Expressway.Contracts.Service;
using Expressway.Model.Dto;
using Expressway.Model.Dto.Vehicle;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Expressway.Api.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        IConfiguration config;
        IVehicleService vehicleService;
        public VehicleController(IConfiguration config, IVehicleService vehicleService)
        {
            this.config = config;
            this.vehicleService = vehicleService;
        }

        // GET: api/Vehicle
        [HttpGet("GetAllVehicles")]
        public async Task<IActionResult> GetAllAsync()
        {
            var vehicleDtoResponse = await vehicleService.GetAllAsync();
            return Ok(vehicleDtoResponse);
        }

        // GET: api/Vehicle/4
        [HttpGet("GetVehicleById/{encriptedId}")]
        public async Task<IActionResult> GetByIdAsync(string encriptedId)
        {
            var vehicle = await vehicleService.GetByIdAsync(encriptedId);
            return Ok(vehicle);
        }

        [HttpGet("GetMyAllVehicles")]
        public async Task<IActionResult> GetAllByUserAsync()
        {
            var vehicleList = await vehicleService.GetAllByUserAsync(_getUserId());
            return Ok(vehicleList);
        }

        [HttpGet("SeatPlan/{encriptedId}")]
        public async Task<IActionResult> GetSeatPlanByVehicleAsync(string encriptedVehicleId)
        {
            var seatPlan = await vehicleService.GetSeatPlanByVehicleAsync(encriptedVehicleId);
            return Ok(seatPlan);
        }

        [HttpGet("MyVehileOptionList")]
        public async Task<IActionResult> GetMyVehicleSelectOptionList()
        {
            var optionList = await vehicleService.GetMyVehicleSelectOptionList(_getUserId());
            return Ok(optionList);
        }

        [HttpGet("VehileBrandOptionList")]
        public async Task<IActionResult> GetMyVehicleBrandSelectOptionList()
        {
            var optionList = await vehicleService.GetVehicleBrandSelectOptionList();
            return Ok(optionList);
        }

        [HttpGet("VehileTypeOptionList")]
        public async Task<IActionResult> GetMyVehicleTypeSelectOptionList()
        {
            var optionList = await vehicleService.GetVehicleTypeSelectOptionList();
            return Ok(optionList);
        }

        [HttpGet("VehileModelOptionList/{brandId}")]
        public async Task<IActionResult> GetMyVehicleModelSelectOptionList(long brandId)
        {
            var optionList = await vehicleService.GetVehicleModelSelectOptionList(brandId);
            return Ok(optionList);
        }

        // POST: api/Vehicle
        [HttpPost("PostVehicle")]
        public async Task<IActionResult> PostAsync([FromBody] VehicleDto vehicleDto)
        {
            var vehicleDtoResponse = await vehicleService.CreateAsync(vehicleDto, _getUserId());

            return Ok(vehicleDtoResponse);
        }

        // PUT: api/Vehicle
        [HttpPut("PutVehicle/{id}")]
        public async Task<IActionResult> PutAsync([FromBody]  VehicleDto vehicleDto)
        {
            var vehicleDtoResponse = await vehicleService.UpdateAsync(vehicleDto);

            return Ok(vehicleDtoResponse);
        }

        #region private methods
        private string _getUserId()
        {
            if (config.GetValue<bool>("DeveloperSettings:IsDeveloperMode"))
            {
                return config.GetValue<string>("DeveloperSettings:DevEncriptedUserId");
            }

            var encriptedUserId = User.FindFirst("NameId").Value;
            return encriptedUserId;
        }

        #endregion
    }
}

