using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace BookStoreLIB
{
    public class OrderHistory
    {

        public string BookID { get; set; }
        public string BookTitle { get; set; }
        public double SubTotal { get; set; }
        public DateTime Date { get; set; }

        public OrderHistory()
        {
            BookID = "";
            BookTitle = "";
        }

        public OrderHistory(string bookID, string bookTitle, double subTotal, DateTime date)
        {
            BookID = bookID;
            BookTitle = bookTitle;
            SubTotal = subTotal;
            Date = date;
        }

        public static DataTable GetOrderHistory(int userID)
        {
            DALOrderHistory dalOrderHistory = new DALOrderHistory();
            DataSet dsOrderHistory = dalOrderHistory.GetOrderHistory(userID);

            if (dsOrderHistory != null && dsOrderHistory.Tables.Count > 0)
            {
                return dsOrderHistory.Tables["Orders"];
            }
            return new DataTable();
        }
    }


}
