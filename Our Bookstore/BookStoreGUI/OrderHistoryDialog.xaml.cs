using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BookStoreLIB;

namespace Comp4220
{
    public partial class OrderHistoryDialog : Window
    {
        int UserId;
        public ObservableCollection<OrderHistory> OrderHistories { get; set; }

        public OrderHistoryDialog(int userId)
        {
            InitializeComponent();
            UserId = userId;
            OrderHistories = new ObservableCollection<OrderHistory>();
            GetOrderHistory();
            orderListView.ItemsSource = OrderHistories;
        }
        public void GetOrderHistory()
        {
            DALOrderHistory dalOrderHistory = new DALOrderHistory();
            DataSet dsOrderHistory = dalOrderHistory.GetOrderHistory(UserId);

            if (dsOrderHistory != null && dsOrderHistory.Tables.Count > 0)
            {
                foreach (DataRow row in dsOrderHistory.Tables["OrderHistory"].AsEnumerable())
                {
                    OrderHistory order = new OrderHistory(
                    
                        row.Field<string>("BookID"),
                        row.Field<string>("BookTitle"),
                        (double)row.Field<decimal>("SubTotal"),
                        row.Field<DateTime>("Date")
                    );
                    OrderHistories.Add(order);
                }
            }
            return;
        }

    }
}
