using Expressway.Model.Domain;
using Expressway.Model.Dto.ExchangePoint;
using Expressway.Model.Dto.Seat;
using Expressway.Model.Dto.User;
using Expressway.Model.Dto.Vehicle;
using Expressway.Model.Enums;
using Expressway.Utility.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Expressway.Model.Dto.Ride
{
    public class RideDto
    {
        public string EncriptedId { get; set; }
        public string EncriptedDriverId { get; set; }
        public string EncriptedVehicleId { get; set; }
        public DateTime StartingTime { get; set; }
        public long EntryPointId { get; set; }
        public long ExitPointId { get; set; }
        public int AproximateTimeDuration { get; set; }
        public int AvailableSeatCount { get; set; }
        public decimal Amount { get; set; }
        public RideState RideState { get; set; }


        // public List<SeatDto> Seats { get; set; }
    }
}
