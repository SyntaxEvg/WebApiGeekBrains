using WpfClient.Requests;
using WpfClient.Responses;

namespace WpfClient.Client.Interfaces
{
    public interface INetworkMetricsClient
    {
        GetByPeriodNetworkMetricsClientResponse GetMetricsFromAgent(GetNetworkMetricsFromAgentRequest request);

        GetByPeriodNetworkMetricsClientResponse GetMetricsFromAllCluster(GetAllNetworkMetricsRequest request);
    }
}