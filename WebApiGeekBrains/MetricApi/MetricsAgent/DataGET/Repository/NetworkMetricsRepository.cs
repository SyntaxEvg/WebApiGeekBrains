﻿using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using Dapper;
using MetricsAgent.Metrics;

namespace MetricsAgent.DataAccessLayer.Repositories
{
    public class NetworkMetricsRepository : INetworkMetricsRepository
    {
        private readonly string _connectionString;
        
        public NetworkMetricsRepository(IDatabaseSettingsProvider dbProvider)
        {
            _connectionString = dbProvider.GetConnectionString();
        }
        
        public void Create(NetworkMetric item)
        {
            using var connection = new SQLiteConnection(_connectionString);
            connection.Execute("INSERT INTO networkmetrics(value, time) VALUES(@value, @time)", 
                new { 
                    value = item.Value,
                    time = item.Time.ToUnixTimeSeconds()
                });
        }
        
        public IList<NetworkMetric> GetByTimePeriod(DateTimeOffset from, DateTimeOffset to)
        {
            using var connection = new SQLiteConnection(_connectionString);
            return connection
                .Query<NetworkMetric>(string.Format(
                    "SELECT Id, Time, Value FROM networkmetrics WHERE time BETWEEN {0} AND {1}", 
                    from.ToUnixTimeSeconds(), 
                    to.ToUnixTimeSeconds()))
                .ToList();
        }
    }
}