using ResortBooking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ResortBooking.Application.Common.Interfaces
{
    public interface IBookingRepository : IRepository<Booking>
    {
       
        void Update(Booking entity);
        void UpdateStatus(int bookingId, string bookingStatus,int resortNumber);
        void UpdateStripePaymentId(int bookingId,string sessionId,string paymentIntentId);
       
    }
}
