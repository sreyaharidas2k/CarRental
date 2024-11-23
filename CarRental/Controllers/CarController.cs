using Microsoft.AspNetCore.Mvc;
using CarRental.Models;
using Microsoft.AspNetCore.Http;
using System.IO;


namespace CarRental.Controllers
{
    public class CarController : Controller
    {
        DBCls dbobj = new DBCls();
        public IActionResult Car_Pageload()
        {
            return View();
        }

        private readonly IWebHostEnvironment _environment;

        public CarController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }




        [HttpPost]
        public IActionResult Car_Click(Carcls carcls)
        {
            if (ModelState.IsValid)
            {
                // Image upload handling
                if (carcls.ImageFile != null)
                {
                    string uploadsFolder = Path.Combine(_environment.WebRootPath, "Images");
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }
                    string imagePath = Path.Combine(uploadsFolder, carcls.ImageFile.FileName);
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        carcls.ImageFile.CopyTo(stream);
                    }
                    carcls.image = "/Images/" + carcls.ImageFile.FileName; // Set image property
                }

                // Insert car data into database
                string resp = dbobj.CarInsert(carcls);
                TempData["msg"] = resp;
                return RedirectToAction("Car_Pageload");
            }
            return View("Car_Pageload", carcls);
        }
    }
}
