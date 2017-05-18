using Microsoft.Extensions.Logging;

namespace Jobs
{
    public class PlanningJob3 : IPlanningJob3
    {
        private ILogger<PlanningJob3> Logger { get; }

        public PlanningJob3(ILogger<PlanningJob3> logger)
        {
            Logger = logger;
        }

        public void Process(string message)
        {
            Logger.LogInformation($"{GetType().Name} Message: {message}");
        }
    }
}