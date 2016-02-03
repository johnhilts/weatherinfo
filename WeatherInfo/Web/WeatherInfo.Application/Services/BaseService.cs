namespace WeatherInfo.Application.Services
{
    public class BaseService
    {
        protected SettingsService SettingsService { get; private set; }

        public BaseService(SettingsService settingsService )
        {
            SettingsService = settingsService;
        }
    }
}
