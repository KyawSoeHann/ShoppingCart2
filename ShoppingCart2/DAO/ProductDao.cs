using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Data.SqlClient;
using ShoppingCart2.Models;
using System.Diagnostics;


namespace ShoppingCart2.DAO
{
    public class ProductDao
    {
        public static List<Product> getAllProducts()
        {
            List<Product> products = new List<Product>();
            using (SqlConnection conn = new SqlConnection(Database.conName))
            {
                conn.Open();
                string query = @"SELECT * FROM Product";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    Product p = new Product()
                    {
                        id = (int)sdr["id"],
                        name = (string)sdr["name"],
                        description = (string)sdr["description"],
                        price = (int)sdr["price"],
                        imageName = (string)sdr["image_name"]
                    };

                    products.Add(p);
                }
            }
            return products;
        }

        public static List<Product> getByProductName(String name)
        {
            List<Product> products = new List<Product>();
            using (SqlConnection conn = new SqlConnection(Database.conName))
            {

                string query = @"SELECT * FROM Product WHERE name LIKE @name";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@name", name + "%");

                conn.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    Product p = new Product()
                    {
                        id = (int)sdr["id"],
                        name = (string)sdr["name"],
                        description = (string)sdr["description"],
                        price = (int)sdr["price"],
                        imageName = (string)sdr["image_name"]
                    };

                    products.Add(p);
                }
            }
            return products;
        }

        public static List<CartList> getProductsByIds(Dictionary<string,string> kV)
        {
            List<CartList> cl = new List<CartList>();
            using (SqlConnection conn = new SqlConnection(Database.conName))
            {
                var keyArrays = kV.Keys.ToArray();
                var inString = String.Join(",@", keyArrays);
                string query = @"SELECT * FROM Product WHERE id in (@" + inString + ")";
                SqlCommand cmd = new SqlCommand(query, conn);

                foreach (KeyValuePair<string,string> k in kV)
                {
                    cmd.Parameters.AddWithValue("@" + k.Key,k.Key);
                }

                conn.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    var pId = kV[System.Convert.ToString(sdr["id"])];

                    CartList p = new CartList()
                    {
                        id = (int)sdr["id"],
                        name = (string)sdr["name"],
                        description = (string)sdr["description"],
                        price = (int)sdr["price"],
                        imageName = (string)sdr["image_name"],
                        count = System.Convert.ToInt32(pId)
                    };

                    cl.Add(p);
                }
                return cl;
            }
        }
    }
}