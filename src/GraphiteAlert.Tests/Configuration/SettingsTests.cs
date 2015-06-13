using FluentAssertions;
using GraphiteAlert.Configuration;
using NUnit.Framework;

namespace GraphiteAlert.Tests.Configuration
{
    [TestFixture]
    [Category(TestRunCategory.Integration)]
    public class SettingsTests
    {
        [Test]
        public void Should_contain_required_graphite_base_url_in_settings()
        {
            Settings.Instance.GraphiteBaseUrl.Should().NotBeNullOrWhiteSpace("File settings do not exist or do not have the GraphiteBaseUrl filled in");
        }
    }
}