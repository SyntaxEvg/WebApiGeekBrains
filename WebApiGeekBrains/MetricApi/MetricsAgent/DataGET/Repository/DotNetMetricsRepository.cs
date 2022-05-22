﻿using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using Dapper;
using MetricsAgent.Metrics;

namespace MetricsAgent.DataAccessLayer.Repositories
{
    public class DotNetMetricsRepository : IDotNetMetricsRepository
    {
        private readonly string _connectionString;
        
        public DotNetMetricsRepository(IDatabaseSettingsProvider dbProvider)
        {
            _connectionString = dbProvider.GetConnectionString();
        }
        
        public void Create(DotNetMetric item)
        {
            using var connection = new SQLiteConnection(_connectionString);
            connection.Execute("INSERT INTO dotnetmetrics(value, time) VALUES(@value, @time)", 
                new { 
                    value = item.Value,
                    time = item.Time.ToUnixTimeSeconds()
                });
        }
        
        public IList<DotNetMetric> GetByTimePeriod(DateTimeOffset from, DateTimeOffset to)
        {
            using var connection = new SQLiteConnection(_connectionString);
            return connection
                .Query<DotNetMetric>(string.Format(
                    "SELECT Id, Time, Value FROM dotnetmetrics WHERE time BETWEEN {0} AND {1}", 
                    from.ToUnixTimeSeconds(), 
                    to.ToUnixTimeSeconds()))
                .ToList();
        }
    }
}