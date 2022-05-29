using System;
using System.Threading.Tasks;
using WpfClient.Client.Interfaces;

using WpfClient.Requests;
using Quartz;
using MetricsManagerClient.Data.Interfaces;

namespace WpfClient.Jobs
{
    [DisallowConcurrentExecution]
    public class HddMetricJob : IJob
    {
        private readonly IHddMetricModel _model;
        private readonly IHddMetricsClient _client;
        private readonly IAppModel _appModel;

        public HddMetricJob(
            IHddMetricModel model,
            IHddMetricsClient client,
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

            var metrics = _client.GetMetricsFromAllCluster(new GetAllHddMetricsRequest
            {
                FromTime = _model.LastAddedTime,
                ToTime = DateTimeOffset.FromUnixTimeSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds() + 86400)
            });
            if (metrics?.Metrics != null)
                _model.AddMetrics(metrics.Metrics);

            return Task.CompletedTask;
        }
    }
}