using FlightBook.Application.Common;
using FlightBook.Application.Dto;
using FlightBook.Data.DatabaseContext;
using FlightBook.Data.DomainModel;
using FlightBook.Data.Repository;
using FlightBooking.Data.Repository;
using FlightBooking.Data.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightBook.Application.Mapper
{
    public class BookingDtoMapper
    {
        public IList<PassengerDataDto> Dto { get; set; }
        public long ScheduleID { get; set; }
        public IContext Context { get; set; }
        public BookingDtoMapper(IList<PassengerDataDto> Dto, long ScheduleID, IContext Context)
        {
            this.Dto = Dto;
            this.ScheduleID = ScheduleID;
            this.Context = Context;
        }

        IGeneralRepository<FlightSchedule> _generalRepository;

        BookingDetail DomainModel = new BookingDetail();
        public BookingDetail SetModelProperties()
        {

            DomainModel.Schedule = Context.ScheduleRepo.LoadById(ScheduleID);

            DomainModel.Passenger = Dto.Select(o => new PassengerDetail()
            {
                Address = Context.AddressRepo.LoadById(o.AddressID),
                DateOfBirth = o.DateOfBirth,
                Firstname = o.FirstName,
                Lastname = o.LastName,
                PassportNo = o.PassportNumber
            }).ToList();

            return DomainModel;


        }
    }
}
