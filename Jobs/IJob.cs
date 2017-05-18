namespace Jobs
{
    public interface IJob
    {
        void Process(string message);
    }
}