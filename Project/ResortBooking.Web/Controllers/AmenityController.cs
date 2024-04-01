using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ResortBooking.Application.Common.Interfaces;
using ResortBooking.Application.Common.Utility;
using ResortBooking.Domain.Entities;
using ResortBooking.Web.ViewModels;

namespace ResortBooking.Web.Controllers
{
    [Authorize(Roles = SD.Role_Admin)]
    public class AmenityController : Controller
    {
        private readonly IUnitOfWork _uw;

        public AmenityController(IUnitOfWork uw)
        {
            _uw = uw;
        }
        public IActionResult Index(int amenityId)
        {
            List<Amenity> AmenityList = _uw.Amenity.GetAll(includeProperties: "Resort").ToList();
            return View(AmenityList);
        }
        public IActionResult Create()
        {

            AmenityVM amenityVM = new()
            {
                ResortList = _uw.Resort.GetAll().ToList().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                })
            };
            return View(amenityVM);
        }

        [HttpPost]
        public IActionResult Create(AmenityVM amenityVM)
        {
            //Remove some validations
            //ModelState.Remove("Amenity.Resort");
            //bool isNumberUnique = _uw.Amenity
            //    .GetAll(u => u.Resort_Number == AmenityVM.Amenity.Resort_Number).Count() == 0;

            if (ModelState.IsValid )
            {
                _uw.Amenity.Add(amenityVM.Amenity);
                _uw.Save();
                TempData["success"] = "amenity created Successfully";
                return RedirectToAction(nameof(Index));
            }
           
            

            return View(amenityVM);
        }

        public IActionResult Update(int amenityId)
        {
            AmenityVM amenityVM = new()
            {
                ResortList = _uw.Resort.GetAll().ToList().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                Amenity = _uw.Amenity.Get(u => u.Id == amenityId)
            };
            if (amenityVM.Amenity == null)
            {
                return RedirectToAction("error", "home");
            }
            return View(amenityVM);
        }

        [HttpPost]
        public IActionResult Update(AmenityVM amenityVM)
        {
            ModelState.Remove("Amenity.Resort");
            if (ModelState.IsValid)
            {
                _uw.Amenity.Update(amenityVM.Amenity);
                _uw.Save();
                TempData["success"] = "amenity Updated Successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(amenityVM);
        }

        public IActionResult Delete(int amenityId)
        {
            AmenityVM amenityVM = new()
            {
                ResortList = _uw.Resort.GetAll().ToList().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                Amenity = _uw.Amenity.Get(u => u.Id == amenityId)
            };
            if (amenityVM.Amenity == null)
            {
                return RedirectToAction("error", "home");
            }
            return View(amenityVM);
        }

        [HttpPost]
        public IActionResult Delete(AmenityVM amenityVM)
        {
            Amenity? objFromDb = _uw.Amenity.Get(x => x.Id == amenityVM.Amenity.Id);
            if (objFromDb != null)
            {
                _uw.Amenity.Remove(objFromDb);
                _uw.Save();
                TempData["success"] = "amenity Deleted Successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(amenityVM);
        }
    }
}
