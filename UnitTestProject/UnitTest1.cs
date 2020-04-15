using Microsoft.VisualStudio.TestTools.UnitTesting;
using MortgageCalcHelperLibrary;
using System;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod()]
        [TestCategory("input")]
        [DataRow(2300, 5.2, 3.33, 40.19)]
        public void TestMethod1(double principal, double duration, double interest, double result)
        {
            double monthlypayment = MortgageCalcHelper.ComputeMonthlyPayment(principal, duration, interest);

            monthlypayment = Math.Round(monthlypayment, 2);

            Assert.AreEqual(result, monthlypayment);
        }
    }
}
