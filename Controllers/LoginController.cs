using JoshuaWood_ST10296167_CLDV_POE.Models;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace JoshuaWood_ST10296167_CLDV_POE.Controllers
{
    public class LoginController : Controller
    {
        private readonly LoginModel login;
        private readonly UserModel usrtbl;

        public LoginController()
        {
            login = new LoginModel();
            usrtbl = new UserModel();
        }

        [HttpPost]
        public ActionResult SignUp(UserModel Users)
        {
            var result = usrtbl.insert_User(Users);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult SignUp()
        {
            return View(usrtbl);
        }

        [HttpPost]
        public ActionResult Login(string email, string password, string accountType)
        {
            var loginModel= new LoginModel();
            int userID = loginModel.selectUser(email, password);

            if(userID != -1)
            {
                HttpContext.Session.SetInt32("IsLoggedIn", 1);
                HttpContext.Session.SetString("AccountType", accountType);
                HttpContext.Session.SetInt32("UserID", userID);
                //ViewBag.IsLoggedIn = IsUserLoggedIn(true);
                if(accountType == "user")
                {
                    return RedirectToAction("UserIndex", "User", new {/*userID = UserID,*/ IsLoggedIn = true });
                }
                else
                {
                    return RedirectToAction("ClientIndex", "Client", new {/*userID = UserID,*/ IsLoggedIn = true });
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View("Login");
        }

        public ActionResult Logout()
        {
            HttpContext.Session.SetInt32("IsLoggedIn", 0);
            HttpContext.Session.Remove("AccountType");
            return RedirectToAction("Index", "Home");
        }

        public bool IsUserLoggedIn(bool b)
        {
            return b;
        }
    }
}
