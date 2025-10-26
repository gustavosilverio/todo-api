namespace TodoApi.Jobs.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class Job : Attribute
    {
        public string Cron { get; set; }
        public string? JobId { get; set; }
        
        public Job(string cron, string? jobId = null)
        {
            Cron = cron;
            JobId = jobId;
        }
    }
}
