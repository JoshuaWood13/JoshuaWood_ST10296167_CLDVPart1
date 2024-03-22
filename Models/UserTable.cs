using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace JoshuaWood_ST10296167_CLDV_POE.Models
{
    public class UserTable /*: Controller*/
    {

        public static string con_string = "Server=tcp:cldv-sql-server-poe.database.windows.net,1433;Initial Catalog=cldv-poe-DB;Persist Security Info=False;User ID=JoshuaWood;Password=Everdisk58!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";
        public static SqlConnection con = new SqlConnection(con_string);

        public string Name { get; set; }
        //public int ID get set
        public string Surname { get; set; }

        public string Email { get; set; }

        public int insert_User(UserTable m)
        {
            string sql = "INSERT INTO userTable (userName, userSurname, userEmail) VALUES (@Name, @Surname, @Email)";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@Name", m.Name);
            cmd.Parameters.AddWithValue("@Surname", m.Surname);
            cmd.Parameters.AddWithValue("@Email", m.Email);
            con.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            con.Close();
            return rowsAffected;
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}
