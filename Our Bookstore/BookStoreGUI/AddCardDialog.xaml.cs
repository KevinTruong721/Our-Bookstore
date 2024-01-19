using System.Windows;
using BookStoreLIB;

namespace BookStoreGUI
{
    public partial class AddCardDialog: Window
    {
        public CardInfo cardInfo;
        public AddCardDialog()
        {
            InitializeComponent();
            cardInfo = new CardInfo();
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            string cvv = cvvTextBox.Text;
            string creditCardNumber = creditCardNumberTextBox.Text;
            string expirationDate = expirationDateTextBox.Text;
            DialogResult = cardInfo.Parse(cvv, creditCardNumber, expirationDate);
            Close();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}