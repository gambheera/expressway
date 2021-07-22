using Expressway.Model.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Expressway.Model.Dto.Ride
{
    public class RideWithInfoDto
    {
        public string EncriptedId { get; set; }
        public string EncriptedDriverId { get; set; }
        public string DriverNickname { get; set; }
        public string EncriptedVehicleId { get; set; }
        public string VehicleModelWithBrand { get; set; }
        public string VehicleRegisterNumber { get; set; }
        public DateTime StartingTime { get; set; }
        public long EntryPointId { get; set; }
        public string EntryPointName { get; set; }
        public long ExitPointId { get; set; }
        public string ExitPointName { get; set; }
        public int AproximateTimeDuration { get; set; }
        public int AvailableSeatCount { get; set; }
        public decimal Amount { get; set; }
        public RideState RideState { get; set; }
        // public List<SeatDto> Seats { get; set; }
    }
}
