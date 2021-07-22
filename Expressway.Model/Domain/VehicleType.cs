using System;
using System.Collections.Generic;
using System.Text;

namespace Expressway.Model.Domain
{
    public class VehicleType
    {
        public long Id { get; set; }
        public string Name { get; set; }
        // public virtual SeatPlanTemplate SeatPlanTemplate { get; set; }
        public virtual ICollection<VehicleModel> VehicleModels { get; set; }
    }
}
