using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using Dapper;
using MetricsManager.DataAccessLayer.Interfaces;
using MetricsManager.DataAccessLayer.Models;

namespace MetricsManager.DataAccessLayer.Repositories
{
    public class AgentInfoRepository : IAgentInfoRepository
    {
        private readonly string _connectionString;
        
        public AgentInfoRepository(IDatabaseSettingsProvider dbProvider)
        {
            _connectionString = dbProvider.GetConnectionString;
        }

        public void Create(MetricsManager.Models.AgentInfoDto item)
        {
            //$"INSERT INTO {item}(value, time) VALUES(@value, @time)";
            string add= item.Address;
            using var connection = new SQLiteConnection(_connectionString);
            connection.Execute("INSERT INTO agents(Address) VALUES(@Address)",
               new
               {
                   Address = item.Address,
               });
        }
        
        public void Delete(string url)
        {
            using var connection = new SQLiteConnection(_connectionString);
            connection.Execute("DELETE FROM agents WHERE Address=@Address",
                new
                {
                    agentUrl = url,
                });
        }
        
        public IList<MetricsManager.Models.AgentInfoDto> GetAgents()
        {
            using var connection = new SQLiteConnection(_connectionString);
            try
            {
                return connection
                .Query<MetricsManager.Models.AgentInfoDto>("SELECT id, Address FROM agents")
                .ToList();
            }
            catch (System.Exception)
            {

                return new List<MetricsManager.Models.AgentInfoDto>();
            }
            
        }
    }
}