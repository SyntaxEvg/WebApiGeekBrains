using WpfClient.Requests;
using WpfClient.Responses;

namespace WpfClient.Client.Interfaces
{
    public interface ICpuMetricsClient
    {
        GetByPeriodCpuMetricsClientResponse GetMetricsFromAgent(GetCpuMetricsFromAgentRequest request);

        GetByPeriodCpuMetricsClientResponse GetMetricsFromAllCluster(GetAllCpuMetricsRequest request);
    }
}