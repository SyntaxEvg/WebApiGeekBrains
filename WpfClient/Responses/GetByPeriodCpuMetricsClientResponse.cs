using MetricsManagerClient.Responses.DataObjects;
using System.Collections.Generic;

namespace WpfClient.Responses
{
    public class GetByPeriodCpuMetricsClientResponse
    {
        public List<CpuMetricClientDto> Metrics { get; set; }
    }
}