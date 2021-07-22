using System;
using System.Collections.Generic;
using System.Text;

namespace Expressway.Model.Domain
{
    public class UserLogin
    {
        // public long Id { get; set; }
        public virtual User User { get; set; }
        public long UserId { get; set; }
        public string AuthKey { get; set; }
        public bool IsActive { get; set; }
        public bool IsAccountSuspended { get; set; }
        public string RefreshToken { get; set; }
    }
}
