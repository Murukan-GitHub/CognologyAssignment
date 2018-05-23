using FlightBook.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightBooking.Data.Repository.Interface
{
    public interface IContext : IDisposable      
    {
        IFlightQueryRepository flightRepo {get;}
        IAddressRepository AddressRepo { get; }
        IScheduleRepository ScheduleRepo { get; }
        void Complete();
    }
}
