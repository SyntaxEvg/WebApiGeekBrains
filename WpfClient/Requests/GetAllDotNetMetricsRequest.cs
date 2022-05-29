using System;

namespace WpfClient.Requests
{
    public class GetAllDotNetMetricsRequest
    {
        public DateTimeOffset FromTime { get; set; }
        
        public DateTimeOffset ToTime { get; set; }
    }
}