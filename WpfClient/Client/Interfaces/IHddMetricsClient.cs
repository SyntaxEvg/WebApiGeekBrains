using WpfClient.Requests;
using WpfClient.Responses;

namespace WpfClient.Client.Interfaces
{
    public interface IHddMetricsClient
    {
        GetByPeriodHddMetricsClientResponse GetMetricsFromAgent(GetHddMetricsFromAgentRequest request);

        GetByPeriodHddMetricsClientResponse GetMetricsFromAllCluster(GetAllHddMetricsRequest request);
    }
}