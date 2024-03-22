using JoshuaWood_ST10296167_CLDV_POE.Models;
using Microsoft.AspNetCore.Mvc;

namespace JoshuaWood_ST10296167_CLDV_POE.Controllers
{
    public class UserController : Controller
    {

        public UserTable usrtbl = new UserTable();

        [HttpPost]
        public ActionResult About(UserTable Users)
        {
            var result = usrtbl.insert_User(Users);
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public ActionResult About()
        {
            return View(usrtbl);
        }
    }
}
