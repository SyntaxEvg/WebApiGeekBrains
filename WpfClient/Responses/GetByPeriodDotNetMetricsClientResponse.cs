using MetricsManagerClient.Responses.DataObjects;
using System.Collections.Generic;

namespace WpfClient.Responses
{
    public class GetByPeriodDotNetMetricsClientResponse
    {
        public List<DotNetMetricClientDto> Metrics { get; set; }
    }
}