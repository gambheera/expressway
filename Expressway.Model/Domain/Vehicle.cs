using System;
using System.Collections.Generic;
using System.Text;

namespace Expressway.Model.Domain
{
    public class Vehicle
    {
        public long Id { get; set; }
        public string RegisterNumber { get; set; }

        public long VehicleModelId { get; set; }
        public virtual VehicleModel VehicleModel { get; set; }

        public virtual ICollection<DriverVehicle> DriverVehicles { get; set; }
    }
}
 