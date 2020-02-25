using Microsoft.Azure.WebJobs;

namespace WebJobsSDKSample
{
    public class CustomNameResolver : INameResolver
    {
        public string Resolve(string name)
        {
            return Program.Configuration[name];
        }
    }
}
