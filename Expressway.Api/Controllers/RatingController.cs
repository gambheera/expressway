using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Expressway.Contracts.Service;
using Expressway.Model.Dto.Rating;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Expressway.Api.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        IConfiguration config;
        IRatingService ratingService;

        public RatingController(IConfiguration config, IRatingService ratingService)
        {
            this.config = config;
            this.ratingService = ratingService;
        }

        #region GET

        [HttpGet("GetDriverRatingByPassengers/{encriptedDriverId}")]
        public async Task<IActionResult> GetDriverRatingByPassengersAsync(string encriptedDriverId)
        {
            var result = await ratingService.GetDriverRatingByPassengersAsync(encriptedDriverId);
            return Ok(result);
        }

        // [Authorize]
        [HttpGet("GetMyRatingAsADriver")]
        public async Task<IActionResult> GetMyRatingAsADriverAsync()
        {
            var result = await ratingService.GetMyRatingAsADriverAsync(_getUserId());
            return Ok(result);
        }

        [HttpGet("GetMyRatingAsAPassenger")]
        public async Task<IActionResult> GetMyRatingAsAPassengerAsync()
        {
            var result = await ratingService.GetMyRatingAsPassengerAsync(_getUserId());
            return Ok(result);
        }

        [HttpGet("GetPassengerRatingByDriversAsync/{encriptedPassengerId}")]
        public async Task<IActionResult> GetPassengerRatingByDriversAsync(string encriptedPassengerId)
        {
            var result = await ratingService.GetPassengerRatingByDriversAsync(encriptedPassengerId);
            return Ok(result);
        }

        #endregion

        #region POST

        [HttpPost("RatePassengerByDriver")]
        public async Task<IActionResult> RatePassengerByDriver([FromBody] PassengerRatingByDriverDto passengerRatingByDriverDto)
        {
            var result = await ratingService.RatePassengerByDriverAsync(passengerRatingByDriverDto, _getUserId());
            return Ok(result);
        }

        [HttpPost("RateDriverByPassenger")]
        public async Task<IActionResult> RateDriverByPassenger([FromBody] DriverRatingByPassengerDto driverRatingByPassengerDto)
        {
            var result = await ratingService.RateDriverByPassengerAsync(driverRatingByPassengerDto, _getUserId());
            return Ok(result);
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
