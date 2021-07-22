using System;
using System.Collections.Generic;
using System.Text;

namespace Expressway.Model.Dto.User
{
    public class AuthUserDto
    {
        public string MobileNo { get; set; }
        public string Password { get; set; }
        public string RemoteIp { get; set; }
    }
}
