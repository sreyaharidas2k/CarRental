using Microsoft.AspNetCore.Mvc;
using CarRental.Models;

namespace CarRental.Controllers
{
    public class AdminController : Controller
    {
        DBCls dbobj=new DBCls();
        public IActionResult Admin_Pagelaod()
        {
            return View();
        }
        public IActionResult Admin_Click(Admincls admincls)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var getid = dbobj.GetRegId();
                    int regid = 0;
                    if (getid == "")
                    {
                        regid = 1;
                    }
                    else
                    {
                        int newid=Convert.ToInt32(getid);
                        regid = newid + 1;
                    }
                    string resp = dbobj.AdminLoginInsert(admincls,regid);
                    //string msg = dbobj.LoginInsert(admincls);
                    
                    
                    TempData["msg"] = resp;
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            return View("Admin_Pagelaod",admincls);
        }
    }
}
