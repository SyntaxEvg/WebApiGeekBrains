using MetricsManager.DataAccessLayer.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLog;
using System;

namespace MetricsManager.DataAccessLayer
{
    public class DatabaseSettingsProvider: IDatabaseSettingsProvider
    {
        ILogger<DatabaseSettingsProvider> _logger;
        private  string _connectionString;

        public DatabaseSettingsProvider(IConfiguration _Configuration, ILogger<DatabaseSettingsProvider> logger )
        {
            _logger = logger;
            try
            {
                string connectionString = _Configuration.GetConnectionString("LocalDB");
                _connectionString = connectionString!= null ? connectionString : throw new Exception() ;
            }
            catch (System.Exception ex)
            {
                _logger.LogInformation($"{ex}");
                Environment.Exit(0);

            }


            var t = _Configuration;
           
        }
        public string GetConnectionString
        {
            get { return _connectionString; }
            set { _connectionString = value; }
        }

      
    }
}