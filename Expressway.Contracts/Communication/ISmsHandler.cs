using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Expressway.Contracts.Communication
{
    public interface ISmsHandler
    {
        Task<bool> SendMessage(string recipient, string message);
    }
}
