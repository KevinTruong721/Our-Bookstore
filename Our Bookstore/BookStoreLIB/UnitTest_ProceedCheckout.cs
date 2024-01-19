using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace BookStoreLIB
{
    [TestClass]
    public class UnitTest_ProceedCheckout
    {
        SqlConnection cn = new SqlConnection(
                Properties.Settings.Default.LocalDBCon);
        BookOrder bookOrder = new BookOrder();

        //Test inputOrder procedure / successful proceed to checkout test case.
        [TestMethod]
        public void TestMethod1()
        {
            //specify val of test inputs
            int inputUserId = 1;
            //specify value of expected outputs
            //get orderID Bc it is going to increment every time calling PlaceOrder
            int expectedOrderId;
            try
            {
                cn.Open();
                string rowCountQuery = $"SELECT COUNT(*) FROM {"Orders"}";
                SqlCommand command = new SqlCommand(rowCountQuery, cn);
                expectedOrderId = 3 + Convert.ToInt32(command.ExecuteScalar());
                //obtain the actual outputs by calling the method under testing
                int actualOrderId = bookOrder.PlaceOrder(inputUserId);
                //verify results
                Assert.AreEqual(expectedOrderId, actualOrderId);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }
        //Failing proceed to checkout test case
        [TestMethod]
        public void TestMethod2()
        {
            //specify val of test inputs
            int inputUserId = 1;
            int inputOrderId = 1;
            //specify value of expected outputs
            int actualOrderId = bookOrder.PlaceOrder(inputUserId);
            //verify results
            Assert.AreNotEqual(inputOrderId, actualOrderId);
        }


        public void ClearItemCart(String isbn, String title, double price, int quantity)
        {
            //add item to the cart
            bookOrder.AddItem(new OrderItem(isbn, title, price, quantity));

            //clear the order item list cart
            bookOrder.OrderItemList.Clear();

            //specify value of test input
            int inputOrderItemListCount = 0;

            //specify value of expected output
            int actualOrderItemListCount = bookOrder.OrderItemList.Count;

            //If equal, then the item cart has successfully been cleared
            Assert.AreEqual(inputOrderItemListCount, actualOrderItemListCount);
        }


        //Test of clearing the cart
        [TestMethod]
        public void Test_3() => ClearItemCart("5432554654", "Book of life", 16.50, 1);
        [TestMethod]
        public void Test_4() => ClearItemCart("1356545673", "Book of Moon", 32.99, 2);
        
    }
}
