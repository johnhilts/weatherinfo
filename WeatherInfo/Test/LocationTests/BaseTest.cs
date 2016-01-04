using AutoMapper;
using NUnit.Framework;

using Location.Map;

namespace LocationTests
{
    [SetUpFixture]
    public class BaseTest
    {
        [SetUp]
        public void Setup()
        {
            Mapper.Initialize(x => x.AddProfile<MapProfile>());
        }

    }
}
