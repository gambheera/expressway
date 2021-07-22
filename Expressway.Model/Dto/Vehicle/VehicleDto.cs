using Expressway.Model.Domain;
using Expressway.Model.Dto.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace Expressway.Model.Dto.Vehicle
{
    public class VehicleDto
    {
        // public long Id { get; set; }
        public string EncriptedId { get; set; }
        public string RegisterNumber { get; set; }

        public long VehicleModelId { get; set; }
        public VehicleModelDto VehicleModel { get; set; }
    }
}
