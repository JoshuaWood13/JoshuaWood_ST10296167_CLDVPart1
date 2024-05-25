using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace JoshuaWood_ST10296167_CLDV_POE.Models
{
    public class UserModel
    {

        public static string con_string = "Server=tcp:cldv-sql-server-poe.database.windows.net,1433;Initial Catalog=cldv-poe-DB;Persist Security Info=False;User ID=JoshuaWood;Password=Everdisk58!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";
        public static SqlConnection con = new SqlConnection(con_string);

        public string Name { get; set; }
        
        public string Surname { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public int insert_User(UserModel u)
        {
            try
            {
                if (string.IsNullOrEmpty(u.Name) || string.IsNullOrEmpty(u.Surname) || string.IsNullOrEmpty(u.Email) || string.IsNullOrEmpty(u.Password))
                {
                    throw new ArgumentException("User information cannot be null or empty.");
                }

                string sql = "INSERT INTO userTable (UserName, UserSurname, UserEmail, UserPassword) VALUES (@Name, @Surname, @Email, @Password)";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@Name", u.Name);
                cmd.Parameters.AddWithValue("@Surname", u.Surname);
                cmd.Parameters.AddWithValue("@Email", u.Email);
                cmd.Parameters.AddWithValue("@Password", u.Password);
                con.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                con.Close();
                return rowsAffected;
            }
            catch (Exception)
            {
                throw;
            }
        }


        //public int insert_User(UserTable u)
        //{
        //    try
        //    {
        //        string sql = "INSERT INTO userTable (UserName, UserSurname, UserEmail, UserPassword) VALUES (@Name, @Surname, @Email, @Password)";
        //        SqlCommand cmd = new SqlCommand(sql, con);
        //        cmd.Parameters.AddWithValue("@Name", u.Name);
        //        cmd.Parameters.AddWithValue("@Surname", u.Surname);
        //        cmd.Parameters.AddWithValue("@Email", u.Email);
        //        cmd.Parameters.AddWithValue("@Password", u.Password);
        //        con.Open();
        //        int rowsAffected = cmd.ExecuteNonQuery();
        //        con.Close();
        //        return rowsAffected;
        //    }
        //    catch(Exception ex)
        //    {
        //        throw ex;
        //    }
        }
    }

