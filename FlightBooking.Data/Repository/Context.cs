using FlightBook.Data.DatabaseContext;
using FlightBook.Data.Repository;
using FlightBooking.Data.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightBooking.Data.Repository
{
    public class Context : IContext
    {
        protected FlightManageBookDBContext db = null;
        public Context(FlightManageBookDBContext context)
        {
            db = context;
            AddressRepo = new AddressRepository(context);
            ScheduleRepo = new ScheduleRepository(context);
            flightRepo = new FlightQueryRepository(context);


        }

        private bool disposed = false;


        public IFlightQueryRepository flightRepo { get; private set; }

        public IAddressRepository AddressRepo { get; private set; }

        public IScheduleRepository ScheduleRepo { get; private set; }

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        public virtual void Complete()
        {
            try
            {

                db.SaveChanges();

            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                List<string> errorMessages = new List<string>();
                foreach (DbEntityValidationResult validationResult in ex.EntityValidationErrors)
                {
                    string entityName = validationResult.Entry.Entity.GetType().Name;
                    foreach (DbValidationError error in validationResult.ValidationErrors)
                    {
                        errorMessages.Add(entityName + "." + error.PropertyName + ": " + error.ErrorMessage);
                    }
                }

            }
        }
    }
}
