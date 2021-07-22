using System;
using System.Collections.Generic;
using System.Text;

namespace Expressway.Model.Dto.User
{
    public class UserLoginDto
    {
        public string EncriptedUserId { get; set; }
        public string AuthKey { get; set; }
    }
}
