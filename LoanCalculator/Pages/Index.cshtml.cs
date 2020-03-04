using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using MortgageHelperClassLibrary;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace LoanCalculator.Pages
{
    public class IndexModel : PageModel
    {
        public string Message { get; private set; }
        private readonly ILogger<IndexModel> _logger;
        private readonly IConfiguration Configuration;


        [Required]
        [BindProperty]
        public string Principal { get; set; }

        [Required]
        [BindProperty]
        public string Interest { get; set; }

        [Required]
        [BindProperty]
        public string Duration { get; set; }


        public IndexModel(ILogger<IndexModel> logger, IConfiguration configuration)
        {
            _logger = logger;
            Configuration = configuration;
        }

        public void OnGet()
        {

        }

        public void OnPost()
        {
            _logger.LogInformation("Inside OnPost method");

            if (ModelState.IsValid && Check_Input_Validity())
            {
                string connectionString = Configuration.GetValue<string>("AzureWebJobsStorage");

                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);
                CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();
                CloudQueue queue = queueClient.GetQueueReference("itmd419519queue");

                MortgageModel mortgageModel = new MortgageModel
                {
                    Principal = this.Principal,
                    Interest = this.Interest,
                    Duration = this.Duration
                };

                MortgageSerializer mortgageSerializer = new MortgageSerializer();
                var serializedMortgageModel = mortgageSerializer.Serialize(mortgageModel);
                queue.AddMessageAsync(new CloudQueueMessage(serializedMortgageModel));

                Message = $"Input data has been submitted for processing";
            }
            else
            {
                Message = $"Invalid input data";
            }

        }

        private bool Check_Input_Validity()
        {
            if (double.TryParse(Principal, out double principal) &&
                double.TryParse(Interest, out double interest) &&
                double.TryParse(Duration, out double duration))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
