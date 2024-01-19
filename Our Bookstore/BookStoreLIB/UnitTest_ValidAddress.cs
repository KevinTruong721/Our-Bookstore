using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data;

namespace BookStoreLIB
{
	[TestClass]
	public class UnitTest_ValidAddress
    {
		private readonly DeliveryInfo addressInfo = new DeliveryInfo();

        private void GeneralTest(string address, string city, string province, string postalCode, string country, string phone, bool expectedResult, AddressInfoError expectedError)
        {
            bool result = addressInfo.Parse(address, city, province, postalCode, country, phone);
            Assert.AreEqual(expectedResult, result, "The result of the Parse method did not match the expected result.");
            Assert.AreEqual(expectedError, addressInfo.GetError, "The error set by Parse did not match the expected error.");
        }

        [TestMethod] public void Test_1() => GeneralTest("", "", "", "", "", "", false, AddressInfoError.InputsEmpty);
        [TestMethod] public void Test_2() => GeneralTest("", "Windsor", "Ontario", "M1M 1M1", "Canada", "519-123-4567", false, AddressInfoError.AddressEmpty);
		[TestMethod] public void Test_3() => GeneralTest("123 home st", "", "Ontario", "M1M 1M1", "Canada", "519-123-4567", false, AddressInfoError.CityEmpty);
		[TestMethod] public void Test_4() => GeneralTest("123 home st", "Windsor", "", "M1M 1M1", "Canada", "519-123-4567", false, AddressInfoError.ProvinceEmpty);
		[TestMethod] public void Test_5() => GeneralTest("123 home st", "Windsor", "Ontario", "", "Canada", "519-123-4567", false, AddressInfoError.PostalCodeEmpty);
		[TestMethod] public void Test_6() => GeneralTest("123 home st", "Windsor", "Ontario", "M1M 1M1", "", "519-123-4567", false, AddressInfoError.CountryEmpty);
		[TestMethod] public void Test_7() => GeneralTest("123 home st", "Windsor", "Ontario", "M1M 1M1", "Canada", "", false, AddressInfoError.PhoneEmpty);
		[TestMethod] public void Test_8() => GeneralTest("123 home st", "Windsor", "Ontario", "M1M 1M1", "Russia", "519-123-4567", false, AddressInfoError.InvalidCountry);
		[TestMethod] public void Test_9() => GeneralTest("123 home st", "Windsor", "Ontario", "M1M", "Canada", "519-123-4567", false, AddressInfoError.InvalidPostalCode);
		[TestMethod] public void Test_10() => GeneralTest("123 home st", "Windsor", "Ontario", "M1M1M1", "Canada", "519-123-4567", false, AddressInfoError.InvalidPostalCode);
		[TestMethod] public void Test_11() => GeneralTest("123 home st", "Windsor", "Ontario", "M1M 1M1", "Canada", "519", false, AddressInfoError.InvalidPhone);
		[TestMethod] public void Test_12() => GeneralTest("123 home st", "Windsor", "Ontario", "M1M 1M1", "Canada", "5191234567", false, AddressInfoError.InvalidPhone);
		[TestMethod] public void Test_13() => GeneralTest("123 home st", "Windsor", "Ontario", "M1M 1M1", "Canada", "519-123-4567", true, AddressInfoError.Valid);



		//todo write a bunch of test methods
	}
}