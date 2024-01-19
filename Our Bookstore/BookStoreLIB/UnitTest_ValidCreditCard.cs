using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data;

namespace BookStoreLIB
{
	[TestClass]
	public class UnitTest_ValidCreditCard
    {
		private readonly CardInfo cardInfo = new CardInfo();

		private void GeneralTest(string cvv, string creditCardNumber, string expirationDate, CardInfo_Error out_GetError, bool out_return)
        {
			Assert.AreEqual(out_return, cardInfo.Parse(cvv, creditCardNumber, expirationDate));
			Assert.AreEqual(out_GetError, cardInfo.GetError);
        }

		[TestMethod] public void Test_1() => GeneralTest("", "", "dffff", CardInfo_Error.InputsEmpty, false);
		[TestMethod] public void Test_2() => GeneralTest("1234", "1234567812345678", "12/25", CardInfo_Error.InvalidCVV, false);
		[TestMethod] public void Test_3() => GeneralTest("123", "1234567812345678", "dfg", CardInfo_Error.InvalidExpirationDate, false);
		[TestMethod] public void Test_4() => GeneralTest("123", "ddff1254", "10/24", CardInfo_Error.CardNumberError, false);
		[TestMethod] public void Test_5() => GeneralTest("123", "1234567812345678", "", CardInfo_Error.InputsEmpty, false);
		[TestMethod] public void Test_6() => GeneralTest("12", "1234567812345678", "10/25", CardInfo_Error.InvalidCVV, false);
		[TestMethod] public void Test_7() => GeneralTest("123", "1234567812345678", "10/22", CardInfo_Error.InvalidExpirationDate, false);
		[TestMethod] public void Test_8() => GeneralTest("123", "124587", "dffff", CardInfo_Error.CardNumberError, false);
		[TestMethod] public void Test_9() => GeneralTest("123", "1234567812345678", "10/27", 0, true);

	}
}