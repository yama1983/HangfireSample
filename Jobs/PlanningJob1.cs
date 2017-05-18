using Microsoft.Extensions.Logging;

namespace Jobs
{
    public class PlanningJob1 : IPlanningJob1
    {
        private ILogger<PlanningJob1> Logger { get; }

        public PlanningJob1(ILogger<PlanningJob1> logger)
        {
            Logger = logger;
        }

        public void Process(string message)
        {
            Logger.LogInformation($"{GetType().Name} Message: {message}");
        }
    }
}