using Microsoft.Extensions.Logging;

namespace Jobs
{
    public class Job2 : IJob2
    {
        private ILogger<Job2> Logger { get; }

        public Job2(ILogger<Job2> logger)
        {
            Logger = logger;
        }

        public void Process(string message)
        {
            Logger.LogInformation($"{GetType().Name} Message: {message}");
        }
    }
}