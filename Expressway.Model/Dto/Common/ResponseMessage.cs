using System;
using System.Collections.Generic;
using System.Text;

namespace Expressway.Model.Dto.Common
{
    public class ResponseMessage
    {
        public bool IsError { get; set; }
        public string Message { get; set; }
        public object EffectedObject { get; set; }
        public object ErrorObject { get; set; }
    }
}
