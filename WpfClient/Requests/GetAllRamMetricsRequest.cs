using System;

namespace WpfClient.Requests
{
    public class GetAllRamMetricsRequest
    {
        public DateTimeOffset FromTime { get; set; }
        
        public DateTimeOffset ToTime { get; set; }
    }
}