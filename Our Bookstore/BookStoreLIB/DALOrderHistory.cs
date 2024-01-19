using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreLIB
{
    public class DALOrderHistory
    {
        SqlConnection conn;
        DataSet dsOrderHistory;
        public DALOrderHistory()
        {
            conn = new SqlConnection(Properties.Settings.Default.LocalDBCon);
            conn.Open();
        }
        public DataSet GetOrderHistory(int UserID)
        {
            try
            {
                string strSQL = "SELECT OrderItem.ISBN AS BookID, BookData.Title AS BookTitle, (OrderItem.Quantity * BookData.Price) AS SubTotal, Orders.OrderDate AS Date " +
                    "FROM OrderItem " +
                    "INNER JOIN Orders ON OrderItem.OrderID = Orders.OrderID " +
                    "INNER JOIN BookData ON OrderItem.ISBN = BookData.ISBN " +
                    "WHERE Orders.UserID = @UserID;";
                SqlCommand cmdGetOrderHistory = new SqlCommand(strSQL, conn);
                cmdGetOrderHistory.Parameters.AddWithValue("@UserID", UserID);
                dsOrderHistory = new DataSet("OrderHistory");
                SqlDataAdapter daOrderHistory = new SqlDataAdapter(cmdGetOrderHistory);
                daOrderHistory.Fill(dsOrderHistory, "OrderHistory");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return dsOrderHistory;
        }
    }

}
