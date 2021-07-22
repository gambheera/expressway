using Expressway.Contracts.Repository;
using Expressway.Database.Context;
using Expressway.Infrastructure.GenericRepository;
using Expressway.Model.Domain;
using Expressway.Utility.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Expressway.Repository.Core
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(ExpresswayContext context) : base(context)
        {
        }

        public ExpresswayContext TenantDbContext => context as ExpresswayContext;
    }
}
