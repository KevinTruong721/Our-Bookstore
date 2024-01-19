using System;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;

namespace BookStoreLIB
{
    public class DALDeliveryInfo
    {
        SqlConnection conn;
        DataSet dsDeliveryInfo;

        public DALDeliveryInfo()
        {
            conn = new SqlConnection(Properties.Settings.Default.LocalDBCon);
            conn.Open();
        }

        public DataSet FindDeliveryInfo(int UserID)
        {
            try
            {
                string strSQL = "SELECT Address, City, Province, PostalCode, Country, Phone " +
                                "FROM SavedAddress WHERE UserID = @UserID;";
                SqlCommand cmdSelAddressInfo = new SqlCommand(strSQL, conn);
                cmdSelAddressInfo.Parameters.AddWithValue("@UserID", UserID);
                dsDeliveryInfo = new DataSet("DeliveryInfo");
                SqlDataAdapter daDeliveryInfo = new SqlDataAdapter(cmdSelAddressInfo);
                daDeliveryInfo.Fill(dsDeliveryInfo, "DeliveryInfo");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return dsDeliveryInfo;
        }

        public int RemoveDeliveryInfo(int UserID, string Address, string City = null, string Province = null, string PostalCode = null, string Country = null, string Phone = null)
        {
            string sql = "DELETE FROM SavedAddress WHERE UserID = @UserID AND Address = @Address";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@UserID", UserID);
            cmd.Parameters.AddWithValue("@Address", Address);
            if (City != null)
            {
                sql += " AND City = @City";
                cmd.Parameters.AddWithValue("@City", City);
            }
            if (Province != null)
            {
                sql += " AND Province = @Province";
                cmd.Parameters.AddWithValue("@Province", Province);
            }
            if (PostalCode != null)
            {
                sql += " AND PostalCode = @PostalCode";
                cmd.Parameters.AddWithValue("@PostalCode", PostalCode);
            }
            if (Country != null)
            {
                sql += " AND Country = @Country";
                cmd.Parameters.AddWithValue("@Country", Country);
            }
            if (Phone != null)
            {
                sql += " AND Phone = @Phone";
                cmd.Parameters.AddWithValue("@Phone", Phone);
            }
            cmd.CommandText = sql;
            return cmd.ExecuteNonQuery();   

        }

        public int EditDeliveryInfo(int UserID, string OldAddress, string NewAddress, string City, string Province, string PostalCode, string Country, string Phone)
        {
            string sql = "UPDATE SavedAddress SET Address = @NewAddress, City = @City, " +
                         "Province = @Province, PostalCode = @PostalCode, Country = @Country, " +
                         "Phone = @Phone " +
                         "WHERE UserID = @UserID AND Address = @OldAddress;";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@UserID", UserID);
            cmd.Parameters.AddWithValue("@OldAddress", OldAddress);
            cmd.Parameters.AddWithValue("@NewAddress", NewAddress);
            cmd.Parameters.AddWithValue("@City", City);
            cmd.Parameters.AddWithValue("@Province", Province);
            cmd.Parameters.AddWithValue("@PostalCode", PostalCode);
            cmd.Parameters.AddWithValue("@Country", Country);
            cmd.Parameters.AddWithValue("@Phone", Phone ?? (object)DBNull.Value);
            return cmd.ExecuteNonQuery();
        }

        public int InsertDeliveryInfo(int UserID, string Address, string City, string Province, string PostalCode, string Country, string Phone)
        {
            string sql = "INSERT INTO SavedAddress (UserID, Address, City, Province, PostalCode, Country, Phone) " +
                         "VALUES (@UserID, @Address, @City, @Province, @PostalCode, @Country, @Phone);";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@UserID", UserID);
            cmd.Parameters.AddWithValue("@Address", Address);
            cmd.Parameters.AddWithValue("@City", City);
            cmd.Parameters.AddWithValue("@Province", Province);
            cmd.Parameters.AddWithValue("@PostalCode", PostalCode);
            cmd.Parameters.AddWithValue("@Country", Country);
            cmd.Parameters.AddWithValue("@Phone", Phone ?? (object)DBNull.Value);
            return cmd.ExecuteNonQuery();
        }
    }
}
