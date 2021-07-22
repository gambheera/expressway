using System;
using System.Collections.Generic;
using System.Text;

namespace Expressway.Contracts.Service
{
    public interface IEncriptedIdService
    {
        List<string> GetRideEncriptedIdList();
        List<string> GetUserEncriptedIdList();
        List<string> GetSeatEncriptedIdList();
        List<string> GetVehicleEncriptedIdList();
        //List<string> GetEntryPointIdList();
        //List<string> GetExitPointIdList();


    }
}
