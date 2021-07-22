using System;
using System.Collections.Generic;
using System.Text;

namespace Expressway.Model.Dto.Token
{
    public class TokenResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime Expiration { get; set; }
    }
}
