using Expressway.Model.Dto.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Expressway.Model.Dto.Ride
{
    public class ExchangePointSelectOptionDto : DropdownOption
    {
        public long Latitude { get; set; }
        public long Longitude { get; set; }
    }
}
