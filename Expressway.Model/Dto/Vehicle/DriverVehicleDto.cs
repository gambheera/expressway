using Expressway.Model.Dto.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace Expressway.Model.Dto.Vehicle
{
    public class DriverVehicleDto
    {
        // public long Id { get; set; }
        public string EncriptedId { get; set; }

        // public long DriverId { get; set; }
        public string EncriptedDriverId { get; set; }
        // public UserDto Driver { get; set; }

        // public long VehicleId { get; set; }
        public string EncriptedVehicleId { get; set; }
        // public virtual VehicleDto Vehicle { get; set; }
    }
}
