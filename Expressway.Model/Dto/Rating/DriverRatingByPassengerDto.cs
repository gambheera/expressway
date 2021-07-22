using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Expressway.Model.Dto.Rating
{
    public class DriverRatingByPassengerDto
    {
        public string EncriptedId { get; set; }

        public string EncriptedDriverId { get; set; }

        public string EncriptedRideId { get; set; }

        public string Comment { get; set; }
        public int Rating { get; set; }

        public string EncriptedPassengerId { get; set; }
    }

    public class DriverRatingByPassengerDtoValidator : AbstractValidator<DriverRatingByPassengerDto>
    {
        public DriverRatingByPassengerDtoValidator()
        {
            RuleFor(v => v.EncriptedDriverId).NotNull().NotEmpty();
            RuleFor(v => v.EncriptedPassengerId).NotNull().NotEmpty();
            RuleFor(v => v.EncriptedRideId).NotNull().NotEmpty();
        }
    }
}
