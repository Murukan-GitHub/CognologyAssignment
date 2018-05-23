﻿using FlightBook.Data.DomainModel;
using FlightBook.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightBook.Data.Repository
{
    public interface IFlightQueryRepository:IGeneralRepository<BookingDetail>
    {
        IQueryable<BookingDetail> SearchBookings(Nullable<DateTime> startDate, string passengerName = "", string arrivalCity = "", string departureCity = "", string flightNumber = "");
        bool CheckAvailability(DateTime startDate, DateTime endDate, int noOfPax);
        IQueryable<FlightSchedule> GetAllFlightInfo();
    }
}

