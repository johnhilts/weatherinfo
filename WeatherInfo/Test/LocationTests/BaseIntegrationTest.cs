using System.IO;

using NUnit.Framework;

namespace LocationTests
{
    public class BaseIntegrationTest : BaseTest
    {
        protected readonly string _keyPath = "../../Integration/env/key.txt";

        [SetUp]
        protected void SetUp()
        {
            if (!File.Exists(_keyPath)) Assert.Ignore("Integration Test: do not run outside of dev environment.");
        }

    }
}
