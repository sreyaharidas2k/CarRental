using CarRental.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Controllers
{
    public class RentController : Controller
    {
        DBCls dbobj=new DBCls();
        [HttpGet]
        public IActionResult Rent_Pageload(int id)
        {
            var carDetails = dbobj.getcardetails(id);
            var viewModel = new bookingcls
            {
                CarDetails = carDetails,
                RentalDetails = new Rentcls(),
                DailyRate = 500 // Replace with the actual daily rate
            };
            return View(viewModel);
        }
        [HttpPost]
        public IActionResult Rent_Click(int carId, DateTime pickupDate, DateTime dropoffDate, decimal rentalAmount)
        {
            Rentcls rentcls = new Rentcls
            {
                pickup = pickupDate,
                dropoff = dropoffDate,
                totalamt = rentalAmount.ToString(),
                // Add other necessary properties
            };
            string resp = dbobj.RentInsert(rentcls);
            var carDetails = dbobj.getcardetails(carId);
            var viewModel = new bookingcls
            {
                CarDetails = carDetails,
                RentalDetails = new Rentcls(),
                DailyRate = 500
            };
            TempData["msg"] = "Booking successful!";
            return View("Rent_Pageload", viewModel);
        }

    }
}
