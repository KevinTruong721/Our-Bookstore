using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.ObjectModel;
using System.Linq;

namespace BookStoreLIB
{
    [TestClass]
    public class UnitTest_CheckoutDialog
    {
        BookOrder bookOrder = new BookOrder();
        public ObservableCollection<OrderItem> OrderList { get; set; }

        [TestMethod]
        public void TestMethod1()
        {
            OrderItem orderitem = new OrderItem("5432554654", "Book of life", 16.50, 1);
            OrderItem orderitem1 = new OrderItem("1356545673", "Book of Moon", 32.99, 2);

            bookOrder.AddItem(orderitem);
            bookOrder.AddItem(orderitem1);

            double actualTotal = bookOrder.OrderItemList.Sum(item => item.SubTotal);
            double expectedTotal = 82.48;

            Assert.AreEqual(expectedTotal, actualTotal);
        }
    }
}
