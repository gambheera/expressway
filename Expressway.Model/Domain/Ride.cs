using Expressway.Model.Enums;
using Expressway.Utility.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Expressway.Model.Domain
{
    public class Ride
    {
        public long Id { get; set; }

        public long DriverId { get; set; }
        public long VehicleId { get; set; }
        public virtual DriverVehicle DriverVehicle { get; set; }

        public DateTime StartingTime { get; set; }

        public long EntryPointId { get; set; }
        public virtual ExchangePoint EntryPoint { get; set; }

        public long ExitPointId { get; set; }
        public virtual ExchangePoint ExitPoint { get; set; }

        public int AproximateTimeDuration { get; set; }
        public RideState RideState { get; set; }

        public int AvailableSeatCount { get; set; }
        public decimal Amount { get; set; }

        public virtual ICollection<PassengerRideRequest> PassengerRideRequests { get; set; }
        public virtual ICollection<DriverRatingByPassenger> DriverRatingsByPassenger { get; set; }
        public virtual ICollection<PassengerRatingByDriver> PassengerRatingsByDriver { get; set; }
        public virtual ICollection<Seat> Seats { get; set; }
    }
}

