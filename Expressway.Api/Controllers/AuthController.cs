using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Expressway.Model.Dto.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Expressway.Contracts.Service;
using Expressway.Model.Helper;
using Microsoft.Extensions.Options;
using System.Net;
using Expressway.Model.Dto.Token;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Cors;

namespace Expressway.Api.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        IConfiguration config;
        IUserService userService;
        private readonly AuthOptions _authOptions;

        public AuthController(IConfiguration config, IOptions<AuthOptions> authOptionsAccessor, IUserService userService)
        {
            this.userService = userService;
            _authOptions = authOptionsAccessor.Value;
            this.config = config;
        }

        [HttpPost("token")]
        public async Task<IActionResult> Post([FromBody] AuthUserDto authUser)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            var userDto = await userService.IsValidAuthAsync(authUser);

            if (userDto == null) { return Unauthorized(); }

            var tokenResponse = await GetToken(userDto.EncriptedId);

            return Ok(tokenResponse);
        }

        [HttpPost("refreshtoken")]
        public async Task<IActionResult> RefreshTokenAsync([FromBody] RefreshTokenRequestDto refreshTokenRequestDto)
        {
            bool isValid = await userService.ValidateRefreshTokenAsync(refreshTokenRequestDto);

            if (isValid)
            {
                return Ok(await GetToken(refreshTokenRequestDto.EncriptedUserId));
            }

            return Unauthorized();
        }


        [HttpGet("refreshtoken/{userEncriptedId}")]
        private async Task<TokenResponse> GetToken(string userEncriptedId)
        {
            string refreshToken = Guid.NewGuid().ToString();
            bool isSaved = await userService.SaveRefreshTokenAsync(userEncriptedId, refreshToken);

            if (!isSaved) { return null; }

            var authClaims = new[]
            {
                    new Claim(JwtRegisteredClaimNames.NameId, userEncriptedId),
                    new Claim(JwtRegisteredClaimNames.AuthTime, DateTime.UtcNow.ToString())
                };

            var token = new JwtSecurityToken(
                issuer: _authOptions.Issuer,
                audience: _authOptions.Audience,
                expires: DateTime.Now.AddHours(_authOptions.ExpiresInMinutes),
                claims: authClaims,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authOptions.SecureKey)), SecurityAlgorithms.HmacSha256Signature)
                );

            var tokenResponse = new TokenResponse()
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken = refreshToken,
                Expiration = token.ValidTo
            };

            return tokenResponse;
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
