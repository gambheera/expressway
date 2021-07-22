using AutoMapper;
using Expressway.Contracts.Communication;
using Expressway.Contracts.Infrastructure;
using Expressway.Contracts.Service;
using Expressway.Model.Domain;
using Expressway.Model.Dto.MobileVerification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Expressway.Service.Core
{
    public class MobileVerificationService : IMobileVerificationService
    {

        private readonly IBaseUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly ISmsHandler smsHandler;

        public MobileVerificationService(IBaseUnitOfWork unitOfWork, IMapper mapper, ISmsHandler smsHandler)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.smsHandler = smsHandler;
        }

        public Task<bool> SaveAppInstallationCodeAsync(string instalationCode)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SendVerificationCodeBySmsAsync(MobileVerificationDto mobileVerificationDto)
        {
            try
            {
                var existingVerificationRecord = await unitOfWork.MobileNoVerifications.FindAsync(mv => mv.MobileNo == mobileVerificationDto.MobileNo);

                var code = GenerateVerificationCode();
                var message = "Your Verification Code: " + code;

                string mobileNumberForSmsHandler = "+94" + mobileVerificationDto.MobileNo.Substring(1);

                if (existingVerificationRecord == null)
                {
                    // await smsHandler.SendMessage(mobileNumberForSmsHandler, message);

                    var mobileVerification = new MobileNoVerification()
                    {
                        HasMobileNoVerifiedSuccessfully = false,
                        HasVerificationCodeRequested = true,
                        HasVerificationCodeSentSuccessfully = true,
                        SentVerificationCode = "55555", //code,
                        VerificationCodeSuccessfullySentTime = DateTime.UtcNow,
                        VerificationCodeRequestedTime = DateTime.UtcNow,
                        MobileNo = mobileVerificationDto.MobileNo,
                        DeviceUniqueId = mobileVerificationDto.DeviceUniqueId,
                        TryCount = 1
                    };

                    await unitOfWork.MobileNoVerifications.AddAsync(mobileVerification);
                    await unitOfWork.CompleteAsync();
                    return true;
                }


                double duration = DateTime.UtcNow.Subtract(existingVerificationRecord.VerificationCodeSuccessfullySentTime).TotalSeconds;

                if (duration < 120)
                {
                    return false;
                }

                await smsHandler.SendMessage(mobileNumberForSmsHandler, message);

                existingVerificationRecord.HasVerificationCodeSentSuccessfully = true;
                existingVerificationRecord.VerificationCodeRequestedTime = DateTime.UtcNow;
                existingVerificationRecord.SentVerificationCode = code;
                existingVerificationRecord.TryCount += 1;

                await unitOfWork.MobileNoVerifications.UpdateAsync(existingVerificationRecord, existingVerificationRecord.Id);
                await unitOfWork.CompleteAsync();
                return true;
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        public async Task<bool> VerifyUserMobileNoAsync(VerificationCodeConfirmationDto verificationCodeConfirmationDto)
        {
            try
            {
                var availableVerification = await unitOfWork.MobileNoVerifications
                    .FindAsync(mv =>
                                    mv.MobileNo == verificationCodeConfirmationDto.MobileNo &&
                                    mv.SentVerificationCode == verificationCodeConfirmationDto.VerificationCode);

                if (availableVerification == null) { return false; }

                availableVerification.MobileNoSuccessfullyVerifiedTime = DateTime.UtcNow;
                availableVerification.HasMobileNoVerifiedSuccessfully = true;

                await unitOfWork.MobileNoVerifications.UpdateAsync(availableVerification, availableVerification.Id);
                await unitOfWork.CompleteAsync();

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private string GenerateVerificationCode()
        {
            Random random = new Random();
            const string chars = "1234567890";
            return new string(Enumerable.Repeat(chars, 5)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
