using System.Data.SqlClient;

namespace JoshuaWood_ST10296167_CLDV_POE.Models
{
    public class LoginModel
    {
        public static string con_string = "Server=tcp:cldv-sql-server-poe.database.windows.net,1433;Initial Catalog=cldv-poe-DB;Persist Security Info=False;User ID=JoshuaWood;Password=Everdisk58!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";

        //This method checks for a matching email and password stored in the database from user input and returns the user ID
        public int selectUser(string email, string password)
        {
            int userID = -1;
            using (SqlConnection con = new SqlConnection(con_string))
            {
                string sql = "SELECT UserID FROM UserTable WHERE UserEmail = @Email AND UserPassword = @Password";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Password", password);
                try
                {
                    con.Open();
                    object result = cmd.ExecuteScalar();
                    if(result != null && result != DBNull.Value)
                    {
                        userID = Convert.ToInt32(result);
                    }
                }
                catch(Exception ex)
                {
                    throw ex;
                }
            }
            return userID;
        }
    }
}
