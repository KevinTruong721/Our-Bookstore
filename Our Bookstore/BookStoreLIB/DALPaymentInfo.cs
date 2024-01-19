using System;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using System.Collections.Generic;

namespace BookStoreLIB
{
    public class DALPaymentInfo
    {
        SqlConnection conn;
        DataSet dsPaymentInfo;
        public DALPaymentInfo()
        {
            conn = new SqlConnection(Properties.Settings.Default.LocalDBCon);
            conn.Open();
        }
        public DataSet FindPaymentInfo(int UserID)
        {
            try
            {
                string strSQL = "select CardNumber, Expiration, CVV from PaymentInfo where UserID = @UserID;";
                SqlCommand cmdSelPaymentInfo = new SqlCommand(strSQL, conn);
                cmdSelPaymentInfo.Parameters.AddWithValue("@UserID", UserID);
                dsPaymentInfo = new DataSet("PaymentInfo");
                SqlDataAdapter daPaymentInfo = new SqlDataAdapter(cmdSelPaymentInfo);
                daPaymentInfo.Fill(dsPaymentInfo, "PaymentInfo");
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return dsPaymentInfo;
        }

        public int RemovePaymentInfo(int UserID, long CardNumber)
        {
            string sql = "delete from PaymentInfo where UserID = @UserID and CardNumber = @CardNumber;";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@UserID", UserID);
            cmd.Parameters.AddWithValue("@CardNumber", CardNumber);
            return cmd.ExecuteNonQuery();
        }

        public int EditPaymentInfo (int UserID, long OldCardNumber, long? NewCardNumber, DateTime? NewExpiration, int? NewCVV)
        {
            SqlCommand cmd = new SqlCommand("", conn);
            cmd.Parameters.AddWithValue("@UserID", UserID);
            cmd.Parameters.AddWithValue("@OldCardNumber", OldCardNumber);
            List<string> changes = new List<string>();
            if (NewCardNumber.HasValue)
            {                
                changes.Add("CardNumber = @NewCardNumber");
                cmd.Parameters.AddWithValue("@NewCardNumber", NewCardNumber.Value);
            }
            if (NewExpiration.HasValue)
            {
                changes.Add("Expiration = @NewExpiration");
                cmd.Parameters.AddWithValue("@NewExpiration", NewExpiration.Value);
            }
            if (NewCVV.HasValue)
            {
                changes.Add("CVV = @NewCVV");
                cmd.Parameters.AddWithValue("@NewCVV", NewCVV.Value);
            }
            string sql = "update PaymentInfo set " + string.Join(", ", changes) + " where UserID = @UserID and CardNumber = @OldCardNumber;";
            cmd.CommandText = sql;
            return cmd.ExecuteNonQuery();
        }

        public int InsertPaymentInfo (int UserID, long CardNumber, DateTime Expiration, int CVV)
        {
            string sql = "insert into PaymentInfo (UserID, CardNumber, Expiration, CVV) VALUES (@UserID, @CardNumber, @Expiration, @CVV);";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@UserID", UserID);
            cmd.Parameters.AddWithValue("@CardNumber", CardNumber);
            cmd.Parameters.AddWithValue("@Expiration", Expiration);
            cmd.Parameters.AddWithValue("@CVV", CVV);
            return cmd.ExecuteNonQuery();
        }
    }
}
