using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace WebJobsSDKSample
{
    public class MortgageTableEntry : TableEntity
    {
        public Guid Id { get; set; }
        public double Principal { get; set; }
        public double Interest { get; set; }
        public double Duration { get; set; }
        public double MotnhlyPayment { get; set; }

        public MortgageTableEntry(string part, string row)
        {
            this.PartitionKey = part;
            this.RowKey = row;
        }
    }
}