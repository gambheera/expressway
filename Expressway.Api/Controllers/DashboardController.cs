using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
// using AutoMapper.Configuration;
using Expressway.Contracts.Service;
using Expressway.Model.Helper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Expressway.Api.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        IConfiguration config;
        IUserService userService;
        IRideService rideService;

        private readonly AuthOptions _authOptions;

        public DashboardController(IConfiguration config, IUserService userService, IRideService rideService, IOptions<AuthOptions> authOptionsAccessor)
        {
            this.config = config;
            this.userService = userService;
            this.rideService = rideService;
            _authOptions = authOptionsAccessor.Value;
        }

        #region Driver

        [HttpGet("GetMyRatingAsADriver")]
        public async Task<IActionResult> GetMyRatingAsADriver()
        {
            double myDriverRating = await userService.GetDriverRatingAsync(_getUserId());
            return Ok(myDriverRating);
        }

        [HttpGet("GetMyNexRidetAsADriver")]
        public async Task<IActionResult> GetMyNextRideAsADriver()
        {
            var myNextRide = await rideService.GetMyNextRideAsDriver(_getUserId());
            return Ok(myNextRide);
        }

        [HttpGet("GetMyCompletedRideCountAsDriver")]
        public async Task<IActionResult> GetMyCompletedRideCountAsDriver()
        {
            var myRideCount = await rideService.GetMyCompletedRideCountAsDriver(_getUserId());
            return Ok(myRideCount);
        }

        #endregion

        #region Passenger

        [HttpGet("GetMyRatingAsAPassenger")]
        public async Task<IActionResult> GetMyRatingAsAPassenger()
        {
            double myDriverRating = await userService.GetPassengerRatingAsync(_getUserId());
            return Ok(myDriverRating);
        }

        [HttpGet("GetMyNextRideAsAPassenger")]
        public async Task<IActionResult> GetMyNextRideAsAPassenger()
        {
            var myNextRide = await rideService.GetMyNextRideAPassenger(_getUserId());
            return Ok(myNextRide);
        }

        [HttpGet("GetMyCompletedRideCountAsPassenger")]
        public async Task<IActionResult> GetMyCompletedRideCountAsPassenger()
        {
            var myRideCount = await rideService.GetMyCompletedRideCountAPassenger(_getUserId());
            return Ok(myRideCount);
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
