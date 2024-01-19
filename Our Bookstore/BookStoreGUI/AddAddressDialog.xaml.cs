using System.Windows;
using BookStoreLIB;

namespace BookStoreGUI
{
    public partial class AddAddressDialog: Window
    {
        public DeliveryInfo deliveryinfo;
        public AddAddressDialog()
        {
            InitializeComponent();
            deliveryinfo = new DeliveryInfo();
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            string address = addressTextBox.Text;
            string city = cityTextBox.Text;
            string province = provinceTextBox.Text;
            string postalCode = postalCodeTextBox.Text;
            string country = countryTextBox.Text;
            string phone = phoneTextBox.Text;
            DialogResult = deliveryinfo.Parse(address, city, province, postalCode, country, phone);
            Close();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}