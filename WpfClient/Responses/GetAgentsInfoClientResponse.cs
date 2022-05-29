using MetricsManagerClient.Responses.DataObjects;
using System.Collections.Generic;

namespace WpfClient.Responses
{
    public class GetAgentsInfoClientResponse
    {
        public List<AgentInfoClientDto> Agents { get; set; }
    }
}