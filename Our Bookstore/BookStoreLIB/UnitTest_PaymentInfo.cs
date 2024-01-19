using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.SqlClient;
using System;
using System.Data;

namespace BookStoreLIB
{
    [TestClass]
    public class UnitTest_PaymentInfo
    {
        SqlConnection conn = new SqlConnection(Properties.Settings.Default.LocalDBCon);
        DALPaymentInfo dalPaymentInfo = new DALPaymentInfo();

        [TestInitialize]
        public void setup()
        {
            conn.Open();
        }

        [TestMethod]
        public void Test_DAL()
        {
            int userId = 2;
            long cardNum = 1234_5678_9876_5432;
            DateTime expiration = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            int cvv = 321;
            new SqlCommand("delete from PaymentInfo where UserID="+userId+" and CardNumber="+cardNum+";", conn).ExecuteNonQuery();
            Assert.AreEqual(
                1,
                dalPaymentInfo.InsertPaymentInfo(userId, cardNum, expiration, cvv),
                "Insertion of new payment info should succeed"
            );

            long newCard = 1111_2222_3333_4444;
            int newCVV = 123;
            new SqlCommand("delete from PaymentInfo where UserID="+userId+" and CardNumber="+newCard+";", conn).ExecuteNonQuery();
            Assert.AreEqual(
                1,
                dalPaymentInfo.EditPaymentInfo(userId, cardNum, newCard, null, newCVV),
                "Editing an existing record should succeed"
            );

            DataSet ds = dalPaymentInfo.FindPaymentInfo(userId);
            DataRowCollection drc = ds.Tables["PaymentInfo"].Rows;
            Assert.AreEqual(1, drc.Count, "FindPaymentInfo should've found one record but didn't");
            object[] record = drc[0].ItemArray;
            Assert.AreEqual(3, record.Length, "Should be three columns in the return");
            Assert.AreEqual(newCard, long.Parse(record[0].ToString()), "Expected card number didnt match the one found");
            Assert.AreEqual(expiration, (DateTime)(record[1]), "Expected expiration didnt match the one found");
            Assert.AreEqual(newCVV, int.Parse(record[2].ToString()), "Expected CVV didnt match the one found");

            Assert.AreEqual(
                1,
                dalPaymentInfo.RemovePaymentInfo(userId, newCard),
                "Tried to delete a record that should exist but failed"
            );
        }
    }
}
