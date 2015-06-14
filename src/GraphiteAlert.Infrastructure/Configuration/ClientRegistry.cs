using GraphiteAlert.Infrastructure.Clients;
using StructureMap.Configuration.DSL;

namespace GraphiteAlert.Infrastructure.Configuration
{
    public class ClientRegistry : Registry
    {
        public ClientRegistry()
        {
            For<IGraphiteClient>().Use<GraphiteClient>();
        }
    }
}