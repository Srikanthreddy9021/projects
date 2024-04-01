using Microsoft.AspNetCore.Mvc;
using ResortBooking.Application.Common.Interfaces;
using ResortBooking.Application.Common.Utility;
using ResortBooking.Web.ViewModels;
using System.Linq;

namespace ResortBooking.Web.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IUnitOfWork _uw;
        static int preMonth= DateTime.Now.Month==1 ? 12 : DateTime.Now.Month-1;
        readonly DateTime previousMonthStartDate = new(DateTime.Now.Year, preMonth, 1);
        readonly DateTime currentMonthStartDate = new(DateTime.Now.Year, DateTime.Now.Month , 1);

        public DashboardController(IUnitOfWork uw)
        {
            _uw = uw;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetTotalRadialChart()
        {
            var totalbookings = _uw.Booking.GetAll(u=>u.Status!=SD.StatusPending || u.Status==SD.StatusCancelled);
            var countByCurrentMonth = totalbookings.Count(u=>u.BookingDate>=currentMonthStartDate &&
            u.BookingDate<=DateTime.Now);
            var countByPreviousMonth = totalbookings.Count(u => u.BookingDate >= previousMonthStartDate &&
            u.BookingDate <= currentMonthStartDate);

            return Json(GetRadialChartData(totalbookings.Count(), countByCurrentMonth, countByPreviousMonth));
        }

        public async Task<IActionResult> GetRegisteredUserChart()
        {
            var totalusers = _uw.User.GetAll();
            var countByCurrentMonth = totalusers.Count(u => u.CreatedAt >= currentMonthStartDate &&
            u.CreatedAt <= DateTime.Now);
            var countByPreviousMonth = totalusers.Count(u => u.CreatedAt >= previousMonthStartDate &&
            u.CreatedAt <= currentMonthStartDate);



            return Json(GetRadialChartData(totalusers.Count(), countByCurrentMonth, countByPreviousMonth));
        }

        public async Task<IActionResult> GetRevenueChart()
        {
            //var totalusers = _uw.User.GetAll();

            var totalbookings = _uw.Booking.GetAll(u => u.Status != SD.StatusPending || u.Status == SD.StatusCancelled);
            var totalRevenue = Convert.ToInt32(totalbookings.Sum(u => u.TotalCost));
            var countByCurrentMonth = totalbookings.Where(u => u.BookingDate >= currentMonthStartDate &&
           u.BookingDate <= DateTime.Now).Sum(u => u.TotalCost);
            var countByPreviousMonth = totalbookings.Where(u => u.BookingDate >= previousMonthStartDate &&
            u.BookingDate <= currentMonthStartDate).Sum(u => u.TotalCost); ;



            return Json(GetRadialChartData(totalRevenue, countByCurrentMonth, countByPreviousMonth));
        }


        public async Task<IActionResult> GetMemberAndBookingLineChart()
        {
            var bookingData = _uw.Booking.GetAll(u => u.BookingDate >= DateTime.Now.AddDays(-30))
                .GroupBy(u => u.BookingDate.Date)
                .Select(u => new
                {
                    DateTime = u.Key,
                    NewBookingCount = u.Count(),
                });

            var customerData = _uw.User.GetAll(u => u.CreatedAt >= DateTime.Now.AddDays(-30))
                .GroupBy(u => u.CreatedAt.Date)
                .Select(u => new
                {
                    DateTime = u.Key,
                    NewCustomerCount = u.Count(),
                });

            var leftJoin = bookingData.GroupJoin(customerData, booking => booking.DateTime, customer => customer.DateTime,
                (booking, customer) => new
                {
                    booking.DateTime,
                    booking.NewBookingCount,
                    NewCustomerCount = customer.Select(u => u.NewCustomerCount).DefaultIfEmpty(0).FirstOrDefault()
                });

            var rightJoin = customerData.GroupJoin(bookingData, customer => customer.DateTime, booking => booking.DateTime,
                (customer,booking) => new
                {
                    customer.DateTime,
                    customer.NewCustomerCount,
                    NewBookingCount = booking.Select(u => u.NewBookingCount).DefaultIfEmpty(0).FirstOrDefault()
                });
            //var mergedata1 = leftJoin.Union(rightJoin).OrderBy(x => x.DateTime).ToList();

            var mergeData = leftJoin.Select(x => new
            {
                DateTime = x.DateTime,
                NewBookingCount = x.NewBookingCount,
                NewCustomerCount = x.NewCustomerCount
            }).Union(rightJoin.Select(x => new
            {
                DateTime = x.DateTime,
                NewBookingCount = x.NewBookingCount,
                NewCustomerCount = x.NewCustomerCount
            })).OrderBy(x => x.DateTime).ToList();

            var newBookingData = mergeData.Select(u=>u.NewBookingCount).ToArray();
            var newCustomerData = mergeData.Select(u=>u.NewCustomerCount).ToArray();
            var categories = mergeData.Select(u=>u.DateTime.ToString("MM/dd/yyyy")).ToArray();


            List<ChartData> chartDataList = new()
            {
                new ChartData
                {
                    Name = "New Bookings",
                    Data = newBookingData
                },
                new ChartData
                {
                    Name="New Members",
                    Data=newCustomerData
                }
            };

            LineChartVM lineChartVM = new()
            {
                Categories = categories,
                Series = chartDataList
            };

            

            return Json(lineChartVM);
        }

        public async Task<IActionResult> GetBookingPieChart()
        {


            var totalbookings = _uw.Booking.GetAll(u => u.BookingDate >= DateTime.Now.AddDays(-30) &&
            (u.Status != SD.StatusPending || u.Status == SD.StatusCancelled));

            var customerWithOneBooking = totalbookings.GroupBy(u => u.UserId).Where(x => x.Count() == 1).Select(x => x.Key).ToList();

            int bookingsByNewCustomer = customerWithOneBooking.Count();
            int bookingsByOldCustomer = totalbookings.Count() - bookingsByNewCustomer;

            PieChartVM pieChartVM = new()
            {
                Labels = new string[] { "New Customer Booking", "Existing Customer Booking" },
                Series = new decimal[] { bookingsByNewCustomer, bookingsByOldCustomer }
            };



            return Json(pieChartVM);
        }


        private static RadialBarChartVM GetRadialChartData(int totalCount, double currentMonthCount, double previousMonthCount)
        {
            RadialBarChartVM radialBarChartVM = new();

            int increaseDeacreaseRatio = 100;

            if (previousMonthCount != 0)
            {
                increaseDeacreaseRatio = Convert.ToInt32((currentMonthCount - previousMonthCount) / previousMonthCount * 100);
            }

            radialBarChartVM.TotalCount = totalCount;
            radialBarChartVM.CountInCurrentMonth = Convert.ToInt32(currentMonthCount);
            radialBarChartVM.HasIncreased = currentMonthCount > previousMonthCount;
            radialBarChartVM.Series = new int[] { increaseDeacreaseRatio };

            return radialBarChartVM;
        }
    }
}
