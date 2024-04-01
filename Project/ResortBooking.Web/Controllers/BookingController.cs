using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ResortBooking.Application.Common.Interfaces;
using ResortBooking.Application.Common.Utility;
using ResortBooking.Domain.Entities;
using Stripe;
using Stripe.Checkout;
using System.Security.Claims;

namespace ResortBooking.Web.Controllers
{
    public class BookingController : Controller
    {
        private readonly IUnitOfWork _uw;

        public BookingController(IUnitOfWork uw)
        {
            _uw = uw;
        }

        [Authorize]
        public IActionResult Index(string status)
        {
            IEnumerable<Booking> objbookings;
            if (User.IsInRole(SD.Role_Admin))
                    {
                        objbookings = _uw.Booking.GetAll(includeProperties:"User,Resort");
                        objbookings = objbookings.Where(u=>u.Status==status).ToList();
                    }
                    else
                    {
                        var claimsIdentity = (ClaimsIdentity)User.Identity;
                        var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

                        objbookings = _uw.Booking.GetAll(u=>u.UserId==userId,includeProperties: "User,Resort");
                        objbookings = objbookings.Where(u=>u.Status==status).ToList();
                    }
                return View(objbookings);
        }

        [Authorize]
        public IActionResult FinalizeBooking(int resortId, DateOnly checkInDate, int nights)
        {
            var claimsIdentity=(ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            ApplicationUser user=_uw.User.Get(u=>u.Id==userId);

            Booking booking = new()
            {
                ResortId = resortId,
                Resort = _uw.Resort.Get(u => u.Id == resortId, includeProperties: "ResortAmenity"),
                CheckInDate = checkInDate,
                Nights = nights,
                CheckOutDate=checkInDate.AddDays(nights),
                UserId = userId,
                Name = user.Name,
                Phone = user.PhoneNumber,
                Email=user.Email

            };
            booking.TotalCost = booking.Resort.Price * nights;
            
            return View(booking);
        }

        [Authorize]
        [HttpPost]
        public IActionResult FinalizeBooking(Booking booking)
        {
            

            var resort = _uw.Resort.Get(u => u.Id == booking.ResortId);

            booking.Status = SD.StatusPending;
            booking .BookingDate=DateTime.Now;
            booking.TotalCost = resort.Price * booking.Nights;


            var resortNumberList = _uw.ResortNumber.GetAll().ToList();
            var bookedResort = _uw.Booking.GetAll(u => u.Status == SD.StatusApproved || u.Status == SD.StatusCheckedIn).ToList();
           
                int roomAvailable = SD.ResortRoomAvailable_Count(resort.Id, resortNumberList, booking.CheckInDate, booking.Nights, bookedResort);

                if (roomAvailable == 0)
            {
                return RedirectToAction(nameof(FinalizeBooking),new
                {
                    resortId = booking.ResortId,
                    checkInDate = booking.CheckInDate,
                    nights = booking.Nights
                });
            }
            

            _uw.Booking.Add(booking);
            _uw.Save();

            var domain = Request.Scheme+"://"+Request.Host.Value+"/";
            var options = new SessionCreateOptions
            {
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment",
                SuccessUrl = domain + $"booking/BookingConfirm?bookingId={booking.Id}",
                CancelUrl = domain + $"booking/FinalizeBooking?resortId={booking.ResortId}&checkInDate={booking.CheckInDate}&nights={booking.Nights}",
            };

            options.LineItems.Add(new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    UnitAmount = (long)(booking.TotalCost * 100),
                    Currency = "inr",
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = resort.Name,
                        //Images = new List<string> { domain +"/"+ resort.ImageUrl },
                    },
                    
                },
                Quantity=1,
            });


            var service = new SessionService();
            Session session = service.Create(options);

            _uw.Booking.UpdateStripePaymentId(booking.Id, session.Id, session.PaymentIntentId);
            _uw.Save();
            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);

           
        }

        [Authorize]
        public IActionResult BookingDetails(int bookingid)
        {
            Booking bookingfromdb = _uw.Booking.Get(u=>u.Id == bookingid,includeProperties:"User,Resort");

            if (bookingfromdb.ResortNumber==0 && bookingfromdb.Status==SD.StatusApproved)
            {
                var availableResortNumber = AvailableResortNumber(bookingfromdb.ResortId);

                bookingfromdb.ResortNumbers=_uw.ResortNumber.GetAll(u=>u.ResortId==bookingfromdb.ResortId && availableResortNumber.Any(x=>x==u.Resort_Number)).ToList();
            }

            return View(bookingfromdb);
            
        }

        [HttpPost]
        [Authorize(Roles =SD.Role_Admin)]
        public IActionResult CheckIn(Booking booking)
        {
            _uw.Booking.UpdateStatus(booking.Id, SD.StatusCheckedIn, booking.ResortNumber);
            _uw.Save();
            return RedirectToAction(nameof(BookingDetails), new { bookingid = booking.Id });
        }

        [HttpPost]
        [Authorize(Roles = SD.Role_Admin)]
        public IActionResult CheckOut(Booking booking)
        {
            _uw.Booking.UpdateStatus(booking.Id, SD.StatusCompleted, booking.ResortNumber);
            _uw.Save();
            return RedirectToAction(nameof(BookingDetails), new { bookingid = booking.Id });
        }

        [HttpPost]
        [Authorize(Roles = SD.Role_Admin)]
        public IActionResult Cancel(Booking booking)
        {
            _uw.Booking.UpdateStatus(booking.Id, SD.StatusCancelled, 0);
            _uw.Save();
            return RedirectToAction(nameof(BookingDetails), new { bookingid = booking.Id });
        }

        private List<int> AvailableResortNumber(int resortid)
        {
            List<int> availableResortNumbers = new();

            var resortNumber = _uw.ResortNumber.GetAll(u=>u.ResortId == resortid);

            var checkedInResort = _uw.Booking.GetAll(u => u.ResortId == resortid && u.Status == SD.StatusCheckedIn).Select(u => u.ResortNumber);

            foreach (var item in resortNumber)
            {
                if (!checkedInResort.Contains(item.Resort_Number))
                {
                    availableResortNumbers.Add(item.Resort_Number);
                }


            }
            return availableResortNumbers;
        }

        [Authorize]
        public IActionResult BookingConfirm(int bookingId)
        {
            Booking bookingfromdb = _uw.Booking.Get(u=>u.Id == bookingId,includeProperties:"User,Resort");

            if(bookingfromdb.Status == SD.StatusPending )
            {
                var service = new SessionService();
                Session session = service.Get(bookingfromdb.StripeSessionId);

                if(session.PaymentStatus == "paid")
                {
                    _uw.Booking.UpdateStatus(bookingfromdb.Id, SD.StatusApproved,0);
                    _uw.Booking.UpdateStripePaymentId(bookingfromdb.Id,session.Id,session.PaymentIntentId);
                    bookingfromdb.PaymentDate=DateTime.Now;
                    _uw.Save();
                }
            }

            return View(bookingId);
        }



    }
}
