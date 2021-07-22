using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Expressway.Model.Domain
{
    public class SeatPlanTemplate
    {
        public long Id { get; set; }
        public string PlanJson { get; set; }
        public virtual ICollection<VehicleModel> VehicleModels { get; set; }
    }
}
