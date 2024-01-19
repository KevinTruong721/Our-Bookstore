using System;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Data;

namespace BookStoreLIB
{
    internal class DALUserInfo
    {
        public int LogIn(string userName, string password)
        {
            var conn = new SqlConnection(Properties.Settings.Default.LocalDBCon);
            try
            {
                SqlCommand cmd = new SqlCommand
                {
                    Connection = conn,
                    CommandText = "Select UserID from UserData where UserName = @UserName and Password = @Password"
                };
                cmd.Parameters.AddWithValue("@UserName", userName);
                cmd.Parameters.AddWithValue("@Password", password);
                conn.Open();
                int? userId = cmd.ExecuteScalar() as int?;
                if (userId != null && userId > 0) return (int)userId;
                return -1;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return -1;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        public string GetCurrentUsername(int userId)
        {
            var conn = new SqlConnection(Properties.Settings.Default.LocalDBCon);
            try
            {
                SqlCommand cmd = new SqlCommand
                {
                    Connection = conn,
                    CommandText = "Select UserName from UserData where UserID = @UserID"
                };
                cmd.Parameters.AddWithValue("@UserID", userId);
                conn.Open();
                object result = cmd.ExecuteScalar();
                return result != null ? result.ToString() : null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return null;
                Console.WriteLine("error");
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        public bool ChangeUsername(int userId, string newUsername)
        {
            var conn = new SqlConnection(Properties.Settings.Default.LocalDBCon);
            try
            {
                SqlCommand cmd = new SqlCommand
                {
                    Connection = conn,
                    CommandText =
                      "Update UserData Set UserName = @NewUsername Where UserID = @UserID"
                };
                cmd.Parameters.AddWithValue("@NewUsername", newUsername);
                cmd.Parameters.AddWithValue("@UserID", userId);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();

                // Check if the update was successful
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return false;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        public bool ChangePassword(int userId, string currentPassword, string newPassword)
        {
            var conn = new SqlConnection(Properties.Settings.Default.LocalDBCon);
            try
            {
                conn.Open();

                // Verify pass
                using (
                    SqlCommand cmdVerify = new SqlCommand(
                        "SELECT COUNT(*) FROM UserData WHERE UserID = @UserID AND Password = @CurrentPassword",
                        conn))
                {
                    cmdVerify.Parameters.AddWithValue("@UserID", userId);
                    cmdVerify.Parameters.AddWithValue("@CurrentPassword", currentPassword);

                    int userExists = (int)cmdVerify.ExecuteScalar();
                    if (userExists == 0)
                    {
                        // Current password does not match
                        return false;
                    }
                }

                // Update pass
                using (
                    SqlCommand cmdUpdate = new SqlCommand(
                        "UPDATE UserData SET Password = @NewPassword WHERE UserID = @UserID",
                        conn))
                {
                    cmdUpdate.Parameters.AddWithValue("@UserID", userId);
                    cmdUpdate.Parameters.AddWithValue("@NewPassword", newPassword);

                    int rowsAffected = cmdUpdate.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }




    }
}