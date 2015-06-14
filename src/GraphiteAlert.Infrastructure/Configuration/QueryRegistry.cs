using GraphiteAlert.Infrastructure.Queries;
using StructureMap.Configuration.DSL;

namespace GraphiteAlert.Infrastructure.Configuration
{
    public class QueryRegistry : Registry
    {
        public QueryRegistry()
        {
            For<IGraphQuery>().Use<GraphQuery>();
        }
    }
}