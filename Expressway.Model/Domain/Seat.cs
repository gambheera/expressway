using System;
using System.Collections.Generic;
using System.Text;

namespace Expressway.Model.Domain
{
    public class Seat
    {
        public long Id { get; set; }

        public long RideId { get; set; }
        public virtual Ride Ride { get; set; }

        public int RowIndex { get; set; }
        public bool IsLeftCorner { get; set; }
        public bool IsRightCorner { get; set; }

        public virtual ICollection<PassengerRideRequest> PassengerRideRequests { get; set; }
    }
}
