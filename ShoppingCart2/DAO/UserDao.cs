using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShoppingCart2.Models;
using System.Data.SqlClient;

namespace ShoppingCart2.DAO
{
    public static class UserDao
    {
        public static Login findLoginUser(Login login)
        {
            Login user = new Login();
            using (SqlConnection conn = new SqlConnection(Database.conName))
            {
                conn.Open();
                string query = @"SELECT TOP 1 id,name FROM Users WHERE username = @username and password = @password";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", login.username);
                cmd.Parameters.AddWithValue("@password", login.password);
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    user.id = (int)sdr["id"];
                    user.name = (string)sdr["name"];
                }
            }

            return user;
        }

        public static Login findUserById(int userId)
        {
            Login user = new Login();
            using (SqlConnection conn = new SqlConnection(Database.conName))
            {
                conn.Open();
                string query = @"SELECT TOP 1 id,name FROM Users WHERE id = @userId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@userId", userId);
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    user.id = (int)sdr["id"];
                    user.name = (string)sdr["name"];
                }
            }

            return user;
        }
    }
}