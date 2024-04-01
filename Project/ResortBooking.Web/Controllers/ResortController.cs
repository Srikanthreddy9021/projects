using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ResortBooking.Application.Common.Interfaces;
using ResortBooking.Application.Common.Utility;
using ResortBooking.Domain.Entities;
using ResortBooking.Infrastructure.Data;

namespace ResortBooking.Web.Controllers
{
    
    public class ResortController : Controller
    {
        private readonly IUnitOfWork _uw;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ResortController(IUnitOfWork uw, IWebHostEnvironment webHostEnvironment)
        {
            _uw = uw;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            var resorts= _uw.Resort.GetAll();
            return View(resorts);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Resort obj)
        {
            if (obj.Name == obj.Description)
            {
                ModelState.AddModelError("description", "The Description and name cannot be the same");
            }
            if (ModelState.IsValid)
            {
                if (obj.Image != null)
                {
                    string filename= Guid.NewGuid().ToString()+ Path.GetExtension(obj.Image.FileName);
                    string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, @"images\Resort");

                    using var filestream = new FileStream(Path.Combine(imagePath, filename), FileMode.Create);
                    
                    obj.Image.CopyTo(filestream);
                    obj.ImageUrl = @"images\Resort\" + filename;
                    
                }
                else
                {
                    obj.ImageUrl = @"images\no image.png";
                }

                _uw.Resort.Add(obj);
                _uw.Save();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public IActionResult Update(int resortId)
        { 
            Resort? obj = _uw.Resort.Get(x => x.Id == resortId);
            //Resort? obj1 = _db.Resorts.Find(resortId);
            //var ResortList = _db.Resorts.Where(u=>u.Price > 50 && u.Occupancy>0).ToList();
            if (obj == null)
            {
                return RedirectToAction("Error","Home");
            }
            return View(obj);
        }

        [HttpPost]
        public IActionResult Update(Resort obj)
        {
            if (obj.Name == obj.Description)
            {
                ModelState.AddModelError("description", "The Description and name cannot be the same");
            }
            if (ModelState.IsValid)
            {
                if (obj.Image != null)
                {
                    string filename = Guid.NewGuid().ToString() + Path.GetExtension(obj.Image.FileName);
                    string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, @"images\Resort");

                    if (!string.IsNullOrEmpty(obj.ImageUrl))
                    {
                        var oldImagepath= Path.Combine(_webHostEnvironment.WebRootPath,obj.ImageUrl);

                        if (System.IO.File.Exists(oldImagepath))
                        {
                            System.IO.File.Delete(oldImagepath);
                        }
                    }

                    using var filestream = new FileStream(Path.Combine(imagePath, filename), FileMode.Create);

                    obj.Image.CopyTo(filestream);
                    obj.ImageUrl = @"images\Resort\" + filename;

                }
                //else
                //{
                //    obj.ImageUrl = @"images\default.jpg";
                //}


                _uw.Resort.Update(obj);
                _uw.Save();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public IActionResult Delete(int resortId)
        {
            Resort? obj = _uw.Resort.Get(x => x.Id == resortId);
            
            if (obj is null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(obj);
        }

        [HttpPost]
        public IActionResult Delete(Resort obj)
        {
            Resort? objdb = _uw.Resort.Get(x => x.Id == obj.Id);
            if (objdb is not null)
            {

                if (!string.IsNullOrEmpty(objdb.ImageUrl))
                {
                    var oldImagepath = Path.Combine(_webHostEnvironment.WebRootPath, objdb.ImageUrl);

                    if (System.IO.File.Exists(oldImagepath))
                    {
                        System.IO.File.Delete(oldImagepath);
                    }
                }

                _uw.Resort.Remove(objdb);
                _uw.Save();
                return RedirectToAction("Index");
            }
            return View(obj);
        }
    }
    
}
