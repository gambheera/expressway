using System;
using System.Collections.Generic;
using System.Text;

namespace Expressway.Model.Dto.Ride
{
    public class RideRequestDto
    {
        public string RideEncriptedId { get; set; }
        public int RequestingSeatCount { get; set; }
    }
}
