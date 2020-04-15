using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage.Table;
using MortgageHelperClassLibrary;
using WebJobsSDKSample;

namespace LoanCalculator.Pages
{
    public class DisplayTableResultsModel : PageModel
    {
        private static IConfiguration Configuration;

        public static MortgageTableEntryDetails MortgageTableEntryDetails;
        public string MonthlyPayment;

        public DisplayTableResultsModel(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static async Task<CloudTable> CreateTable(string storageAcctName, string tableUri, string storageKey, string tableName)
        {
            Uri uri = new Uri(tableUri);

            var creds = new Microsoft.WindowsAzure.Storage.Auth.StorageCredentials(
                storageAcctName,
                storageKey);

            //Client
            CloudTableClient tableClient = new CloudTableClient(uri, creds);

            //Table
            CloudTable table = tableClient.GetTableReference(tableName);

            await table.CreateIfNotExistsAsync();

            return table;
        }

        public async void OnGet()
        {
            if (!Request.Query.ContainsKey("Id"))
                return;

            String idToRemove = "?id=";

            string splitString = Request.QueryString.Value.Substring(idToRemove.Length, Request.QueryString.Value.Length - idToRemove.Length);

            if (String.IsNullOrEmpty(splitString))
                return;

            try
            {
                //var table = await StorageTableHelper.CreateTable(Configuration.GetValue<string>("storageaactname"),
                //    Configuration.GetValue<string>("tableuri"), Configuration.GetValue<string>("storagekey"),
                //    Configuration.GetValue<string>("tablename"));

                var table = await StorageTableHelper.CreateTable("itmd419519",
                    "https://itmd419519.table.core.windows.net/Mortgage", "9HIjtlOI0iMdXzgneppc25c7CiGFf3ta0EPtn6OyZ4e1oU9Ia8YP/zayJZDJZWKCkLhq2BVLHlYR8gUXr9KFHw==",
                    "Mortgage");

                var result = await table.ExecuteAsync(TableOperation.Retrieve<MortgageTableEntry>("Mortgage", splitString));

                if (result.HttpStatusCode != 200)
                    return;

                var info = (MortgageTableEntry)result.Result;

                MortgageTableEntryDetails.Principal = info.Principal.ToString();
                MortgageTableEntryDetails.Interest = info.Interest.ToString();
                MortgageTableEntryDetails.Duration = info.Duration.ToString();
                MortgageTableEntryDetails.MotnhlyPayment = info.MotnhlyPayment.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}