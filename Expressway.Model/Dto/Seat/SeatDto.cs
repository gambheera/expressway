using System;
using System.Collections.Generic;
using System.Text;

namespace Expressway.Model.Dto.Seat
{
    public class SeatDto
    {
        // public long Id { get; set; }
        public string EncriptedId { get; set; }
        // public long RideId { get; set; }
        public string EncriptedRideId { get; set; }
        public int RowIndex { get; set; }
        public bool IsLeftCorner { get; set; }
        public bool IsRightCorner { get; set; }
    }
}
