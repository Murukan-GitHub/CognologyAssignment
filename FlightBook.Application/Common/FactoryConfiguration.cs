using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using FlightBook.Data.Repository;
using FlightBook.Application.Services;
using FlightBook.Application.Services;
using FlightBooking.Data.Repository.Interface;

namespace FlightBook.Application.Common
{
    class FactoryConfiguration
    {
        private static object locker = new object();

        public static void Configure()
        {
            lock (locker)
            {
                if (!Factory.HasBeenInitialised)
                {
                    UnityContainer container = Factory.Container;
                    container.RegisterType<IFlightInfoService, FlightInfoService>();
                }
            }
        }
    }
}