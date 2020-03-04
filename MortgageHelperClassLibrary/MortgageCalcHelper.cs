using System;
using System.Collections.Generic;
using System.Text;

namespace MortgageHelperClassLibrary
{
    public class MortgageCalcHelper
    {
        public static double ComputeMonthlyPayment(double principal, double years, double rate)
        {
            double monthly = 0;
            double top = principal * rate / 1200.00;
            double bottom = 1 - Math.Pow(1.0 + rate / 1200.0, -12.0 * years);
            // http://www.bankrate.com/calculators/mortgages/loan-calculator.aspx
            monthly = top / bottom;

            monthly = Math.Round(monthly, 2);
            return monthly;
        }
    }
}
