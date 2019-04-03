using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Diagnostics;
using ShoppingCart2.Models;

namespace ShoppingCart2.DAO
{
    public static class PurchaseDao
    {
        public static int InsertPurchase(int userId,Dictionary<string,string> kV)
        {

            using (SqlConnection conn = new SqlConnection(Database.conName))
            {
                conn.Open();
                string query = @"INSERT INTO PurchaseItem (user_id,product_id) Values (@userId,@productId)";
                SqlCommand cmd = new SqlCommand(query, conn);

                string[] keys = kV.Keys.ToArray();
                foreach (var key in keys)
                {
                    var limit = System.Convert.ToInt32(kV[key]);
                    for (var i = 0; i < limit; i++)
                    {
                        Debug.WriteLine("Product id " + key);
                        cmd.Parameters.AddWithValue("@userId", userId);
                        cmd.Parameters.AddWithValue("@productId", key);
                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                    }
                }
            }
            return 0;
        }

        public static List<PurchaseItem> getPurchaseItems(int userId)
        {
            List<PurchaseItem> piList = new List<PurchaseItem>();
            using (SqlConnection conn = new SqlConnection(Database.conName))
            {
                string query = @"SELECT p.name,p.price,p.image_name,p.description,pi.id,pi.purchase_time from PurchaseItem pi inner join Product p ON 
                                pi.product_id = p.id WHERE pi.user_id = @userId Order By p.name,pi.purchase_time";
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@userId", userId);
                SqlDataReader sdr = cmd.ExecuteReader();
                int count = 0;
                while (sdr.Read())
                {
                    if (count == 0)
                    {
                        PurchaseItem pi = new PurchaseItem()
                        {
                            productName = (string)sdr["image_name"],
                            description = (string)sdr["description"],
                            imageName = (string)sdr["image_name"],
                            price = (int)sdr["price"]

                        };
                        List<string> activeCodes = new List<string>();
                        activeCodes.Add(sdr.GetGuid(4).ToString());
                        pi.activationCode = activeCodes;
                        pi.purchaseTime = Convert.ToDateTime(sdr["purchase_time"]);
                        piList.Add(pi);

                    } else
                    {
                        PurchaseItem pi = new PurchaseItem()
                        {
                            productName = (string)sdr["image_name"],
                            description = (string)sdr["description"],
                            imageName = (string)sdr["image_name"],
                            price = (int)sdr["price"]

                        };
                        List<string> activeCodes = new List<string>();
                        activeCodes.Add(sdr.GetGuid(4).ToString());
                        pi.activationCode = activeCodes;
                        pi.purchaseTime = Convert.ToDateTime(sdr["purchase_time"]);

                        PurchaseItem previousItem = piList[piList.Count - 1];
                        

                        if (previousItem.productName == pi.productName &&  previousItem.purchaseTime.Date == pi.purchaseTime.Date)
                        {
                            previousItem.activationCode.Add(sdr.GetGuid(4).ToString());
                        }else
                        {
                            piList.Add(pi);
                        }

                    }
                    count++;
                }

            }


            return piList;
        }
    }
}