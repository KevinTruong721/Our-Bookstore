using System.Windows;
using System.Data;
using BookStoreLIB;
using Comp4220;
using System;

namespace BookStoreGUI
{
    /// Interaction logic for MainWindow.xaml
    public partial class MainWindow : Window {
        DataSet dsBookCat;
        UserData userData;
        BookOrder bookOrder;

        private void LoginButton_Click(object sender, RoutedEventArgs e) {
            LoginDialog dlg = new LoginDialog();
            dlg.Owner = this;
            _ = dlg.ShowDialog();
            // Process data entered by user if dialog box is accepted
            if (dlg.DialogResult == true)
            {
                userData = dlg.userData;
                statusTextBlock.Text = "You are logged in as User #" + dlg.userData.UserID;

                loginButton.Content = "Account";
                loginButton.Name = "Account";
                loginButton.Click -= LoginButton_Click;
                loginButton.Click += AccountButton_Click;
            }
            else
            {
                if (dlg.userData.GetError > 0)
                    _ = MessageBox.Show(dlg.userData.GetErrorDescription());
            }
        }

        private void AccountButton_Click(object sender, RoutedEventArgs e)
        {
            UserInformationDialog dlg = new UserInformationDialog(userData.UserID);
            dlg.Owner = this;
            _ = dlg.ShowDialog();

            if(dlg.DialogResult == true)
            {
                this.userData = new UserData();
                loginButton.Content = "Login";
                loginButton.Name = "Login";
                loginButton.Click -= AccountButton_Click;
                loginButton.Click += LoginButton_Click;
                statusTextBlock.Text = "Please login before proceeding to checkout.";
            }
        }



        private void ExitButton_Click(object sender, RoutedEventArgs e) { Close(); }
        public MainWindow() { 
            InitializeComponent();
            LoadTheme(Comp4220.Properties.Settings.Default.IsDarkMode);
        }
        private void Window_Loaded(object sender, RoutedEventArgs e) 
        {
            BookCatalog bookCat = new BookCatalog();
            dsBookCat = bookCat.GetBookInfo();
            bookOrder = new BookOrder();
            this.DataContext = dsBookCat.Tables["Category"];
            userData = new UserData();
            this.orderListView.ItemsSource = bookOrder.OrderItemList;
        }
        private void CheckoutButton_Click(object sender, RoutedEventArgs e) {
            if (userData.UserID < 1 || bookOrder.orderItemList.Count == 0)
            {
                MessageBox.Show("Please sign in and select book(s) before placing the order.");
            }
            else
            {
                int orderId;
                CheckoutDialog dlg = new CheckoutDialog(userData.UserID, bookOrder.OrderItemList);
                _ = dlg.ShowDialog();
                if (dlg.DialogResult == true)
                {
                    this.orderListView.ItemsSource = null;
                    orderId = bookOrder.PlaceOrder(userData.UserID);
                    MessageBox.Show("Your order has been placed. Your order id is " + orderId.ToString());
                }                
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            // Instalize the OrderItemDialog for the user to input quantity.
            OrderItemDialog orderItemDialog = new OrderItemDialog();
            DataRowView selectedRow;
            // Attempt to get the selected row from the ProductsDataGrid.
            try
            {
                selectedRow = (DataRowView)this.ProductsDataGrid.SelectedItems[0];
            }
            catch
            {
                // If no book is selected, inform the user and exit the method.
                MessageBox.Show("Please select a book to add to the order");
                return;
            }
            // Populate the OrderItemDialog text boxes with data from the selected row.
            orderItemDialog.isbnTextBox.Text = selectedRow.Row.ItemArray[0].ToString();
            orderItemDialog.titleTextBox.Text = selectedRow.Row.ItemArray[2].ToString();
            orderItemDialog.priceTextBox.Text = selectedRow.Row.ItemArray[4].ToString();
            orderItemDialog.Owner = this;
            orderItemDialog.ShowDialog();
            // Check if the dialog was closed with the OK button.
            if (orderItemDialog.DialogResult == true)
            {
                // Extract book details from the dialog.
                string isbn = orderItemDialog.isbnTextBox.Text;
                string title = orderItemDialog.titleTextBox.Text;
                double unitPrice = double.Parse(orderItemDialog.priceTextBox.Text);
                int quantity = int.Parse(orderItemDialog.quantityTextBox.Text);
                // Add the selected book with the specified quantity to the order.
                bookOrder.AddItem(new OrderItem(isbn, title, unitPrice, quantity));
            }
        }

        
        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            // Check if an item is selected in the order.
            if (this.orderListView.SelectedItem != null)
            {
                var selectedOrderItem = this.orderListView.SelectedItem as OrderItem;
                bookOrder.RemoveItem(selectedOrderItem.BookID);
            }
            // If no item is selected, inform the user.
            else
            {
                MessageBox.Show("Please select a book to remove from the order");
                return;
            }
        }
        private void LoadTheme(bool isDarkMode)
        {
            var themeUri = isDarkMode ? "DarkThemeResources.xaml" : "LightThemeResources.xaml";
            var themeDict = new ResourceDictionary { Source = new Uri(themeUri, UriKind.Relative) };

            // Clear previous dictionaries and add new
            Application.Current.Resources.MergedDictionaries.Clear();
            Application.Current.Resources.MergedDictionaries.Add(themeDict);
        }

        private void DarkMode_Click(object sender, RoutedEventArgs e)
        {
            // Toggle theme
            bool isDarkMode = !Comp4220.Properties.Settings.Default.IsDarkMode;
            LoadTheme(isDarkMode);
            Comp4220.Properties.Settings.Default.IsDarkMode = isDarkMode;
            Comp4220.Properties.Settings.Default.Save();

            ProductsDataGrid.Items.Refresh();
            foreach (var column in ProductsDataGrid.Columns)
            {
                column.HeaderStyle = (Style)Application.Current.FindResource("DataGridColumnHeaderStyle");
            }
        }
    }
}
