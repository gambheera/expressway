using Expressway.Contracts.Repository;
using Expressway.Database.Context;
using Expressway.Infrastructure.GenericRepository;
using Expressway.Model.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Expressway.Repository.Core
{
    public class VehicleTypeRepository : GenericRepository<VehicleType>, IVehicleTypeRepository
    {
        public VehicleTypeRepository(ExpresswayContext context) : base(context)
        {
        }

        public ExpresswayContext ExpresswayContext => context as ExpresswayContext;
    }
}
