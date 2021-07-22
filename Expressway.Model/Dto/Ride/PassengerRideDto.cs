using Expressway.Model.Domain;
using Expressway.Model.Dto.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace Expressway.Model.Dto.Ride
{
    public class PassengerRideDto
    {
        public long Id { get; set; }
        public UserDto Passanger { get; set; }
        public RideDto Ride { get; set; }
    }

    
}
