using Expressway.Contracts.Repository;
using Expressway.Database.Context;
using Expressway.Infrastructure.GenericRepository;
using Expressway.Model.Domain;

namespace Expressway.Repository.Core
{
    public class PassengerRatingByDriverRepository : GenericRepository<PassengerRatingByDriver>, IPassengerRatingByDriverRepository
    {
        public PassengerRatingByDriverRepository(ExpresswayContext context) : base(context)
        {
        }

        public ExpresswayContext TenantDbContext => context as ExpresswayContext;
    }
}
