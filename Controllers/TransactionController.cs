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
                using(SqlConnection con = new SqlConnection(ProductModel.con_string))
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

        [HttpPost]
        public IActionResult UpdateProcessingStatus(int transactionID, string status)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(TransactionModel.connectionString))
                {
                    string sql = "UPDATE TransactionTable SET ProcessingStatus = @Status WHERE TransactionID = @TransactionID";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@Status", status);
                        cmd.Parameters.AddWithValue("@TransactionID", transactionID);
                        con.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        con.Close();

                        if (rowsAffected > 0)
                        {
                            return RedirectToAction("ViewClientOrders");
                        }
                        else
                        {
                            return View("Error");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

        [HttpGet]
        public IActionResult ViewOrders(int UserID)
        {
            int userID = HttpContext.Session.GetInt32("UserID") ?? 0;
            // Call the GetTransactionsByUserID method from the TransactionModel class
            List<TransactionModel> userTransactions = TransactionModel.GetTransactionsByUserID(userID);

            // Fetch product details for each transaction
            foreach (var transaction in userTransactions)
            {
                // Call a method to fetch product details by product ID
                ProductDisplayModel product = ProductDisplayModel.GetProductDetailsByUserID(transaction.ProductID);

                // Assign product details to the transaction object
                transaction.Product = product;
            }

            // Pass the transaction data to the view
            return View("Orders",userTransactions);
        }

        [HttpGet]
        public IActionResult ViewClientOrders()
        {
            try
            {
                int userID = HttpContext.Session.GetInt32("UserID") ?? 0;

                // Call the GetOrdersForClientUsers method from the TransactionModel class
                List<TransactionModel> clientOrders = TransactionModel.GetOrdersForClientUsers(userID);

                // Fetch product details for each order
                foreach (var order in clientOrders)
                {
                    // Call a method to fetch product details by product ID
                    ProductDisplayModel product = ProductDisplayModel.GetProductDetailsByUserID(order.ProductID);

                    // Assign product details to the order object
                    order.Product = product;
                }

                // Pass the order data to the view
                return View("ClientOrders", clientOrders);
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }
    }
}
