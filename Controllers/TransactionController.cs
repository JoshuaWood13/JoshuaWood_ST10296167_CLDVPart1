using JoshuaWood_ST10296167_CLDV_POE.Models;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;

namespace JoshuaWood_ST10296167_CLDV_POE.Controllers
{
    public class TransactionController : Controller
    {
        [HttpPost]
        public ActionResult PlaceOrder(int UserID, int ProductID)
        {
            try
            {
                using(SqlConnection con = new SqlConnection(ProductTable.con_string))
                {
                    string sql = "INSERT INTO TransactionTable (UserID, ProductID) VALUES (@UserID, @ProductID)";

                    using(SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@UserID", UserID);
                        cmd.Parameters.AddWithValue("@ProductID", ProductID);
                        con.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        con.Close();

                        if(rowsAffected > 0)
                        {
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            return View("Error");
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                return View("Error");
            }
        }
    }
}
