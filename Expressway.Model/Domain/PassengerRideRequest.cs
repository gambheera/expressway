using Expressway.Model.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Expressway.Model.Domain
{
    public class PassengerRideRequest
    {
        public long Id { get; set; }

        public virtual Ride Ride { get; set; }
        public long RideId { get; set; }

        public int RequestingSeatCount { get; set; }

        //public long SeatId { get; set; }
        //public virtual Seat Seat { get; set; }

        public long PassengerId { get; set; }
        public virtual User Passenger { get; set; }

        public RequestStatus RequestStatus { get; set; }
        public DateTime RequestedTime { get; set; }
    }
}
