using Expressway.Model.Enums;
using Expressway.Utility.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Expressway.Model.Domain
{
    public class User
    {
        public long Id { get; set; }
        public string MobileNo { get; set; }
        public string ExtraMobileNo { get; set; }
        public string Name { get; set; }
        public string Nickname { get; set; }
        public UserMode UserMode { get; set; }
        public string Nic { get; set; }
        public Gender Gender { get; set; }

        public virtual UserLogin UserLogin { get; set; }
        public virtual ICollection<DriverVehicle> DriverVehicles { get; set; }
        public virtual ICollection<PassengerRideRequest> PassengerRideRequests { get; set; }
        public virtual ICollection<DriverRatingByPassenger> DriverRatingsByPassenger { get; set; }
        public virtual ICollection<PassengerRatingByDriver> PassengerRatingsByDriver { get; set; }
    }
}
