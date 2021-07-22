using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Expressway.Contracts.Service;
using Expressway.Model.Dto;
using Expressway.Model.Dto.Ride;
using Expressway.Service.Core;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Expressway.Api.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class RideController : ControllerBase
    {
        IConfiguration config;
        IRideService rideService;

        public RideController(IConfiguration config, IRideService rideService)
        {
            this.rideService = rideService;
            this.config = config;
        }

        #region GET

        [HttpGet("GetAllRides")]
        public async Task<IActionResult> GetAllAsync()
        {
            var rideDtoResponse = await rideService.GetAllAsync();
            return Ok(rideDtoResponse);
        }

        [HttpGet("AllExchangePoints")]
        public async Task<IActionResult> GetAllExchangePointsAsync()
        {
            var exchangePointDtoResponse = await rideService.GetAllExchangePointsAsync();
            return Ok(exchangePointDtoResponse);
        }

        [HttpGet("{encriptedId}")]
        public async Task<IActionResult> GetByIdAsync(string encriptedId)
        {
            var ride = await rideService.GetByIdAsync(encriptedId);
            return Ok(ride);
        }

        [HttpGet("NewRides/{latitude}/{longitude}")]
        public IActionResult GetNewRidesAsync(string latitude, string longitude)
        {
            // TODO: This method should be an async one
            return Ok();
        }

        [HttpGet("GetMyPastRidesAsPassenger/{page}/{pageSize}")]
        public IActionResult GetMyPastRidesAsPassenger(int page, int pageSize)
        {
            var result = rideService.GetMyPastRidesAsPassenger(_getUserId(), page, pageSize);
            return Ok(result);
        }

        [HttpGet("GetMyPastRidesAsDriver/{page}/{pageSize}")]
        public IActionResult GetMyPastRidesAsDriver(int page, int pageSize)
        {
            var result = rideService.GetMyPastRidesAsDriver(_getUserId(), page, pageSize);
            return Ok(result);
        }

        [HttpGet("GetMyUpcommingRidesAsDriver")]
        public async  Task<IActionResult> GetMyUpcommingRidesAsDriver()
        {
            var result = await rideService.GetMyUpcommingRidesAsDriver(_getUserId());
            return Ok(result);
        }

        [HttpGet("GetMyUpcommingRidesAsPassenger")]
        public IActionResult GetMyUpcommingRidesAsPassenger()
        {
            var result = rideService.GetMyUpcommingRidesAsPassenger(_getUserId());
            return Ok(result);
        }

        #endregion

        #region POST

        [HttpPost("PostRide")]
        public async Task<IActionResult> PostAsync([FromBody] RideDto rideDto)
        {

            var rideDtoResponse = await rideService.CreateRideAsync(rideDto, _getUserId());
            return Ok(rideDtoResponse);
        }

        [HttpPost("RequestForRide")]
        public async Task<IActionResult> RequestForRideAsync([FromBody] RideRequestDto rideRequest)
        {
            var result = await rideService.RequestForRideAsync(rideRequest, _getUserId());
            return Ok(result);
        }

        [HttpPost("AcceptRideRequest")]
        public async Task<IActionResult> AcceptRideRequestAsync([FromBody] string rideEncriptedId)
        {
            var result = await rideService.AcceptRideRequestAsync(_getUserId(), rideEncriptedId);
            return Ok(result);
        }

        [HttpPost("RejectRideRequest")]
        public async Task<IActionResult> RejectRideRequestAsync([FromBody] string rideEncriptedId)
        {
            var result = await rideService.RejectRideRequestAsync(_getUserId(), rideEncriptedId);
            return Ok(result);
        }

        [HttpPost("CancelByDriverRideRequest")]
        public async Task<IActionResult> CancelByDriverRideRequestAsync([FromBody] CancelRideByDriverDto cancelRideByDriverDto)
        {
            var result = await rideService.CancelByDriverRideRequestAsync(cancelRideByDriverDto);
            return Ok(result);
        }

        [HttpPost("CancelByPassengerRideRequest")]
        public async Task<IActionResult> CancelByPassengerRideRequest([FromBody] string rideEncriptedId)
        {
            var result = await rideService.CancelByPassengerRideRequestAsync(_getUserId(), rideEncriptedId);
            return Ok(result);
        }

        #endregion

        #region PUT

        [HttpPut("PutRide/{id}")]
        public async Task<IActionResult> PutAsync([FromBody] RideDto rideDto)
        {
            var rideDtoResponse = await rideService.UpdateRideAsync(rideDto);
            return Ok(rideDtoResponse);
        }

        #endregion

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
