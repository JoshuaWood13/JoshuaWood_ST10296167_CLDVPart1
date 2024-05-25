using System.Data.SqlClient;
using System.Transactions;

namespace JoshuaWood_ST10296167_CLDV_POE.Models
{
    public class ClientModel
    {
        public int TransactionID { get; set; }
        public int UserID { get; set; }
        public int ProductID { get; set; }

        public static List<ClientModel> GetTransactionsByUserID(int userID)
        {
            List<ClientModel> orders = new List<ClientModel>();

            string connectionString = "Server=tcp:cldv-sql-server-poe.database.windows.net,1433;Initial Catalog=cldv-poe-DB;Persist Security Info=False;User ID=JoshuaWood;Password=Everdisk58!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";

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
                    ClientModel transaction = new ClientModel
                    {
                        TransactionID = Convert.ToInt32(reader["TransactionID"]),
                        UserID = Convert.ToInt32(reader["UserID"]),
                        ProductID = Convert.ToInt32(reader["ProductID"]),
                    };

                    orders.Add(transaction);
                }

                reader.Close();
            }

            return orders;
        }
    }
}
