using WpfClient.Requests;
using WpfClient.Responses;

namespace WpfClient.Client.Interfaces
{
    public interface IDotNetMetricsClient
    {
        GetByPeriodDotNetMetricsClientResponse GetMetricsFromAgent(GetDotNetMetricsFromAgentRequest request);

        GetByPeriodDotNetMetricsClientResponse GetMetricsFromAllCluster(GetAllDotNetMetricsRequest request);
    }
}