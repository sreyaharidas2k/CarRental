using Microsoft.AspNetCore.Mvc;
using CarRental.Models;

namespace CarRental.Controllers
{
    public class UserController : Controller
    {
        DBCls dbobj=new DBCls();
        public IActionResult UserLogin_Pageload()
        {
            return View();
        }
        public IActionResult UserLogin_Click(Usercls usercls)
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
                        int newid = Convert.ToInt32(getid);
                        regid = newid + 1;
                        
                    }
                    string resp = dbobj.UserLoginInsert(usercls, regid);
                    //string msg = dbobj.LoginInsert(admincls);


                    TempData["msg"] = resp;
                    return RedirectToAction("SelectAll_Pageload","SelectAll");
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            return View("UserLogin_Pageload", usercls);
        }
    }
}
