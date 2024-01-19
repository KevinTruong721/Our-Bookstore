using System.Linq;
using System.Windows;
using BookStoreLIB;
using BookStoreGUI;
using System.Collections.ObjectModel;

namespace Comp4220 {
    public partial class CheckoutDialog : Window {
        int UserId;
        public ObservableCollection<OrderItem> OrderList { get; set; }
        public double Total {
            get { return OrderList.Sum(item => item.SubTotal); }
        }
        public bool IsDeliveryChecked {
            get { return _isDeliveryChecked; }
            set{
                _isDeliveryChecked = value;
                Delivery_Info_Button.Visibility = Visibility.Visible;
            }
        }
        private bool _isDeliveryChecked;
        public bool IsPickupChecked{
            get { return _isPickupChecked; }
            set{
                _isPickupChecked = value;
                Delivery_Info_Button.Visibility = Visibility.Hidden;
            }
        }
        private bool _isPickupChecked;
        public CheckoutDialog(int userId, ObservableCollection<OrderItem> orderItemList) {
            InitializeComponent();
            UserId = userId;
            OrderList = orderItemList ?? new ObservableCollection<OrderItem>();
            DataContext = this; 
        }
        public CheckoutDialog(int userId){
            InitializeComponent();
            UserId = userId;
        }
        private void Cancel_Button_Click(object sender, RoutedEventArgs e){
            DialogResult = false;
            Close();
        }
        private void Confirm_Button_Click(object sender, RoutedEventArgs e){
            DialogResult = true;
            Close();
        }
        private void Delivery_Info_Button_Click(object sender, RoutedEventArgs e){
            ManageDeliveryInfoDialog dlg = new ManageDeliveryInfoDialog(UserId);
            _ = dlg.ShowDialog();
        }
        private void Pay_Info_Button_Click(object sender, RoutedEventArgs e){
            ManagePaymentInfoDialog dlg = new ManagePaymentInfoDialog(UserId);
            _ = dlg.ShowDialog();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e){
            this.orderListView.ItemsSource = OrderList;
        }
    }
}
