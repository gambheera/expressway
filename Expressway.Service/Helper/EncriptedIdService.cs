using Expressway.Contracts.Infrastructure;
using Expressway.Contracts.Service;
using Expressway.Utility.Encriptors;
using System;
using System.Collections.Generic;
using System.Text;

namespace Expressway.Service.Helper
{
    public class EncriptedIdService : IEncriptedIdService
    {
        private readonly IBaseUnitOfWork unitOfWork;

        public EncriptedIdService(IBaseUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public List<string> GetRideEncriptedIdList()
        {
            try
            {
                var rideList = unitOfWork.Rides.GetAll();

                List<string> idList = new List<string>();

                foreach (var item in rideList)
                {
                    idList.Add(item.Id + " -> " + Encriptor.EncryptFromLong(item.Id, Utility.Enums.EncriptObjectType.Ride));
                }

                return idList;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<string> GetSeatEncriptedIdList()
        {
         
            try
            {
                var seatList = unitOfWork.Users.GetAll();

                List<string> SeatsIdList = new List<string>();

                foreach (var item in seatList)
                {
                    SeatsIdList.Add(item.Id + " -> " + Encriptor.EncryptFromLong(item.Id, Utility.Enums.EncriptObjectType.User));
                }

                return SeatsIdList;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<string> GetUserEncriptedIdList()
        {
            //throw new NotImplementedException();

            try
            {
                var userList = unitOfWork.Users.GetAll();

                List<string> UsersIdList = new List<string>();

                foreach (var item in userList)
                {
                    UsersIdList.Add(item.Id + " -> " + Encriptor.EncryptFromLong(item.Id, Utility.Enums.EncriptObjectType.User));
                }

                return UsersIdList;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<string> GetVehicleEncriptedIdList()
        {
            //throw new NotImplementedException();

            try
            {
                var vehicleList = unitOfWork.Vehicles.GetAll();

                List<string> vehicleIdList = new List<string>();

                foreach (var item in vehicleList)
                {
                    vehicleIdList.Add(item.Id + " -> " + Encriptor.EncryptFromLong(item.Id, Utility.Enums.EncriptObjectType.Vehicle));
                }

                return vehicleIdList;
            }
            catch (Exception)
            {

                throw;
            }
        }


        //List<string> GetEntryPointIdList();
        //List<string> GetExitPointIdList();

        //public List<string> GetEntryPointIdList()
        //{
        //    //throw new NotImplementedException();

        //    try
        //    {
        //        var entryPointList = unitOfWork.EntryPoints.GetAll();

        //        List<string> entryPointIdList = new List<string>();

        //        foreach (var item in entryPointList)
        //        {
        //            entryPointIdList.Add(item.Id + " -> " + Encriptor.EncryptFromLong(item.Id, Utility.Enums.EncriptObjectType.EntryPoint));
        //        }

        //        return entryPointIdList;
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        //public List<string> GetExitPointIdList()
        //{
        //    //throw new NotImplementedException();

        //    try
        //    {
        //        var exitPointList = unitOfWork.ExitPoints.GetAll();

        //        List<string> exitPointIdList = new List<string>();

        //        foreach (var item in exitPointList)
        //        {
        //            exitPointIdList.Add(item.Id + " -> " + Encriptor.EncryptFromLong(item.Id, Utility.Enums.EncriptObjectType.ExitPoint));
        //        }

        //        return exitPointIdList;
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}
    }
}
