using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Expressway.Model.Dto.Vehicle
{
    public class VehicleTypeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AvailableSeats { get; set; }
    }

    public class VehicleTypeDtoValidator : AbstractValidator<VehicleTypeDto>
    {
        public VehicleTypeDtoValidator()
        {
            RuleFor(v => v.Id).GreaterThan(0);
            RuleFor(v => v.Name).NotEmpty().NotNull().WithMessage("Invalid vehicle type");
            RuleFor(v => v.AvailableSeats).GreaterThan(0);
        }
    }
}
