using StructureMap.Configuration.DSL;

namespace GraphiteAlert.Configuration
{
    public class DefaultRegistry : Registry
    {
        public DefaultRegistry()
        {
            For<ISettings>().Singleton().Use(() => Settings.Instance);
            IncludeRegistry<Infrastructure.Configuration.DefaultRegistry>();
        }
    }
}