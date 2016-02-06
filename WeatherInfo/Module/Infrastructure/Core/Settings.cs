using System.Configuration;

namespace Infrastructure.Core
{
    public class Settings
    {
        public Settings(string keyPath, string configurationPath)
        {
            KeyPath = keyPath;
            _configurationPath = configurationPath;
        }

        public string KeyPath { get; private set; }
        private string _configurationPath;

        private string _weatherInfoConnectionString;
        public string GetWeatherInfoConnectionString()
        {
            return _weatherInfoConnectionString ?? (_weatherInfoConnectionString = GetConnectionString());
        }

        private string GetConnectionString()
        {
            var connectionStringFromConfig = ConfigurationManager.AppSettings["SQLSERVER_CONNECTION_STRING"];
            return string.IsNullOrWhiteSpace(connectionStringFromConfig) ? System.IO.File.ReadAllText(_configurationPath) : connectionStringFromConfig;
        }
    }
}
