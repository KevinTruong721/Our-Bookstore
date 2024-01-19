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

namespace BookStoreGUI
    {
        //Summary:
        //  Interaction logic for OrderItemDialog.xaml
        public partial class OrderItemDialog : Window
        {

            //Summary:
            //  Initializes a new instance of the OrderItemDialog class.
            public OrderItemDialog()
            {
                InitializeComponent();
            }

            //
            //Summary:
            //  Validates the entered quantity and closes the dialog.
            //Parameters:
            //  sender:The source of the event.
            //  e:The event arguments.
            private void okButton_Click(object sender, RoutedEventArgs e)
            {
                int enteredQuantity;

                if (!int.TryParse(quantityTextBox.Text, out enteredQuantity))
                {
                    MessageBox.Show("Please enter a valid quantity");
                    return;
                }

                this.DialogResult = true;
            }

            //
            //Summary:
            //  Closes the dialog without setting a quantity.
            //Parameters:
            //  sender:The source of the event.
            //  e:The event arguments.
            private void cancelButton_Click(object sender, RoutedEventArgs e)
            {
                this.DialogResult = false;
            }
        }
    }

