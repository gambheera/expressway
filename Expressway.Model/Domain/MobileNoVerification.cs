using System;
using System.Collections.Generic;
using System.Text;

namespace Expressway.Model.Domain
{
    public class MobileNoVerification
    {
        public long Id { get; set; }
        public string MobileNo { get; set; }
        public string DeviceUniqueId { get; set; }
        public bool HasVerificationCodeRequested { get; set; }
        public DateTime VerificationCodeRequestedTime { get; set; }
        public bool HasVerificationCodeSentSuccessfully { get; set; }
        public DateTime VerificationCodeSuccessfullySentTime { get; set; }
        public string SentVerificationCode { get; set; }
        public bool HasMobileNoVerifiedSuccessfully { get; set; }
        public DateTime MobileNoSuccessfullyVerifiedTime { get; set; }
        public string VerificationUniqueIdGivenByMobile { get; set; }
        public int TryCount { get; set; }
        public bool IsSuspended { get; set; }
    }
}
