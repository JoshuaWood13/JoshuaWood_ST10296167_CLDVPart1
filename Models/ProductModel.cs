using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace JoshuaWood_ST10296167_CLDV_POE.Models
{
    public class ProductModel
    {
        public static string con_string = "Server=tcp:cldv-sql-server-poe.database.windows.net,1433;Initial Catalog=cldv-poe-DB;Persist Security Info=False;User ID=JoshuaWood;Password=Everdisk58!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";
        public static SqlConnection con = new SqlConnection(con_string);

        public string Name { get; set; }

        public string Price { get; set; }

        public string Category { get; set; }

        public string Availability {  get; set; }

        public int UserID { get; set; }

        public int insert_product(ProductModel p, int userID)
        {
            try
            {
                string sql = "INSERT INTO ProductTable (ProductName, ProductPrice, ProductCategory, ProductAvailability, UserID) VALUES (@Name, @Price, @Category, @Availability, @UserId)";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@Name", p.Name);
                cmd.Parameters.AddWithValue("@Price", p.Price);
                cmd.Parameters.AddWithValue("@Category", p.Category);
                cmd.Parameters.AddWithValue("@Availability", p.Availability);
                cmd.Parameters.AddWithValue("@UserId", userID);
                con.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                con.Close();
                return rowsAffected;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
