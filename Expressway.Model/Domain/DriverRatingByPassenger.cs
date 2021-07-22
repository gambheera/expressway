using System;
using System.Collections.Generic;
using System.Text;

namespace Expressway.Model.Domain
{
    public class DriverRatingByPassenger
    {
        // This class is about passenger rating by driver
        public long Id { get; set; }

        public long DriverId { get; set; }
        public virtual User Driver { get; set; }

        public long RideId { get; set; }
        public virtual Ride Ride { get; set; }

        public string Comment { get; set; }
        public int Rating { get; set; }
        public DateTime Time { get; set; }

        public long PassengerId { get; set; }
        public virtual User Passenger { get; set; }
    }
}
