using Microsoft.VisualStudio.TestTools.UnitTesting;
using MortgageCalcHelperLibrary;
using System;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            double monthlypayment = MortgageCalcHelper.ComputeMonthlyPayment(2300, 5.2, 3.33);

            monthlypayment = Math.Round(monthlypayment, 2);

            Assert.AreEqual(monthlypayment, 40.19);
        }
    }
}
