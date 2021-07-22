using Expressway.Contracts.Repository;
using Expressway.Database.Context;
using Expressway.Infrastructure.GenericRepository;
using Expressway.Model.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Expressway.Repository.Core
{
    public class UserLoginRepository: GenericRepository<UserLogin>, IUserLoginRepository
    {
        public UserLoginRepository(ExpresswayContext context) : base(context)
        {
        }

        public ExpresswayContext TenantDbContext => context as ExpresswayContext;
    }
}
