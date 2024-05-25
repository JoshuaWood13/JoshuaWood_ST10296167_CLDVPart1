using JoshuaWood_ST10296167_CLDV_POE.Models;
using Microsoft.AspNetCore.Mvc;

namespace JoshuaWood_ST10296167_CLDV_POE.Controllers
{
    public class UserController : Controller
    {
        public UserController()
        {

        }

        public IActionResult UserIndex(/*int userID*/)
        {
            //ViewData["UserID"] = userID;
            return View();
        }

        public IActionResult AddProduct()
        {
            return View();
        }

        public IActionResult Orders()
        {
            return View();
        }
        //public UserModel usrtbl = new UserModel();

        ////[HttpPost]
        ////public ActionResult Login(UserTable Users)
        ////{
        ////    var result = usrtbl.insert_User(Users);
        ////    return RedirectToAction("Index", "Home");
        ////}

        ////[HttpGet]
        ////public ActionResult Login()
        ////{
        ////    return View(usrtbl);
        ////}

        //[HttpPost]
        //public ActionResult SignUp(UserModel Users)
        //{
        //    var result = usrtbl.insert_User(Users);
        //    return RedirectToAction("Index", "Home");
        //}

        //[HttpGet]
        //public ActionResult SignUp()
        //{
        //    return View(usrtbl);
        //}
    }
}
