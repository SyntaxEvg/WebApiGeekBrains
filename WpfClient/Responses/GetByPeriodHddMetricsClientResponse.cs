using MetricsManagerClient.Responses.DataObjects;
using System.Collections.Generic;

namespace WpfClient.Responses
{
    public class GetByPeriodHddMetricsClientResponse
    {
        public List<HddMetricClientDto> Metrics { get; set; }
    }
}