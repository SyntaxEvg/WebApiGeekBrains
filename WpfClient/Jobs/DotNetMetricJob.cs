using System;
using System.Threading.Tasks;
using WpfClient.Client.Interfaces;

using WpfClient.Requests;
using Quartz;
using MetricsManagerClient.Data.Interfaces;

namespace WpfClient.Jobs
{
    [DisallowConcurrentExecution]
    public class DotNetMetricJob : IJob
    {
        private readonly IDotNetMetricModel _model;
        private readonly IDotNetMetricsClient _client;
        private readonly IAppModel _appModel;

        public DotNetMetricJob(IDotNetMetricModel model, IDotNetMetricsClient client,IAppModel appModel)
        {
            _model = model;
            _client = client;
            _appModel = appModel;
        }

        public Task Execute(IJobExecutionContext context)
        {
            if (!_appModel.IsFollowAgent) 
                return Task.CompletedTask;

            var metrics = _client.GetMetricsFromAllCluster(new GetAllDotNetMetricsRequest
            {
                FromTime = _model.LastAddedTime,
                ToTime = DateTimeOffset.FromUnixTimeSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds() + 86400)
            });

            _model?.AddMetrics(metrics?.Metrics!);

            return Task.CompletedTask;
        }
    }
}