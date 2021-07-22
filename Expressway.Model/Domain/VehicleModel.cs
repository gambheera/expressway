using System;
using System.Collections.Generic;
using System.Text;

namespace Expressway.Model.Domain
{
    public class VehicleModel
    {
        public long Id { get; set; }

        public long VehicleBrandId { get; set; }
        public virtual VehicleBrand VehicleBrand { get; set; }

        public long VehicleTypeId { get; set; }
        public virtual VehicleType VehicleType { get; set; }

        public int Year { get; set; }
        public string Name { get; set; }

        public int MaxAvailableSeatCount { get; set; }

        public virtual SeatPlanTemplate SeatPlanTemplate { get; set; }

        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
}
