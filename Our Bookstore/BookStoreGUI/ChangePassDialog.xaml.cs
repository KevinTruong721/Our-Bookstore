using BookStoreLIB;
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
 
    public partial class ChangePassDialog : Window
    {
        private int UserId;
        public UserData UserDataInstance;


        public ChangePassDialog(int userID)
        {
            InitializeComponent();
            UserDataInstance = new UserData();
            UserId = userID;
        }


        private void Button_OK_Click(object sender, RoutedEventArgs e)
        {
            string currentPassword = CurrentPasswordBox.Password;
            string newPassword = NewPasswordBox.Password;


            if (UserDataInstance.ChangePassword(UserId, currentPassword, newPassword))
            {
                // Password change was successful
                MessageBox.Show("Password changed successfully.");
                DialogResult = true;
                Close();
            }
            else
            {
                // Password change failed
                MessageBox.Show("Failed to change the password.");
            }


        }

        private void Button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

    }
}
