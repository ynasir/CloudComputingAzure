using Microsoft.WindowsAzure.Storage.Table;

namespace WebJobsSDKSample
{
    public class MortgageTableEntry : TableEntity
    {
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