using System.IO;

using NUnit.Framework;

namespace LocationTests
{
    public class BaseIntegrationTest : BaseTest
    {
        protected readonly string _openWeatherMapKeyPath = "../../Integration/env/openWeatherMapKey.txt";
        protected readonly string _worldWeatherOnlineKeyPath = "../../Integration/env/worldWeatherOnlineKey.txt";

        [SetUp]
        protected void SetUp()
        {
            if (!File.Exists(_openWeatherMapKeyPath)) Assert.Ignore("Integration Test: do not run outside of dev environment.");
        }

    }
}
