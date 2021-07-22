using System;
using System.Collections.Generic;
using System.Text;

namespace Expressway.Model.Domain
{
    public class VehicleBrand
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string LogoUrl { get; set; }

        public virtual ICollection<VehicleModel> VehicleModels { get; set; }
    }
}
