using Expressway.Model.Enums;
using Expressway.Utility.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Expressway.Model.Dto.User
{
    public class UserDto
    {
        public string EncriptedId { get; set; }
        public string Name { get; set; }
        public string Nickname { get; set; }
        public UserMode UserMode { get; set; }
        public string Nic { get; set; }
        public string MobileNo { get; set; }
        public string ExtraMobileNo { get; set; }
        public Gender Gender { get; set; }

        public UserLoginDto UserLogin { get; set; }
    }
}
