using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.SqlClient;
using System;
using System.Data;

namespace BookStoreLIB
{
    [TestClass]
    public class UnitTest_DeliveryInfo
    {
        SqlConnection conn = new SqlConnection(Properties.Settings.Default.LocalDBCon);
        DALDeliveryInfo dalDeliveryInfo = new DALDeliveryInfo();

        [TestInitialize]
        public void Setup()
        {
            conn.Open();
        }

        [TestMethod]
        public void Test_DALDeliveryInfo()
        {
            string address = "123 Main St";
            string city = "Windsor";
            string province = "ON";
            string postalCode = "A1A 1A1";
            string country = "Canada";
            string phone = "999-999-9999";
            new SqlCommand("delete from SavedAddress where UserID=1 and Address='123 Main St';", conn).ExecuteNonQuery();
            Assert.AreEqual(
                1,
                dalDeliveryInfo.InsertDeliveryInfo(1, address, city, province, postalCode, country, phone),
                "Insertion of new delivery info should succeed"
            );

            string newAddress = "456 Elm St";
            new SqlCommand("delete from SavedAddress where UserID=1 and Address='456 Elm St';", conn).ExecuteNonQuery();
            Assert.AreEqual(
                1,
                dalDeliveryInfo.EditDeliveryInfo(1, address, newAddress, city, province, postalCode, country, phone),
                "Editing an existing record should succeed"
            );

            DataSet ds = dalDeliveryInfo.FindDeliveryInfo(1);
            DataRowCollection drc = ds.Tables["DeliveryInfo"].Rows;
            DataRow record = drc[drc.Count - 1];
            Assert.AreEqual(newAddress, record["Address"].ToString(), "Expected address didn't match the one found");
            Assert.AreEqual(city, record["City"].ToString(), "Expected city didn't match the one found");
            Assert.AreEqual(province, record["Province"].ToString(), "Expected province didn't match the one found");
            Assert.AreEqual(postalCode, record["PostalCode"].ToString(), "Expected postal code didn't match the one found");
            Assert.AreEqual(country, record["Country"].ToString(), "Expected country didn't match the one found");
            Assert.AreEqual(phone, record["Phone"].ToString(), "Expected phone didn't match the one found");

            Assert.AreEqual(
                1,
                dalDeliveryInfo.RemoveDeliveryInfo(1, newAddress, city, province, postalCode),
                "Tried to delete a record that should exist but failed"
            );
        }

        [TestCleanup]
        public void Cleanup()
        {
            conn.Close();
        }
    }
}