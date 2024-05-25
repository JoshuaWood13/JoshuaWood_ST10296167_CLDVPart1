using Microsoft.AspNetCore.Mvc;

namespace JoshuaWood_ST10296167_CLDV_POE.Controllers
{
    public class ClientController : Controller
    {
        public IActionResult ClientIndex()
        {
            return View();
        }

        public IActionResult OrderHistory()
        {
            return View();
        }
    }
}
