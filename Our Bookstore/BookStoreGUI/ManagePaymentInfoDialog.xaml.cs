using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using BookStoreLIB;

namespace BookStoreGUI
{ 
	public partial class ManagePaymentInfoDialog : Window
    {
        int UserId;
        public ObservableCollection<CardInfo> CardInfos { get; set; }
        public ManagePaymentInfoDialog(int userId)
        {
            InitializeComponent();
            UserId = userId;
            CardInfos = new ObservableCollection<CardInfo>();
            Get_All_Cards();
            ItemsControlCreditCards.ItemsSource = CardInfos;
        }

        public void Get_All_Cards()
        {
            DALPaymentInfo dalPI = new DALPaymentInfo();
            DataSet ds = dalPI.FindPaymentInfo(UserId);
            foreach (DataRow row in ds.Tables["PaymentInfo"].AsEnumerable())
            {
                CardInfo c = new CardInfo(
                    (long)row.Field<decimal>("CardNumber"),
                    row.Field<DateTime>("Expiration"),
                    (int)row.Field<decimal>("CVV")
                );
                CardInfos.Add(c);
            }
            return;
        }

        private CardInfo Find_Card_With_Number(long cardNumber)
        {
            foreach (CardInfo c in CardInfos)
            {
                if (c.CreditCardNumber == cardNumber) return c;
            }
            throw new KeyNotFoundException("No card with that number exists");
        }

        private void Button_EditCard_Click(object sender, RoutedEventArgs e)
        {
            long cardNumber = (long)((Button)sender).Tag;
            var card = Find_Card_With_Number(cardNumber);
            // Code to edit the selected credit card
            AddCardDialog dlg = new AddCardDialog();
            dlg.Owner = this;
            dlg.creditCardNumberTextBox.Text = card.CreditCardNumber.ToString();
            dlg.cvvTextBox.Text = card.CVV.ToString();
            dlg.expirationDateTextBox.Text = (card.ExpirationDate.Month+100).ToString().Substring(1) + "/" + (card.ExpirationDate.Year % 100).ToString();
            if (dlg.ShowDialog() == true)
            {
                var dal = new DALPaymentInfo();
                dal.EditPaymentInfo(
                    UserId,
                    cardNumber,
                    dlg.cardInfo.CreditCardNumber,
                    dlg.cardInfo.ExpirationDate,
                    dlg.cardInfo.CVV
                );
                CardInfos[CardInfos.IndexOf(card)] = dlg.cardInfo;
            }
            else
            {
                if (dlg.cardInfo.GetError > 0)
                {
                    _ = MessageBox.Show(dlg.cardInfo.GetErrorDescription());
                }
            }
        }

        private void Button_DeleteCard_Click(object sender, RoutedEventArgs e)
        {
            long cardNumber = (long)((Button)sender).Tag;
            var dalPI = new DALPaymentInfo();
            int res = dalPI.RemovePaymentInfo(UserId, cardNumber);
            if (res != 1)
            {
                _ = MessageBox.Show("Failed to delete the card, please contact support");
            } else
            {
                CardInfos.Remove(Find_Card_With_Number(cardNumber));
            }
        }

        private void Button_AddCard_Click(object sender, RoutedEventArgs e)
        {
            AddCardDialog dlg = new AddCardDialog();
            dlg.Owner = this;
            _ = dlg.ShowDialog();
            if (dlg.DialogResult == true)
            {
                var dal = new DALPaymentInfo();
                dal.InsertPaymentInfo(
                    UserId,
                    dlg.cardInfo.CreditCardNumber,
                    dlg.cardInfo.ExpirationDate,
                    dlg.cardInfo.CVV
                );
                CardInfos.Add(dlg.cardInfo);
            }
            else
            {
                if (dlg.cardInfo.GetError > 0)
                {
                    _ = MessageBox.Show(dlg.cardInfo.GetErrorDescription());
                }
            }
        }
    }
}