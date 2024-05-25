using System.Data.SqlClient;

namespace JoshuaWood_ST10296167_CLDV_POE.Models
{
    public class TransactionModel
    {
        public int TransactionID { get; set; }
        public int UserID { get; set; }
        public int ProductID { get; set; }
        public string ProcessingStatus { get; set; }
        public string ProductName { get; set; }
        public ProductDisplayModel Product { get; set; }

        public static string connectionString = "Server=tcp:cldv-sql-server-poe.database.windows.net,1433;Initial Catalog=cldv-poe-DB;Persist Security Info=False;User ID=JoshuaWood;Password=Everdisk58!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";

        public static List<TransactionModel> GetTransactionsByUserID(int userID)
        {
            List<TransactionModel> orders = new List<TransactionModel>();

            // SQL query to retrieve transactions by user ID
            string query = "SELECT * FROM TransactionTable WHERE UserID = @UserID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserID", userID);

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    // Creates a transaction object and add it to the list
                    TransactionModel transaction = new TransactionModel
                    {
                        TransactionID = Convert.ToInt32(reader["TransactionID"]),
                        UserID = Convert.ToInt32(reader["UserID"]),
                        ProductID = Convert.ToInt32(reader["ProductID"]),
                        ProcessingStatus = reader["ProcessingStatus"].ToString(),
                    };

                    orders.Add(transaction);
                }

                reader.Close();
            }

            return orders;
        }

        public static List<TransactionModel> GetOrdersForClientUsers(int userID)
        {
            List<TransactionModel> orders = new List<TransactionModel>();

            // SQL query to retrieve orders made by client users for products added by other users
            string query = @"
                SELECT T.TransactionID, T.UserID, T.ProductID, P.ProductName, T.ProcessingStatus
                FROM TransactionTable T
                INNER JOIN ProductTable P ON T.ProductID = P.ProductID
                WHERE P.UserID = @UserID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserID", userID);

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    // Create a transaction object and add it to the list
                    TransactionModel transaction = new TransactionModel
                    {
                        TransactionID = Convert.ToInt32(reader["TransactionID"]),
                        UserID = Convert.ToInt32(reader["UserID"]),
                        ProductID = Convert.ToInt32(reader["ProductID"]),
                        ProductName = reader["ProductName"].ToString(),
                        ProcessingStatus = reader["ProcessingStatus"].ToString(),
                    };

                    orders.Add(transaction);
                }

                reader.Close();
            }

            return orders;
        }
    }
}
