using System;

namespace WpfClient.Requests
{
    public class GetAllNetworkMetricsRequest
    {
        public DateTimeOffset FromTime { get; set; }
        
        public DateTimeOffset ToTime { get; set; }
    }
}