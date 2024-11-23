using Microsoft.AspNetCore.Mvc;
using CarRental.Models;

namespace CarRental.Controllers
{
    public class SelectAllController : Controller
    {
        DBCls dbobj=new DBCls();
        public IActionResult SelectAll_Pageload()
        {
            List<Carcls> getlist = dbobj.SelectAllDetails();

            return View(getlist);
        }
        

    }
}
