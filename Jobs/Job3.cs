using Microsoft.Extensions.Logging;

namespace Jobs
{
    public class Job3 : IJob3
    {
        private ILogger<Job3> Logger { get; }

        public Job3(ILogger<Job3> logger)
        {
            Logger = logger;
        }

        public void Process(string message)
        {
            Logger.LogInformation($"{GetType().Name} Message: {message}");
        }
    }
}