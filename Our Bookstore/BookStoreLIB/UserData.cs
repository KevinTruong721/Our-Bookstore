using System;
using System.ComponentModel;


namespace BookStoreLIB
{
    public enum UserData_Error
    {
        [Description("No User Name or Password was provided")]
        InputsEmpty = 1,
        [Description("The given Password is invalid")]
        InvalidPassword,
        [Description("Failed to login, either the User Name or Password is incorrect")]
        NoOneFound,
        [Description("New username is invalid")]
        InvalidNewUsername
    }

    public class UserData
    {
        public int UserID { set; get; }
        public string LoginName { set; get; }
        public string Password { set; get; }
        public UserData_Error GetError { private set; get; }

        public string GetErrorDescription()
        {
            DescriptionAttribute[] attr = (DescriptionAttribute[])GetError
                .GetType().GetField(GetError.ToString())?
                .GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attr[0]?.Description ?? "";
        }

        public bool LogIn(string loginName, string password)
        {
            if (loginName == "" || password == "")
            {
                GetError = UserData_Error.InputsEmpty;
                return false;
            }
            if (password.Length < 6)
            {
                GetError = UserData_Error.InvalidPassword;
                return false;
            }
            // First char must be a letter, cannot be a digit
            if (IsDigit(password[0]))
            {
                GetError = UserData_Error.InvalidPassword;
                return false;
            }
            foreach (char c in password)
            {   // Each character must be a lower/upper/digit
                if (IsLower(c) || IsDigit(c) || IsUpper(c)) continue;
                GetError = UserData_Error.InvalidPassword;
                return false;
            }
            var dbUser = new DALUserInfo();
            UserID = dbUser.LogIn(loginName, password);
            if (UserID > 0)
            {
                LoginName = loginName;
                Password = password;
                GetError = 0;
                return true;
            }
            else
            {
                GetError = UserData_Error.NoOneFound;
                return false;
            }
        }

        public string GetCurrentUsername(int userID)
        {
            DALUserInfo dalUserInfo = new DALUserInfo();
            return dalUserInfo.GetCurrentUsername(userID);
        }


        public bool ChangeUsername(int userID, string newUsername)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(newUsername) || newUsername.Length < 3)
                {
                    GetError = UserData_Error.InvalidNewUsername;
                    return false;
                }

                // Create a new instance of DALUserInfo
                var dbUser = new DALUserInfo();

                // Call the ChangeUsername method in DALUserInfo
                bool success = dbUser.ChangeUsername(userID, newUsername);

                if (success)
                {
                    // If the username change was successful, update the LoginName property
                    LoginName = newUsername;
                    GetError = 0;
                    return true;
                }
                else
                {
                    GetError = UserData_Error.NoOneFound;
                    return false;
                }
            }
            catch (Exception ex)
            {
       
                return false;
            }
        }

        public bool ChangePassword(int userID, string currentPassword, string newPassword)
        {
            DALUserInfo dalUserInfo = new DALUserInfo();
            return dalUserInfo.ChangePassword(userID, currentPassword, newPassword);
        }




        public static bool IsDigit(char c) => c >= 48 && c <= 57;
        public static bool IsLower(char c) => c >= 97 && c <= 122;
        public static bool IsUpper(char c) => c >= 65 && c <= 90;
    }
}