using System;
using System.Collections.Generic;
using System.Text;

namespace Expressway.Model.Dto.Token
{
    public class RefreshTokenRequestDto
    {
        public string EncriptedUserId { get; set; }
        public string RefreshToken { get; set; }
    }
}
