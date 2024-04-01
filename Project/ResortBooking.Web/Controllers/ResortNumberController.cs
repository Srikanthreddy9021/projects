using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ResortBooking.Application.Common.Interfaces;
using ResortBooking.Domain.Entities;
using ResortBooking.Web.ViewModels;

namespace ResortBooking.Web.Controllers
{
    public class ResortNumberController : Controller
    {
        private readonly IUnitOfWork _uw;

        public ResortNumberController(IUnitOfWork uw)
        {
            _uw = uw;
        }
        public IActionResult Index(int resortId)
        {
            List<ResortNumber> resortNumberList = _uw.ResortNumber.GetAll(includeProperties: "Resort").ToList();
            return View(resortNumberList);
        }
        public IActionResult Create()
        {

            ResortNumberVM resortNumberVM = new()
            {
                ResortList = _uw.Resort.GetAll().ToList().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                })
            };
            return View(resortNumberVM);
        }

        [HttpPost]
        public IActionResult Create(ResortNumberVM resortNumberVM)
        {
            //Remove some validations
            ModelState.Remove("ResortNumber.Resort");
            bool isNumberUnique = _uw.ResortNumber
                .GetAll(u => u.Resort_Number == resortNumberVM.ResortNumber.Resort_Number).Count() == 0;

            if (ModelState.IsValid && isNumberUnique)
            {
                _uw.ResortNumber.Add(resortNumberVM.ResortNumber);
                _uw.Save();
                TempData["success"] = "Resort Number Successfully";
                return RedirectToAction(nameof(Index));
            }
            if (!isNumberUnique)
            {
                TempData["error"] = "Resort number already exists. Please use a different resort number.";
            }
            return View(resortNumberVM);
        }

        public IActionResult Update(int resortId)
        {
            ResortNumberVM resortNumberVM = new()
            {
                ResortList = _uw.Resort.GetAll().ToList().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                ResortNumber = _uw.ResortNumber.Get(u => u.Resort_Number == resortId)
            };
            if (resortNumberVM.ResortNumber == null)
            {
                return RedirectToAction("error", "home");
            }
            return View(resortNumberVM);
        }

        [HttpPost]
        public IActionResult Update(ResortNumberVM resortNumberVM)
        {
            ModelState.Remove("ResortNumber.Resort");
            if (ModelState.IsValid)
            {
                _uw.ResortNumber.Update(resortNumberVM.ResortNumber);
                _uw.Save();
                TempData["success"] = "Resort Number Updated Successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(resortNumberVM);
        }

        public IActionResult Delete(int resortId)
        {
            ResortNumberVM resortNumberVM = new()
            {
                ResortList = _uw.Resort.GetAll().ToList().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                ResortNumber = _uw.ResortNumber.Get(u => u.Resort_Number == resortId)
            };
            if (resortNumberVM.ResortNumber == null)
            {
                return RedirectToAction("error", "home");
            }
            return View(resortNumberVM);
        }

        [HttpPost]
        public IActionResult Delete(ResortNumberVM resortNumberVM)
        {
            ResortNumber? objFromDb = _uw.ResortNumber.Get(x => x.Resort_Number == resortNumberVM.ResortNumber.Resort_Number);
            if (objFromDb != null)
            {
                _uw.ResortNumber.Remove(objFromDb);
                _uw.Save();
                TempData["success"] = "Villa Number Deleted Successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(resortNumberVM);
        }
    }
}
