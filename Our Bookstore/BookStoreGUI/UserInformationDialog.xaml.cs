using BookStoreGUI;
using System;
using System.Collections.Generic;
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

namespace Comp4220
{
    /// <summary>
    /// Interaction logic for UserInformationDialog.xaml
    /// </summary>
    public partial class UserInformationDialog : Window
    {
        int userID;
        public UserInformationDialog(int userID)
        {
            InitializeComponent();
            this.userID = userID;
        }

        private void Button_Payment_Information_Click(object sender, RoutedEventArgs e)
        {
            ManagePaymentInfoDialog dlg = new ManagePaymentInfoDialog(this.userID);
            dlg.Owner = this;
            _ = dlg.ShowDialog();
        }

        private void Button_Delivery_Information_Click(object sender, RoutedEventArgs e)
        {
            ManageDeliveryInfoDialog dlg = new ManageDeliveryInfoDialog(this.userID);
            dlg.Owner = this;
            _ = dlg.ShowDialog();
        }

        private void Button_Order_History_Click(object sender, RoutedEventArgs e)
        {
            OrderHistoryDialog dlg = new OrderHistoryDialog(this.userID);
            dlg.Owner = this;
            _ = dlg.ShowDialog();
        }
        private void Button_Change_User_Click(object sender, RoutedEventArgs e)
        {
            ChangeUserDialog dlg = new ChangeUserDialog(this.userID);
            dlg.Owner = this;
            _ = dlg.ShowDialog();
        }
        private void Button_Change_Pass_Click(object sender, RoutedEventArgs e)
        {
            ChangePassDialog dlg = new ChangePassDialog(this.userID);
            dlg.Owner = this;
            _ = dlg.ShowDialog();
        }

        private void Button_Logout_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
