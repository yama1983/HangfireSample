using Microsoft.Extensions.Logging;

namespace Jobs
{
    public class Job4 : IJob4
    {
        private ILogger<Job4> Logger { get; }

        public Job4(ILogger<Job4> logger)
        {
            Logger = logger;
        }

        public void Process(string message)
        {
            Logger.LogInformation($"{GetType().Name} Message: {message}");
        }
    }
}