using AutoMapper;
using YogaTime.Services.Automappers;
using Xunit;

namespace YogaTime.Services.Tests
{
    public class MapperTest
    {
        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void TestMap()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ServiceProfile());
            });

            config.AssertConfigurationIsValid();
        }
    }
}
