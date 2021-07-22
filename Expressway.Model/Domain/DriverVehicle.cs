using System;
using System.Collections.Generic;
using System.Text;

namespace Expressway.Model.Domain
{
    public class DriverVehicle
    {
        public long Id { get; set; }

        public long DriverId { get; set; }
        public User Driver { get; set; }

        public long VehicleId { get; set; }
        public virtual Vehicle Vehicle { get; set; }

        public virtual ICollection<Ride> Rides { get; set; }
        //public long EntryPointId { get; set; }
        //public long ExitPointId { get; set; }
    }
}
