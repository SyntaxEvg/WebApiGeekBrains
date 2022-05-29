using System;
using System.Linq;
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
    public class RamMetricJob : IJob
    {
        private readonly IRamMetricsManagerRepository _managerRepository;
        private readonly IRamMetricsAgentClient _agentClient;
        private readonly IAgentInfoRepository _agentRepository;
        
        public RamMetricJob(
            IRamMetricsManagerRepository managerRepository,
            IRamMetricsAgentClient client,
            IAgentInfoRepository agentsRepository)
        {
            _managerRepository = managerRepository;
            _agentClient = client;
            _agentRepository = agentsRepository;
        }

        public Task Execute(IJobExecutionContext context)
        {
            var agents = _agentRepository.GetAgents();
            foreach (var item in agents.AsParallel())
            {
                var metrics = _agentClient.GetRamMetrics(new RamMetricApiGetRequest
                {
                    FromTime = _managerRepository.GetLastRecordDate(),
                    ToTime = DateTimeOffset.UtcNow,
                    ClientBaseAddress = item.Address,
                });
                if (metrics?.Metrics != null)
                    foreach (var metric in metrics?.Metrics)
                {
                        _managerRepository.Create(new RamMetric
                        {
                            Time = metric.Time,
                            Value = metric.Value,
                            AgentId = item.Id,
                        });                   
                }

            }
            return Task.CompletedTask;
        }
    }
}