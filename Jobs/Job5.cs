using Microsoft.Extensions.Logging;

namespace Jobs
{
    public class Job5 : IJob5
    {
        private ILogger<Job5> Logger { get; }

        public Job5(ILogger<Job5> logger)
        {
            Logger = logger;
        }

        public void Process(string message)
        {
            Logger.LogInformation($"{GetType().Name} Message: {message}");
        }
    }
}