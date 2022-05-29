using MetricsManagerClient.Responses.DataObjects;
using System.Collections.Generic;

namespace WpfClient.Responses
{
    public class GetByPeriodRamMetricsClientResponse
    {
        public List<RamMetricClientDto> Metrics { get; set; }
    }
}