using System;
using System.ComponentModel;
using System.Globalization;
using System.Text.RegularExpressions;

namespace BookStoreLIB
{
    public enum AddressInfoError
    {
        [Description("Valid")]
        Valid,
        [Description("The address field cannot be empty.")]
        AddressEmpty,
        [Description("The city field cannot be empty.")]
        CityEmpty,
        [Description("The province field cannot be empty.")]
        ProvinceEmpty,
        [Description("The postal code field cannot be empty.")]
        PostalCodeEmpty,
        [Description("The country field cannot be empty.")]
        CountryEmpty,
        [Description("The phone field cannot be empty.")]
        PhoneEmpty,
        [Description("Please fill out all fields.")]
        InputsEmpty,
        [Description("Only Canadian addresses are accepted.")]
        InvalidCountry,
        [Description("The postal code is invalid. (Format: X#X #X#)")]
        InvalidPostalCode,
        [Description("The phone number is invalid. (Format: ###-###-####)")]
        InvalidPhone
    }

    public class DeliveryInfo : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        public void Notify(string propName)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        #endregion

        private string _Address;
        public string Address
        {
            get { return _Address; }
            set
            {
                _Address = value;
                Notify(nameof(Address));
            }
        }

        public string City { get; set; }
        public string Province { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }

        public AddressInfoError GetError { set; get; }

        public string GetErrorDescription()
        {
            DescriptionAttribute[] attr = (DescriptionAttribute[])GetError
                .GetType().GetField(GetError.ToString())?
                .GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attr.Length > 0 ? attr[0].Description : string.Empty;
        }

        public DeliveryInfo()
        {
            Address = "";
            City = "";
            Province = "";
            PostalCode = "";
            Country = "";
            Phone = "";
        }

        public DeliveryInfo(string address, string city, string province, string postalCode, string country, string phone)
        {
            Address = address;
            City = city;
            Province = province;
            PostalCode = postalCode;
            Country = country;
            Phone = phone;
        }

        public bool Parse(string address, string city, string province, string postalCode, string country, string phone)
        {
            if (address == "" && city == "" && province == "" && postalCode == "" && country == "" && phone == "")
            {
                GetError = AddressInfoError.InputsEmpty;
                return false;
            }
            else
            if (address == "")
            {
                GetError = AddressInfoError.AddressEmpty;
                return false;
            }
            else if (city == "")
            {
                GetError = AddressInfoError.CityEmpty;
                return false;
            }
            else if (province == "")
{
                GetError = AddressInfoError.ProvinceEmpty;
                return false;
            }
            else if (postalCode == "")
{
                GetError = AddressInfoError.PostalCodeEmpty;
                return false;
            }
            else if (country == "")
{
                GetError = AddressInfoError.CountryEmpty;
                return false;
            }
            else if (phone == "")
            { 
                GetError = AddressInfoError.PhoneEmpty;
                return false;
            }   
            else if (country != "Canada")
            {
                GetError = AddressInfoError.InvalidCountry;
                return false;
            }
            else if (!IsValidPostalCode(postalCode))
            {
                GetError = AddressInfoError.InvalidPostalCode;
                return false;
            }
            else if (!IsValidPhoneNumber(phone))
            {
                GetError = AddressInfoError.InvalidPhone;
                return false;
            }
            else
            {
                Address = address;
                City = city;
                Province = province;
                PostalCode = postalCode;
                Country = country;
                Phone = phone;
                GetError = AddressInfoError.Valid;
                return true;
            }
        }

        private bool IsValidPostalCode(string postalCode)
        {
            // You should replace this regex with one that's appropriate for your use case
            return Regex.IsMatch(postalCode, "^[A-Z]\\d[A-Z] \\d[A-Z]\\d$", RegexOptions.IgnoreCase);
        }

        private bool IsValidPhoneNumber(string phone)
        {
            // You should replace this regex with one that's appropriate for your use case
            return Regex.IsMatch(phone, "^\\d{3}-\\d{3}-\\d{4}$");
        }
    }
}
