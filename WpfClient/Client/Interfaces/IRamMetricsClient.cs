using WpfClient.Requests;
using WpfClient.Responses;

namespace WpfClient.Client.Interfaces
{
    public interface IRamMetricsClient
    {
        GetByPeriodRamMetricsClientResponse GetMetricsFromAgent(GetRamMetricsFromAgentRequest request);

        GetByPeriodRamMetricsClientResponse GetMetricsFromAllCluster(GetAllRamMetricsRequest request);
    }
}