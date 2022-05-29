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
    public class HddMetricJob : IJob
    {
        private readonly IHddMetricsManagerRepository _managerRepository;
        private readonly IHddMetricsAgentClient _agentClient;
        private readonly IAgentInfoRepository _agentRepository;
        
        public HddMetricJob(
            IHddMetricsManagerRepository managerRepository,
            IHddMetricsAgentClient client,
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
               
                    var metrics = _agentClient.GetHddMetrics(new HddMetricsApiGetRequest
                    {
                        FromTime = _managerRepository.GetLastRecordDate(),
                        ToTime = DateTimeOffset.UtcNow,
                        ClientBaseAddress = item.Address,
                    } );
                if (metrics?.Metrics != null)
                    foreach (var metric in metrics?.Metrics)
                {
                   _managerRepository.Create(new HddMetric
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