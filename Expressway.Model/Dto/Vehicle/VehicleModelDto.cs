using Expressway.Model.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Expressway.Model.Dto.Vehicle
{
    public class VehicleModelDto
    {
        // Test
        public long Id { get; set; }

        public long VehicleBrandId { get; set; }
        public VehicleBrandDto VehicleBrand { get; set; }

        public long VehicleTypeId { get; set; }
        public virtual VehicleTypeDto VehicleType { get; set; }

        public int Year { get; set; }
        public string Name { get; set; }
    }
}
