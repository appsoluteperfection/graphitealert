using StructureMap.Configuration.DSL;

namespace GraphiteAlert.Infrastructure.Configuration
{
    public class DefaultRegistry : Registry
    {
        public DefaultRegistry()
        {
            IncludeRegistry<ClientRegistry>();
            IncludeRegistry<QueryRegistry>();
        }
    }
}