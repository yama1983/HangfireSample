using Microsoft.Extensions.Logging;

namespace Jobs
{
    public class Job1 : IJob1
    {
        private ILogger<Job1> Logger { get; }

        public Job1(ILogger<Job1> logger)
        {
            Logger = logger;
        }

        public void Process(string message)
        {
            Logger.LogInformation($"{GetType().Name} Message: {message}");
        }
    }
}