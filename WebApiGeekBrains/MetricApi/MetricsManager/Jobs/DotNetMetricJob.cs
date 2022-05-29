using System;
using System.Threading.Tasks;
using AutoMapper.Internal;
using MetricsManager.Client.Interfaces;
using MetricsManager.DataAccessLayer.Interfaces;
using MetricsManager.DataAccessLayer.Models;
using MetricsManager.Requests;
using Quartz;

namespace MetricsManager.Jobs
{
    [DisallowConcurrentExecution]
    public class DotNetMetricJob : IJob
    {
        private readonly IDotNetMetricsManagerRepository _managerRepository;
        private readonly IDotNetMetricsAgentClient _agentClient;
        private readonly IAgentInfoRepository _agentRepository;

        public DotNetMetricJob(
            IDotNetMetricsManagerRepository managerRepository,
            IDotNetMetricsAgentClient client,
            IAgentInfoRepository agentsRepository)
        {
            _managerRepository = managerRepository;
            _agentClient = client;
            _agentRepository = agentsRepository;
        }


        public async Task Execute(IJobExecutionContext context)
        {
            var agents = _agentRepository.GetAgents();
            foreach (var item in agents)
            {
                var metrics = _agentClient.GetDotNetMetrics(new DotNetMetricApiGetRequest
                {
                    FromTime = _managerRepository.GetLastRecordDate(),
                    ToTime = DateTimeOffset.UtcNow,
                    ClientBaseAddress = item.Address,
                } );
                if (metrics?.Metrics != null)
                    foreach (var metric in metrics?.Metrics)
                {
                    _managerRepository.Create(new DotNetMetric
                    {
                        Time = metric.Time,
                        Value = metric.Value,
                        AgentId = item.Id,
                    });
                }
            }
          //  return Task.CompletedTask;
        }
    }
}