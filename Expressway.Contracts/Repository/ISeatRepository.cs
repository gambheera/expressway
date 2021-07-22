//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace Expressway.Contracts.Repository
//{
//    class ISeatRepository
//    {
//    }
//}

using Expressway.Contracts.Infrastructure;
using Expressway.Model.Domain;

namespace Expressway.Contracts.Repository
{
    public interface ISeatRepository : IGenericRepository<Seat>
    {

    }
}
