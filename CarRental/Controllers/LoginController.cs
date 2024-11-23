using Microsoft.AspNetCore.Mvc;
using CarRental.Models;

namespace CarRental.Controllers
{
    public class LoginController : Controller
    {
        DBCls dbobj=new DBCls();
        public IActionResult Login_Pageload()
        {
            return View();
        }
        public IActionResult UserHome()
        {
            return View();
        }
        public IActionResult AdminHome()
        {
            return View();
        }
   
        public IActionResult Login_Click(Logincls logincls)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int cid = dbobj.Logincountid(logincls);
                    if (cid == 1)
                    {
                        int rid = dbobj.Loginregid(logincls);
                        TempData["uid"] = rid;
                        string ltype = dbobj.Logintype(logincls);
                        if (ltype == "Admin")
                        {
                            return RedirectToAction("AdminHome");
                        }
                        else if (ltype == "User")
                        {
                            return RedirectToAction("SelectAll_Pageload", "SelectAll");
                        }
                       
                    }
                    else
                    {
                        ModelState.Clear();
                        // TempData["msg"] = "invalid login";
                        logincls.lmsg = "Invalid Login";
                        return View("Login_Pageload", logincls);
                    }
                }
             
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            return View("Login_Pageload", logincls);

        }
    }
}
