using JoshuaWood_ST10296167_CLDV_POE.Models;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;

namespace JoshuaWood_ST10296167_CLDV_POE.Controllers
{
    public class TransactionController : Controller
    {
        [HttpPost]
        //This method processes a clients order and adds it to the transaction table
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
                            return RedirectToAction("ViewOrders", "Transaction");
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
        //This method updates the processing status of an order 
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
        //This method retrieves all orders made by a client along with certain product information for the orders
        public IActionResult ViewOrders(int UserID)
        {
            int userID = HttpContext.Session.GetInt32("UserID") ?? 0;

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
        //This method retrieves all orders made for a users products along with certain transaction and product information
        public IActionResult ViewClientOrders()
        {
            try
            {
                int userID = HttpContext.Session.GetInt32("UserID") ?? 0;

                List<TransactionModel> clientOrders = TransactionModel.GetOrdersForClientUsers(userID);

                // Fetch product details for each order
                foreach (var order in clientOrders)
                {
                    // Fetch product details by product ID
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
