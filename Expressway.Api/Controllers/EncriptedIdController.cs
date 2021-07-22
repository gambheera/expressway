using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Expressway.Contracts.Service;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Expressway.Api.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class EncriptedIdController : ControllerBase
    {
        IConfiguration config;
        private readonly IEncriptedIdService encriptedIdService;

        public EncriptedIdController(IConfiguration config, IEncriptedIdService encriptedIdService)
        {
            this.config = config;
            this.encriptedIdService = encriptedIdService;
        }


        [HttpGet("RideEncriptedId")]
        public IActionResult Get()
        {
            var rideResult = encriptedIdService.GetRideEncriptedIdList();

            return Ok(rideResult);
        }

        [HttpGet("UserEncriptedId")]

        public IActionResult GetUser()
        {
            var userResult = encriptedIdService.GetUserEncriptedIdList();

            return Ok(userResult);
        }

        [HttpGet("SeatEncriptedId")]

        public IActionResult GetSeat()
        {
            var seatResult = encriptedIdService.GetSeatEncriptedIdList();

            return Ok(seatResult);
        }


        [HttpGet("VehicleEncriptedId")]

        public IActionResult GetVehicle()
        {
            var vehicleResult = encriptedIdService.GetVehicleEncriptedIdList();

            return Ok(vehicleResult);
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
