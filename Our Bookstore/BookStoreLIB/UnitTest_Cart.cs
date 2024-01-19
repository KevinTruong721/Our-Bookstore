using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace BookStoreLIB
{
    [TestClass]
    public class UnitTest_Cart
    {
        private readonly BookOrder bookOrder = new BookOrder();
        [TestInitialize]
        public void TestInitialize()
        {
            bookOrder.OrderItemList.Clear();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            bookOrder.OrderItemList.Clear();
        }

        
        [TestMethod]
        private void addBook(String isbn, String title, double price, int quantity)
        {
            bookOrder.AddItem(new OrderItem(isbn, title, price, quantity));
            // Find book within list and check if it exists, if not fail
            for (int i = 0; i < bookOrder.OrderItemList.Count; i++)
            {
                if (bookOrder.OrderItemList[i].BookID == isbn)
                {
                    Assert.AreEqual(isbn, bookOrder.OrderItemList[i].BookID);
                    Assert.AreEqual(title, bookOrder.OrderItemList[i].BookTitle);
                    Assert.AreEqual(price, bookOrder.OrderItemList[i].UnitPrice);
                    Assert.AreEqual(quantity, bookOrder.OrderItemList[i].Quantity);
                    return;
                }
            }
            Assert.Fail();
        }

        [TestMethod]
        private void removeBook(String isbn, String title, double price, int quantity)
        {
            bookOrder.RemoveItem(isbn);
            if (bookOrder.OrderItemList.Count == 0)
            {
                Assert.AreEqual(0, bookOrder.OrderItemList.Count);
                return;
            }
            for (int i = 0; i < bookOrder.OrderItemList.Count; i++)
            {
                Assert.AreNotEqual(isbn, bookOrder.OrderItemList[i].BookID);
            }
        }


        [TestMethod] public void Test_1 () => addBook("1234567890123", "Book1", 10.00, 1);

        [TestMethod] public void Test_2 () => addBook("1234567890123", "Book1", 10.00, 2);

        [TestMethod] public void Test_3 ()
        {

            addBook("1234567890123", "Book1", 10.00, 1);
            removeBook("1234567890123", "Book1", 10.00, 0);
        }

        [TestMethod]
        public void Test_4()
        {

            addBook("1234567890123", "Book1", 10.00, 1);
            addBook("1234567890124", "Book2", 10.00, 1);
            removeBook("1234567890123", "Book1", 10.00, 0);
        }

    }
}
   