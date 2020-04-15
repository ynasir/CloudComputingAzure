using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Table;
using MortgageHelperClassLibrary;
using System;

namespace WebJobsSDKSample
{
    public class Functions
    {
        //private static CloudTable MortgageCloudTable = null;

        public static void ProcessQueueMessage([QueueTrigger("itmd419519queue")] string message,
            [Table("Mortgage")] CloudTable mortgageTable,
            ILogger logger)
        {
            logger.LogInformation(message);
            WriteTableStorage(mortgageTable, message, logger);
        }

        //public static async Task<CloudTable> CreateTable(string tablename)
        //{
        //    if (MortgageCloudTable != null)
        //        return MortgageCloudTable;

        //    string storageAcct = Program.Configuration["storageacctname"];
        //    string tableUri = Program.Configuration["tableUri"];
        //    string storageKey = Program.Configuration["storagekey"];

        //    //MortgageCloudTable = await StorageTableHelper(storageAcct, tableUri, storageKey)

        //    return MortgageCloudTable;
        //}

        public static async void WriteTableStorage(CloudTable cloudTable, string message, ILogger logger)
        {
            logger.LogInformation("Deserializing data");

            MortgageSerializer mortgageSerializer = new MortgageSerializer();
            MortgageModel data = mortgageSerializer.Deserialize(message);

            double monthlyPayment = MortgageCalcHelper.ComputeMonthlyPayment(
                Convert.ToDouble(data.Principal),
                Convert.ToDouble(data.Interest),
                Convert.ToDouble(data.Duration));

            TableEntity tableEntity = new MortgageTableEntry(cloudTable.Name, DateTime.Now.Ticks.ToString())
            {
                Id = new Guid(data.Id),
                Principal = Convert.ToDouble(data.Principal),
                Interest = Convert.ToDouble(data.Interest),
                Duration = Convert.ToDouble(data.Duration),
                MotnhlyPayment = monthlyPayment
            };

            TableOperation insertOrMergeOperation = TableOperation.InsertOrMerge(tableEntity);
            await cloudTable.ExecuteAsync(insertOrMergeOperation);
        }


    }
}
