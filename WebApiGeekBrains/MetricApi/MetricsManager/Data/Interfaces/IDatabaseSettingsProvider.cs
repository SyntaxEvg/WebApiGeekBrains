
namespace MetricsManager.DataAccessLayer.Interfaces
{
    public interface IDatabaseSettingsProvider
    {
        public string GetConnectionString { get; set; }
    }
}