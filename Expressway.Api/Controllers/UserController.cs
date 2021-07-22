using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Expressway.Contracts.Service;
using Expressway.Model.Dto;
using Expressway.Model.Dto.User;
using Expressway.Model.Helper;
using Expressway.Utility.Enums;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Expressway.Api.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IConfiguration config;
        IUserService userService;
        private readonly AuthOptions _authOptions;

        public UserController(IConfiguration config, IUserService userService, IOptions<AuthOptions> authOptionsAccessor)
        {
            this.config = config;
            this.userService = userService;
            _authOptions = authOptionsAccessor.Value;
        }


        [HttpGet("token")]
        public IActionResult Get()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var authClaims = new[]
           {
                    new Claim(JwtRegisteredClaimNames.Sub, "85554"),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

            var token = new JwtSecurityToken(
                issuer: _authOptions.Issuer,
                audience: _authOptions.Audience,
                expires: DateTime.Now.AddHours(_authOptions.ExpiresInMinutes),
                claims: authClaims,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authOptions.SecureKey)),
                    SecurityAlgorithms.HmacSha256Signature)
                );

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            });

            return Unauthorized();
        }


        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllAsync()
        {
            var userDtoResponse = await userService.GetAllAsync();
            return Ok(userDtoResponse);
        }


        [HttpGet("GetUserById/{encriptedId}")]
        public async Task<IActionResult> GetByIdAsync(string encriptedId)
        {
            var user = await userService.GetByIdAsync(encriptedId);
            return Ok(user);
        }


        [HttpPost("PostUser")]
        public async Task<IActionResult> PostAsync([FromBody] UserDto userDto)
        {

            var userDtoResponse = await userService.CreateAsync(userDto);
            return Ok(userDtoResponse);
        }


        [HttpPut("PutUser/{id}")]
        public async Task<IActionResult> PutAsync([FromBody] UserDto userDto)
        {
            var vehicleDtoResponse = await userService.UpdateAsync(userDto, _getUserId());
            return Ok(vehicleDtoResponse);
        }

        [HttpPatch("PatchUser")]
        public async Task<IActionResult> ChangeUserMode([FromBody] UserModeChangeDto userModeChangeDto)
        {
            bool isChanged = await userService.UpdateUserMode(userModeChangeDto, _getUserId());

            if (!isChanged) return NotFound();

            return Ok(isChanged);
        }

        //[HttpPost]

        //public async Task<IActionResult> UserLogin ([FromBody] UserLoginDto userLoginDto)
        //{
        //    var userLoginDtoResponse = await userService.CreateAsync(userLoginDto);
        //    return Ok(userLoginDtoResponse);
        //}

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
