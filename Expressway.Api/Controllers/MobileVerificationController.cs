using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Expressway.Contracts.Service;
using Expressway.Model.Dto.MobileVerification;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Expressway.Api.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class MobileVerificationController : ControllerBase
    {
        IConfiguration config;
        private IMobileVerificationService mobileVerificationService;
        public MobileVerificationController(IConfiguration config, IMobileVerificationService mobileVerificationService)
        {
            this.config = config;
            this.mobileVerificationService = mobileVerificationService;
        }

        [HttpPost("SendVerificationCode")]
        public async Task<IActionResult> SendVerificationCodeAsync([FromBody] MobileVerificationDto mobileVerificationDto)
        {
            var result = await mobileVerificationService.SendVerificationCodeBySmsAsync(mobileVerificationDto);
            return Ok(result);
        }

        [HttpPost("VerifyUserMobileNo")]
        public async Task<IActionResult> VerifyUserMobileNoAsync([FromBody] VerificationCodeConfirmationDto verificationCodeConfirmationDto)
        {
            var result = await mobileVerificationService.VerifyUserMobileNoAsync(verificationCodeConfirmationDto);
            return Ok(result);
        }

        [HttpPost("SaveAppInstallationCode")]
        public async Task<IActionResult> SaveAppInstallationCodeAsync([FromBody] string instalationCode)
        {
            var result = await mobileVerificationService.SaveAppInstallationCodeAsync(instalationCode);
            return Ok(result);
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
