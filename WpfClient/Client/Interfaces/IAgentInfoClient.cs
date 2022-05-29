using WpfClient.Requests;
using WpfClient.Responses;

namespace WpfClient.Client.Interfaces
{
    public interface IAgentInfoClient
    {
        void RegisterAgent(AgentInfoRegisterRequest request);
        
        void UnregisterAgent(AgentInfoUnregisterRequest request);
        
        GetAgentsInfoClientResponse GetRegisterAgents();
    }
}