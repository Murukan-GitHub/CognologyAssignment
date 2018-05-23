using FlightBook.Data.DatabaseContext;
using FlightBook.Data.DomainModel;
using FlightBook.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightBooking.Data.Repository
{
    public class ScheduleRepository : GeneralRepository<FlightSchedule>, IScheduleRepository
    {
        public ScheduleRepository(FlightManageBookDBContext db) : base(db)
        { }
    }
}
