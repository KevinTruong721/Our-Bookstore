using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BookStoreLIB
{
    [TestClass]
    public class UnitTest_Login
    {
        private readonly UserData userData = new UserData();

        private void GeneralTest(string in_loginName, string in_password, UserData_Error out_GetError, bool out_return)
        {
            Assert.AreEqual(out_return, userData.LogIn(in_loginName, in_password));
            Assert.AreEqual(out_GetError, userData.GetError);
        }

        [TestMethod] public void Test_1 () => GeneralTest("", "", UserData_Error.InputsEmpty, false);
        [TestMethod] public void Test_2 () => GeneralTest("asdf", "asdf", UserData_Error.InvalidPassword, false);
        [TestMethod] public void Test_3 () => GeneralTest("asdf", "abcdefg", UserData_Error.NoOneFound, false);
        [TestMethod] public void Test_4 () => GeneralTest("asdf", "5abcdef", UserData_Error.InvalidPassword, false);
        [TestMethod] public void Test_5 () => GeneralTest("asdf", "ABCDEFG", UserData_Error.NoOneFound, false);
        [TestMethod] public void Test_6 () => GeneralTest("asdf", "A234567", UserData_Error.NoOneFound, false);
        [TestMethod] public void Test_7 () => GeneralTest("asdf", "Aab123$", UserData_Error.InvalidPassword, false);
        [TestMethod] public void Test_8 () => GeneralTest("meppel", "me1234", 0, true);
    }
}
   