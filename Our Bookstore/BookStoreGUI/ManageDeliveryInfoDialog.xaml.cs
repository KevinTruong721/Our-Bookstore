using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using BookStoreLIB;

namespace BookStoreGUI
{
    public partial class ManageDeliveryInfoDialog : Window
    {
        int UserId;
        public ObservableCollection<DeliveryInfo> DeliveryInfos { get; set; }

        public ManageDeliveryInfoDialog(int userId)
        {
            InitializeComponent();
            UserId = userId;
            DeliveryInfos = new ObservableCollection<DeliveryInfo>();
            Get_All_Addresses();
            ItemsControlAddresses.ItemsSource = DeliveryInfos;
        }

        public void Get_All_Addresses()
        {
            DALDeliveryInfo dalSA = new DALDeliveryInfo();
            DataSet ds = dalSA.FindDeliveryInfo(UserId);
            foreach (DataRow row in ds.Tables[0].AsEnumerable())
            {
                DeliveryInfo a = new DeliveryInfo
                {
                    Address = row.Field<string>("Address"),
                    City = row.Field<string>("City"),
                    Province = row.Field<string>("Province"),
                    PostalCode = row.Field<string>("PostalCode"),
                    Country = row.Field<string>("Country"),
                    Phone = row.Field<string>("Phone"),
                };
                DeliveryInfos.Add(a);
            }
        }

        private void Button_EditAddress_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var addressInfo = button?.Tag as DeliveryInfo;
            if (addressInfo != null)
            {
                AddAddressDialog dlg = new AddAddressDialog();
                dlg.Owner = this;
                dlg.addressTextBox.Text = addressInfo.Address;
                dlg.cityTextBox.Text = addressInfo.City;
                dlg.provinceTextBox.Text = addressInfo.Province;
                dlg.postalCodeTextBox.Text = addressInfo.PostalCode;
                dlg.countryTextBox.Text = addressInfo.Country;
                dlg.phoneTextBox.Text = addressInfo.Phone;
                if(dlg.ShowDialog() == true) 
                { 
                    var dal = new DALDeliveryInfo();
                    dal.EditDeliveryInfo(
                        UserId,
                        (string)addressInfo.Address,
                        dlg.deliveryinfo.Address,
                        dlg.deliveryinfo.City,
                        dlg.deliveryinfo.Province,
                        dlg.deliveryinfo.PostalCode,
                        dlg.deliveryinfo.Country,
                        dlg.deliveryinfo.Phone
                    );
                    DeliveryInfos[DeliveryInfos.IndexOf(addressInfo)] = dlg.deliveryinfo;
                }
                else
                {
                    _ = MessageBox.Show(dlg.deliveryinfo.GetErrorDescription());
                }
            }
        }

        private void Button_DeleteAddress_Click(object sender, RoutedEventArgs e)
        {
            var addressInfo = ((Button)sender)?.Tag as DeliveryInfo;
            if (addressInfo != null)
            {
                var dalSA = new DALDeliveryInfo();
                int res = dalSA.RemoveDeliveryInfo(
                    UserId, 
                    addressInfo.Address, 
                    addressInfo.City, 
                    addressInfo.Province, 
                    addressInfo.PostalCode, 
                    addressInfo.Country, 
                    addressInfo.Phone 
                );
                if (res != 1)
                    _ = MessageBox.Show("Failed to delete the address, please contact support");
                else
                    DeliveryInfos.Remove(addressInfo);
            }
        }

        private void Button_AddAddress_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new AddAddressDialog();
            dlg.Owner = this;
            if (dlg.ShowDialog() == true)
            {
                var dal = new DALDeliveryInfo();
                dal.InsertDeliveryInfo(
                    UserId,
                    dlg.deliveryinfo.Address,
                    dlg.deliveryinfo.City,
                    dlg.deliveryinfo.Province,
                    dlg.deliveryinfo.PostalCode,
                    dlg.deliveryinfo.Country,
                    dlg.deliveryinfo.Phone
                );
                DeliveryInfos.Add(dlg.deliveryinfo);
            }
            else
            {
                if (dlg.deliveryinfo.GetError > 0)
                {
                    _ = MessageBox.Show(dlg.deliveryinfo.GetErrorDescription());
                }
            }

        }
    }
}
