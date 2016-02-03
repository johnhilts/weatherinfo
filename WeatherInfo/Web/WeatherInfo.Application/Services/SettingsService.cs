namespace WeatherInfo.Application.Services
{
    public class SettingsService
    {
        private readonly string _applicationRootPath;
        private readonly string _keyPath;

        public SettingsService(string applicationRootPath, string keyPath)
        {
            _applicationRootPath = applicationRootPath;
            _keyPath = keyPath;
        }

        public string GetKeyPath()
        {
            return _keyPath;
        }
    }
}
