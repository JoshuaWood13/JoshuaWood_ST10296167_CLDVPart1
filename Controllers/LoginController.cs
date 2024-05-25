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
        //Inserts new user details into the database then redirects to home page
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
        //Attempts to login user and sets session varaibles accordingly 
        public ActionResult Login(string email, string password, string accountType)
        {
            var loginModel= new LoginModel();
            int userID = loginModel.selectUser(email, password);

            //Checks to see if login was succesful
            if(userID != -1)
            {
                HttpContext.Session.SetInt32("IsLoggedIn", 1);
                HttpContext.Session.SetString("AccountType", accountType);
                HttpContext.Session.SetInt32("UserID", userID);
                if(accountType == "user")
                {
                    return RedirectToAction("UserIndex", "User", new { IsLoggedIn = true });
                }
                else
                {
                    return RedirectToAction("ClientIndex", "Client", new { IsLoggedIn = true });
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

        // Resets session variables 
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
