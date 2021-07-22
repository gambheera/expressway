using Expressway.Model.Dto.MobileVerification;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Expressway.Contracts.Service
{
    public interface IMobileVerificationService
    {
        Task<bool> SendVerificationCodeBySmsAsync(MobileVerificationDto mobileVerificationDto);
        Task<bool> VerifyUserMobileNoAsync(VerificationCodeConfirmationDto verificationCodeConfirmationDto);
        Task<bool> SaveAppInstallationCodeAsync(string instalationCode);
    }
}
