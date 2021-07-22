using Expressway.Model.Domain;
using Expressway.Model.Dto;
using Expressway.Model.Dto.Token;
using Expressway.Model.Dto.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Expressway.Contracts.Service
{
    public interface IUserService
    {
        Task<UserDto> IsValidAuthAsync(AuthUserDto authUser);
        Task<UserDto> CreateAsync(UserDto userDto);
        Task<UserDto> UpdateAsync(UserDto userDto, string encriptedId);
        Task<UserDto> GetByIdAsync(string encriptedId);
        Task<List<UserDto>> GetAllAsync();
        Task<bool> UpdateUserMode(UserModeChangeDto userModeChangeDto, string encriptedUserId);
        Task<bool> SaveRefreshTokenAsync(string encriptedUserId, string refreshToken);
        Task<bool> ValidateRefreshTokenAsync(RefreshTokenRequestDto refreshTokenRequestDto);
        Task<double> GetDriverRatingAsync(string encriptedDriverId);
        Task<double> GetPassengerRatingAsync(string encriptedDriverId);
    }
}
