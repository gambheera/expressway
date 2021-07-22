using AutoMapper;
using Expressway.Contracts.Infrastructure;
using Expressway.Contracts.Service;
using Expressway.Model.Domain;
using Expressway.Model.Dto;
using Expressway.Model.Dto.Token;
using Expressway.Model.Dto.User;
using Expressway.Utility.Encriptors;
using Expressway.Utility.Enums;
//using Expressway.Model.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expressway.Service.Core
{
    public class UserService : IUserService
    {
        private readonly IBaseUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public UserService(IBaseUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<UserDto> IsValidAuthAsync(AuthUserDto authUser)
        {
            try
            {
                var user = await unitOfWork.Users.FindIncludingAsync(u => u.MobileNo.Equals(authUser.MobileNo), inc => inc.UserLogin);

                //return user == null ? null : (await Hasher.Validate(authUser.Password, user.UserLogin.AuthKey)) ? mapper.Map<UserDto>(user) : null;

                if (user == null) { return null; }

                bool isUserValid = await Hasher.Validate(authUser.Password, user.UserLogin.AuthKey);

                return isUserValid ? mapper.Map<UserDto>(user) : null;

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<List<UserDto>> GetAllAsync()
        {
            try
            {
                var userList = await unitOfWork.Users.GetAllAsync();
                var userDtoList = mapper.Map<List<UserDto>>(userList);
                return userDtoList;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<UserDto> GetByIdAsync(string encriptedId)
        {
            try
            {
                long decriptedId = Encriptor.DecryptToLong(encriptedId, EncriptObjectType.User);
                var user = await unitOfWork.Users.FindAsync(u => u.Id == decriptedId);
                var userDto = mapper.Map<UserDto>(user);
                return userDto;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<UserDto> CreateAsync(UserDto userDto)
        {
            try
            {
                var domainObject = mapper.Map<User>(userDto);

                domainObject.UserLogin.AuthKey = await Hasher.CreatePasswordHash(userDto.UserLogin.AuthKey);
                domainObject.UserLogin.IsActive = true;

                await unitOfWork.BeginTransactionAsync();
                var createdUser = await unitOfWork.Users.AddAsync(domainObject);
                await unitOfWork.CompleteAsync();
                unitOfWork.CommitTransaction();

                return mapper.Map<UserDto>(createdUser);
            }
            catch (Exception ex)
            {
                unitOfWork.RollbackTransaction();
                throw;
            }

        }

        public async Task<UserDto> UpdateAsync(UserDto userDto, string encriptedId)
        {
            try
            {
                var domainObject = mapper.Map<User>(userDto);

                await unitOfWork.BeginTransactionAsync();

                long userId = Encriptor.DecryptToLong(encriptedId, EncriptObjectType.User);
                var uodatedUser = await unitOfWork.Users.UpdateAsync(domainObject, userId);
                await unitOfWork.CompleteAsync();
                unitOfWork.CommitTransaction();

                return mapper.Map<UserDto>(uodatedUser);
            }
            catch (Exception ex)
            {
                unitOfWork.RollbackTransaction();
                throw;
            }

        }

        public async Task<bool> UpdateUserMode(UserModeChangeDto userModeChangeDto, string encriptedUserId)
        {
            try
            {
                long userId = Encriptor.DecryptToLong(encriptedUserId, EncriptObjectType.User);

                if (userId == 0) return false;

                var user = await unitOfWork.Users.FindAsync(u => u.Id == userId);

                if (user == null) return false;

                await unitOfWork.BeginTransactionAsync();
                user.UserMode = userModeChangeDto.NewUserMode;
                await unitOfWork.Users.UpdateAsync(user, userId);
                int effectedRowCount = await unitOfWork.CompleteAsync();
                unitOfWork.CommitTransaction();

                if (effectedRowCount == 1) return true;

                return false;
            }
            catch (Exception ex)
            {
                unitOfWork.RollbackTransaction();
                throw;
            }


        }

        public async Task<bool> SaveRefreshTokenAsync(string encriptedUserId, string refreshToken)
        {
            try
            {
                long userId = Encriptor.DecryptToLong(encriptedUserId, EncriptObjectType.User);
                var userLogin = await unitOfWork.UserLogins.FindAsync(ul => ul.UserId == userId);
                userLogin.RefreshToken = refreshToken;
                await unitOfWork.UserLogins.UpdateAsync(userLogin, userId);
                int effectedRowCount = await unitOfWork.CompleteAsync();

                return effectedRowCount > 0 ? true : false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> ValidateRefreshTokenAsync(RefreshTokenRequestDto refreshTokenRequestDto)
        {
            try
            {
                long userId = Encriptor.DecryptToLong(refreshTokenRequestDto.EncriptedUserId, EncriptObjectType.User);
                var existingRefreshToken = (await unitOfWork.UserLogins.FindAsync(ul => ul.UserId == userId)).RefreshToken;

                return refreshTokenRequestDto.RefreshToken == existingRefreshToken;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<double> GetDriverRatingAsync(string encriptedDriverId)
        {
            try
            {
                long driverId = Encriptor.DecryptToLong(encriptedDriverId, EncriptObjectType.User);

                var ratingList = (await unitOfWork.DriverRatingsByPassenger.FindAllAsync(r => r.DriverId == driverId)).ToList();

                // TODO: Make this return to 0 after filling data to the db
                if(ratingList.Count == 0) { return 3.3; }

                double avg = ratingList.Average(r => r.Rating);

                return avg;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<double> GetPassengerRatingAsync(string encriptedPassengerId)
        {
            try
            {
                long passengerId = Encriptor.DecryptToLong(encriptedPassengerId, EncriptObjectType.User);

                var ratingList = (await unitOfWork.DriverRatingsByPassenger.FindAllAsync(r => r.PassengerId == passengerId)).ToList();

                // TODO: Make this return to 0 after filling data to the db
                if (ratingList.Count == 0) { return 4.3; }

                double avg = ratingList.Average(r => r.Rating);

                return avg;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
