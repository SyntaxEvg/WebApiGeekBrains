using System.Collections.Generic;
using System.Linq;

namespace MetricsManager.Models
{
    public class AgentPool
    {
        private Dictionary<int, AgentInfoDto> _values;

        public AgentPool()
        {
            _values = new Dictionary<int, AgentInfoDto>();
        }

        public void Add(AgentInfoDto value)
        {
            if (!_values.ContainsKey(value.Id))
                _values.Add(value.Id, value);
        }

        public AgentInfoDto[] Get()
        {
            return _values.Values.ToArray();
        }

        public Dictionary<int, AgentInfoDto> Values
        {
            get { return _values; }
            set { _values = value; }
        }

    }
}
