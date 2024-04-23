using JoshuaWood_ST10296167_CLDV_POE.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace JoshuaWood_ST10296167_CLDV_POE.Controllers
{
    public class ProductController : Controller
    {
        public ProductTable prodtbl = new ProductTable();

        [HttpPost]
        public ActionResult MyWork(ProductTable products)
        {
            var result = prodtbl.insert_product(products);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult MyWork()
        {
            return View(prodtbl);
        }
    }
}
