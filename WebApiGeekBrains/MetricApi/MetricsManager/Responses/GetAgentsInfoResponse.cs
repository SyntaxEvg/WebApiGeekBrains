using System.Collections.Generic;
using MetricsManager.Responses.DataTransferObjects;

namespace MetricsManager.Responses
{
    public class GetAgentsInfoResponse
    {
        public IList<AgentInfoDto> Agents { get; set; }
    }
}