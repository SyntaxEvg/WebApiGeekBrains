
using System.Collections.Generic;
using MetricsManager.DataAccessLayer.Models;

namespace MetricsManager.DataAccessLayer.Interfaces
{
    public interface IAgentInfoRepository
    {
        void Create(MetricsManager.Models.AgentInfoDto item);
        void Delete(string url);
        IList<MetricsManager.Models.AgentInfoDto> GetAgents();
    }
}