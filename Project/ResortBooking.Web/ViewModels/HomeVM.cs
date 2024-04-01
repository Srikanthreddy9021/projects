

using ResortBooking.Domain.Entities;

namespace ResortBooking.Web.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<Resort>? ResortList { get; set; }
        public DateOnly CheckInDate { get; set; }
        public DateOnly? CheckOutDate { get; set; }
        public int Nights { get; set; }
    }
}
