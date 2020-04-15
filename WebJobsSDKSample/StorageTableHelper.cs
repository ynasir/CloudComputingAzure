using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WebJobsSDKSample
{
    public class StorageTableHelper
    {
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
    }
}
