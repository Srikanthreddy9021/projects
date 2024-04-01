using ResortBooking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortBooking.Application.Common.Utility
{
    public static class SD
    {
        public const string Role_Customer = "Customer";
        public const string Role_Admin = "Admin";

        public const string StatusPending = "Pending";
        public const string StatusApproved = "Approved";
        public const string StatusCheckedIn = "CheckedIn";
        public const string StatusCompleted = "Completed";
        public const string StatusCancelled = "Cancelled";
        public const string StatusRefunded = "Refunded";


        public static int ResortRoomAvailable_Count(int resortId,
            List<ResortNumber> resortNumberList,DateOnly checkInDate, int nights,
            List<Booking> bookings)
        {
            List<int> bookingDate = new();
            int finalAvailableRoomsForAllNights = int.MaxValue;
            var roomsInResort = resortNumberList.Where(u => u.ResortId == resortId).Count();

            for(int i = 0;i<nights;i++)
            {
                var resortBooked = bookings.Where(u=> u.CheckInDate<= checkInDate.AddDays(i) &&
                u.CheckOutDate>checkInDate.AddDays(i) && u.ResortId==resortId);

                foreach(var booking in resortBooked)
                {
                    if (!bookingDate.Contains(booking.Id))
                    {
                        bookingDate.Add(booking.Id);
                    }
                }

                var totalRoomsAvailable = roomsInResort - bookingDate.Count;
                if(totalRoomsAvailable == 0)
                {
                    return 0;
                }
                else
                {
                    if (finalAvailableRoomsForAllNights > totalRoomsAvailable)
                    {
                        finalAvailableRoomsForAllNights = totalRoomsAvailable;
                    }
                }

                
            }

            return finalAvailableRoomsForAllNights;
        }


        
    }
}
