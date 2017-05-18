using Microsoft.Extensions.Logging;

namespace Jobs
{
    public class PlanningJob2 : IPlanningJob2
    {
        private ILogger<PlanningJob2> Logger { get; }

        public PlanningJob2(ILogger<PlanningJob2> logger)
        {
            Logger = logger;
        }

        public void Process(string message)
        {
            Logger.LogInformation($"{GetType().Name} Message: {message}");
        }
    }
}