using FlightBook.Data.DomainModel;
using FlightBook.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightBooking.Data.Repository
{
  public  interface IScheduleRepository : IGeneralRepository<FlightSchedule>
    {
    }
}
