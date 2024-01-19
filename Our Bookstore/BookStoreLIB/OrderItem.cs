using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace BookStoreLIB
{
    public class OrderItem : INotifyPropertyChanged
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

        //
        //Summary:
        //  Gets or sets the ISBN of the book.
        public String BookID { get; set; }

        //
        //Summary:
        //  Gets or sets the title of the book.
        public String BookTitle { get; set; }

        //
        //Summary:
        //  Gets or sets the quantity of books ordered.
        private int _quantity;
        public int Quantity
        {
            get { return _quantity; }
            set
            {
                _quantity = value;
                Notify(nameof(Quantity));
            }
        }

        //
        //Summary:
        //  Gets or sets the unit price of the book.
        public double UnitPrice { get; set; }

        //
        //Summary:
        //  Gets or sets the subtotal price for this order item.
        public double SubTotal { get; set; }
        
        //
        //Summary:
        //  Initializes a new instance of the OrderItem class.
        //Parameters:
        //  isbn:The ISBN of the book.
        //  title:The title of the book.
        //  unitPrice:The unit price of the book.
        //  quantity:The quantity of books ordered.
        public OrderItem(String isbn, String title, double unitPrice, int quantity)
        {
            BookID = isbn;
            BookTitle = title;
            UnitPrice = unitPrice;
            Quantity = quantity;
            SubTotal = UnitPrice * Quantity;
        }

        //
        //Summary:
        //  Returns a string representation of the order item in XML format.
        //Rreturn:
        //  An XML string that represents the order item.
        public override string ToString()
        {
            string xml = "<OrderItem ISBN='" + BookID + "'";
            xml += " Quantity='" + Quantity + "' />";
            return xml;
        }
    }
}
