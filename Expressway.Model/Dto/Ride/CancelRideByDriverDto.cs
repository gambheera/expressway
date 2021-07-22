using System;
using System.Collections.Generic;
using System.Text;

namespace Expressway.Model.Dto.Ride
{
    public class CancelRideByDriverDto
    {
        public string PassengerEncriptedId { get; set; }
        public string RideEncriptedId { get; set; }
    }
}
