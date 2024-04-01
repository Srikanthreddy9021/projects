using Microsoft.AspNetCore.Mvc;
using ResortBooking.Application.Common.Interfaces;
using ResortBooking.Application.Common.Utility;
using ResortBooking.Web.Models;
using ResortBooking.Web.ViewModels;
using System.Diagnostics;

namespace ResortBooking.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _uw;

        public HomeController(IUnitOfWork uw)
        {
            _uw = uw;
        }

        public IActionResult Index()
        {
            HomeVM homeVM = new()
            {
                ResortList = _uw.Resort.GetAll(includeProperties: "ResortAmenity"),
                Nights=1,
                CheckInDate=DateOnly.FromDateTime(DateTime.Now),
            };
            return View(homeVM);
        }
        [HttpPost]
        public IActionResult GetResortbyDate(int nights,DateOnly checkInDate)
        {
           var resortList = _uw.Resort.GetAll(includeProperties: "ResortAmenity").ToList();
            var resortNumberList = _uw.ResortNumber.GetAll().ToList();
            var bookedResort = _uw.Booking.GetAll(u=>u.Status == SD.StatusApproved || u.Status==SD.StatusCheckedIn).ToList();
            foreach (var resort in resortList)
            {
                int roomAvailable = SD.ResortRoomAvailable_Count(resort.Id, resortNumberList, checkInDate, nights, bookedResort);

                resort.IsAvailable = roomAvailable > 0?true:false;
            }

            HomeVM homeVM = new()
            {
                CheckInDate = checkInDate,
                Nights = nights,
                ResortList = resortList
            };

            return PartialView("_ResortList",homeVM);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        
        public IActionResult Error()
        {
            return View();
        }
    }
}
