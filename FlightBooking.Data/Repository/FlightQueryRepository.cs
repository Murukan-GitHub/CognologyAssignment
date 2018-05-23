using FlightBook.Data.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FlightBook.Data.Repository;
using System.Data.Entity;
using FlightBook.Data.DatabaseContext;

namespace FlightBook.Data.Repository
{
    public class FlightQueryRepository : GeneralRepository<BookingDetail>, IFlightQueryRepository
    {
        protected DbSet<BookingDetail> thisentity = null;
        public FlightQueryRepository(FlightManageBookDBContext db):base(db)
        {
        }

        public bool CheckAvailability(DateTime startDate, DateTime endDate, int noOfPax)
        {

            var thisSchedleEntity = db.Set<FlightSchedule>();
            var AvailableSlots = (from p in thisSchedleEntity.Where(o => o.StartTime >= startDate && o.EndTime <= endDate).AsQueryable()
                                  select new { ID = p.Id, Pax = p.MaximumPax }).ToList();

            long[] _ids = AvailableSlots.Select(a => a.ID).ToArray();           
            long BookedPax = entity.AsQueryable().Where(x=>_ids.Contains(x.ScheduleID)).Select(e => e.Passenger).ToList().Count();
            
            return (AvailableSlots.Sum(o => o.Pax) - BookedPax) > 0;

        }

        public IQueryable<BookingDetail> SearchBookings(Nullable<DateTime> startDate, string passengerName = "", string arrivalCity = "", string departureCity = "", string flightNumber = "")
        {
            var result = entity.Include(e => e.Passenger).Include(o => o.Schedule)
                         .AsQueryable();
            if (startDate != null)
                result = result.Where(o => o.Schedule.StartTime >= startDate).AsQueryable();

            if (!string.IsNullOrEmpty(passengerName))
                result = result.Where(o => (o.Passenger.Select(e => e.Firstname + " " + e.Lastname)).Contains(passengerName)).AsQueryable();

            if (!string.IsNullOrEmpty(arrivalCity))
                result = result.Where(o => o.Schedule.ArrivalCity.Code == arrivalCity).AsQueryable();

            if (!string.IsNullOrEmpty(flightNumber))
                result = result.Where(o => o.Schedule.Flight.FlightNo == flightNumber).AsQueryable();

            return result;
        }

        IQueryable<FlightSchedule> IFlightQueryRepository.GetAllFlightInfo()
        {
            var thisentity = db.Set<FlightSchedule>();
            var result = thisentity.Include(e => e.Flight).Include(o => o.DepartureCity).Include(a => a.ArrivalCity)
                        .AsQueryable();

            return result;
        }

        public FlightManageBookDBContext FlightContext
        {
         get { return db as FlightManageBookDBContext; }
        }


    }
}
