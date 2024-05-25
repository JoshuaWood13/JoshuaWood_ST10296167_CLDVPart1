using JoshuaWood_ST10296167_CLDV_POE.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
using System.Data.SqlClient;

namespace JoshuaWood_ST10296167_CLDV_POE.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductModel prodtbl;
        //public ProductModel prodtbl = new ProductModel();

        public ProductController()
        {
            prodtbl = new ProductModel();
        }

        [HttpPost]
        public ActionResult AddProduct(ProductModel products)
        {
            int userID = HttpContext.Session.GetInt32("UserID") ?? 0;
            var result = prodtbl.insert_product(products, userID);
            return RedirectToAction("UserIndex", "User");
        }

        [HttpGet]
        public ActionResult AddProduct()
        {
            return View(prodtbl);
        }

        [HttpGet]
        public IActionResult MyWork()
        {
            var products = ProductDisplayModel.DisplayProducts();
            return View(products);
        }
    }
}
