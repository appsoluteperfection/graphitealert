using GraphiteAlert.Infrastructure.Configuration;
using StructureMap.Configuration.DSL;

namespace GraphiteAlert.Configuration
{
    public class DefaultRegistry : Registry
    {
        public DefaultRegistry()
        {
            For<ISettings>().Singleton().Use(() => Settings.Instance);
            For<IGraphiteSettings>()
                .Singleton()
                .Use(c => new GraphiteSettings(c.GetInstance<ISettings>().GraphiteBaseUrl));
            IncludeRegistry<Infrastructure.Configuration.DefaultRegistry>();
        }
    }
}