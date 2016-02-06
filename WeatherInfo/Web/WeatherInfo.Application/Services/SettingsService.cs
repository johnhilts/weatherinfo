namespace WeatherInfo.Application.Services
{
    public class SettingsService
    {
        private readonly string _applicationRootPath;
        private readonly string _keyPath;
        private readonly string _configurationPath;

        public SettingsService(string applicationRootPath, string keyPath, string configurationPath)
        {
            _applicationRootPath = applicationRootPath;
            _keyPath = keyPath;
            _configurationPath = configurationPath;
        }

        public string GetKeyPath()
        {
            return _keyPath;
        }

        public string GetConfigurationPath()
        {
            return _configurationPath;
        }
    }
}
