using JoshuaWood_ST10296167_CLDV_POE.Models;
using Microsoft.AspNetCore.Mvc;

namespace JoshuaWood_ST10296167_CLDV_POE.Controllers
{
    public class LoginController : Controller
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}

        private readonly LoginModel login;

        public LoginController()
        {
            login = new LoginModel();
        }
    }
}
