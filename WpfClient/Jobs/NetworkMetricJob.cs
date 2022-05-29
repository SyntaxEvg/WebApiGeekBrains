using System;
using System.Threading.Tasks;
using WpfClient.Client.Interfaces;

using WpfClient.Requests;
using Quartz;
using MetricsManagerClient.Data.Interfaces;

namespace WpfClient.Jobs
{
    [DisallowConcurrentExecution]
    public class NetworkMetricJob : IJob
    {
        private readonly INetworkMetricModel _model;
        private readonly INetworkMetricsClient _client;
        private readonly IAppModel _appModel;

        public NetworkMetricJob(
            INetworkMetricModel model,
            INetworkMetricsClient client,
            IAppModel appModel)
        {
            _model = model;
            _client = client;
            _appModel = appModel;
        }

        public Task Execute(IJobExecutionContext context)
        {
            if (!_appModel.IsFollowAgent) 
                return Task.CompletedTask;

            var metrics = _client.GetMetricsFromAllCluster(new GetAllNetworkMetricsRequest
            {
                FromTime = _model.LastAddedTime,
                ToTime = DateTimeOffset.FromUnixTimeSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds() + 86400)
            });
            if(metrics?.Metrics !=null)
            _model.AddMetrics(metrics.Metrics);

            return Task.CompletedTask;
        }
    }
}