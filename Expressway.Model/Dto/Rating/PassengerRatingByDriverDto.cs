using System;
using System.Collections.Generic;
using System.Text;

namespace Expressway.Model.Dto.Rating
{
    public class PassengerRatingByDriverDto
    {
        public string EncriptedId { get; set; }
        public string EncriptedPassengerId { get; set; }
        public string EncriptedDriverId { get; set; }
        public string EncriptedRideId { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
    }
}
