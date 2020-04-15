using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoanCalculator
{
    public class MortgageTableEntryDetails
    {
        public string Principal { get; set; }
        public string Interest { get; set; }
        public string Duration { get; set; }
        public string MotnhlyPayment { get; set; }
    }
}
