using System;
using System.ComponentModel;
using System.Globalization;

namespace BookStoreLIB
{
    public enum CardInfo_Error
    {
        [Description("Credit Card Number is invalid. Ensure it is 16 numeric characters.")]
        CardNumberError = 1,
        [Description("The CVV is invalid. Ensure it is 3 numeric characters.")]
        InvalidCVV,
        [Description("The expiration date is invalid. Ensure it is in MM/yy format and not in the past.")]
        InvalidExpirationDate,
        [Description("Please fill out all fields.")]
        InputsEmpty,
    }

    public class CardInfo : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged Members

        //
        //Summary
        //  Occurs when a property value changes.
        public event PropertyChangedEventHandler PropertyChanged;

        //
        //Summary
        //  Raises the PropertyChanged event.
        //Parameters:
        //  propName:Name of the property that changed.
        protected void Notify(string propName)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
        #endregion

        private long _CreditCardNumber;
        public long CreditCardNumber
        {
            get { return _CreditCardNumber; }
            set
            {
                _CreditCardNumber = value;
                Notify(nameof(_CreditCardNumber));
            }
        }
        public int CVV { get; set; }
        public DateTime ExpirationDate { get; set; }
        public CardInfo_Error GetError { set; get; }
        public string GetErrorDescription()
        {
            DescriptionAttribute[] attr = (DescriptionAttribute[])GetError
                .GetType().GetField(GetError.ToString())?
                .GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attr[0]?.Description ?? "";
        }
        public CardInfo() { }
        public CardInfo(long cardNumber, DateTime exp, int CVV)
        {
            CreditCardNumber = cardNumber;
            ExpirationDate = exp;
            this.CVV = CVV;
        }

        public bool Parse(string cvv, string creditCardNumber, string expirationDate)
        {
            if (cvv == "" || creditCardNumber == "" || expirationDate == "")
            {
                GetError = CardInfo_Error.InputsEmpty;
                return false;
            }

            int outCVV;
            if (!((cvv.Length == 3) && int.TryParse(cvv, out outCVV)))
            {
                GetError = CardInfo_Error.InvalidCVV;
                return false;
            }

            long outCreditCardNumber;
            if (!((creditCardNumber.Length == 16) && long.TryParse(creditCardNumber, out outCreditCardNumber)))
            {
                GetError = CardInfo_Error.CardNumberError;
                return false;
            }

            if (DateTime.TryParseExact(expirationDate, "MM/yy", CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateTime expDate)) {
                if (expDate < DateTime.Now)
                {
                    GetError = CardInfo_Error.InvalidExpirationDate;
                    return false;
                }
            } else
            {
                GetError = CardInfo_Error.InvalidExpirationDate;
                return false;
            }

            CreditCardNumber = outCreditCardNumber;
            CVV = outCVV;
            ExpirationDate = expDate;
            GetError = 0;
            return true;
        }
    }
}
