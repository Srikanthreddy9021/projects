using Microsoft.EntityFrameworkCore;
using ResortBooking.Application.Common.Interfaces;
using ResortBooking.Application.Common.Utility;
using ResortBooking.Domain.Entities;
using ResortBooking.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ResortBooking.Infrastructure.Repository
{
    public class BookingRepository : Repository<Booking>, IBookingRepository
    {

        private readonly ApplicationDbContext _db;

        public BookingRepository(ApplicationDbContext db) : base(db) 
        {
            _db = db;
        }
        

        //public void Save()
        //{
        //    _db.SaveChanges();
        //}

        public void Update(Booking entity)
        {
            _db.Bookings.Update(entity);
        }

        public void UpdateStatus(int bookingId, string bookingStatus, int resortNumber)
        {
            var bookingfromdb = _db.Bookings.FirstOrDefault(u => u.Id == bookingId);

            if (bookingfromdb != null)
            {
                bookingfromdb.ResortNumber = resortNumber;
                bookingfromdb.Status = bookingStatus;

                if(bookingStatus == SD.StatusCheckedIn)
                {
                    bookingfromdb.ActualCheckInDate = DateTime.Now;
                }
                if (bookingStatus == SD.StatusCompleted)
                {
                    bookingfromdb.ActualCheckOutDate = DateTime.Now;
                }
            }

        }

        public void UpdateStripePaymentId(int bookingId, string sessionId, string paymentIntentId)
        {
            var bookingfromdb = _db.Bookings.FirstOrDefault(u => u.Id == bookingId);

            if (bookingfromdb != null)
            {
                if(!string.IsNullOrEmpty(sessionId))
                {
                    bookingfromdb.StripeSessionId = sessionId;
                }
                if (!string.IsNullOrEmpty(paymentIntentId))
                {
                    bookingfromdb.StripePaymentIntentId = paymentIntentId;
                   // bookingfromdb.CheckOutDate = DateOnly.FromDateTime( DateTime.Now);
                    bookingfromdb.IsPaymentSuccesful = true;
                }
            }
        }
    }
}
