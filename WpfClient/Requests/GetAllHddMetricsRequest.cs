using System;

namespace WpfClient.Requests
{
    public class GetAllHddMetricsRequest
    {
        public DateTimeOffset FromTime { get; set; }
        
        public DateTimeOffset ToTime { get; set; }
    }
}