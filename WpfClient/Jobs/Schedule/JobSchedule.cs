using System;

namespace WpfClient.Jobs.Schedule
{
    public class JobSchedule
    {
        public JobSchedule(Type jobType, string perjob)
        {
            JobType = jobType;
            CronExpression = perjob;
        }

        public Type JobType { get; }
        public string CronExpression { get; }
    }

}