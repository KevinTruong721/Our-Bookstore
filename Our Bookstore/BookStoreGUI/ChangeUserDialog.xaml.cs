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
    
    public partial class ChangeUserDialog : Window
    {
        private int UserId;
        private string CurrentUsername;
        public UserData UserDataInstance;



        public ChangeUserDialog(int UserID)
        {
            InitializeComponent();
            UserId = UserID;
            UserDataInstance = new UserData();
            CurrentUsername = UserDataInstance.GetCurrentUsername(UserID);
            SetCurrentUsername(CurrentUsername);
        }

        private void Button_OK_Click(object sender, RoutedEventArgs e)
        {

            string newUsername = NewUserTextBox.Text;

            if (string.IsNullOrWhiteSpace(newUsername))
            {
                MessageBox.Show("Please enter a valid new username.");
                return;
            }

            // Attempt to change the username
            bool success = UserDataInstance.ChangeUsername(UserId, newUsername);

            if (success)
            {
                MessageBox.Show("Username updated successfully.");
                DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show("Failed to update username. Please try again.");
            }
        }

        private void Button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }




        public void SetCurrentUsername(string username)
        {
            CurrentUserTextBox.Text = username;
        }


    }
}
