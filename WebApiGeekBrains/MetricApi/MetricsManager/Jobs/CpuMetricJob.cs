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
    public class CpuMetricJob : IJob
    {
        private readonly ICpuMetricsManagerRepository _managerRepository;
        private readonly ICpuMetricsAgentClient _agentClient;
        private readonly IAgentInfoRepository _agentRepository;

        public CpuMetricJob(
            ICpuMetricsManagerRepository managerRepository, 
            ICpuMetricsAgentClient client, 
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

                var metrics = _agentClient.GetCpuMetrics(new CpuMetricApiGetRequest
                {
                    FromTime = _managerRepository.GetLastRecordDate(),
                    ToTime = DateTimeOffset.UtcNow,
                    ClientBaseAddress = item.Url.ToString(),
                });
                foreach (var metric in metrics.Metrics)
                {
                    _managerRepository.Create(new CpuMetric
                    {
                        Time = metric.Time,
                        Value = metric.Value,
                        AgentId = item.Id,
                    });

                }
            }
          
        }
    }
}