using JoshuaWood_ST10296167_CLDV_POE.Models;
using Microsoft.AspNetCore.Mvc;

namespace JoshuaWood_ST10296167_CLDV_POE.Controllers
{
    public class UserController : Controller
    {
        public UserController()
        {

        }

        public IActionResult UserIndex()
        {
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
    }
}
