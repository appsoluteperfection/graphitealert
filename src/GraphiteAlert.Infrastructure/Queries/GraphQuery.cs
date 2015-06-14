using System.Linq;
using GraphiteAlert.Infrastructure.Clients;
using GraphiteAlert.Infrastructure.Entities;
using System.Collections.Generic;

namespace GraphiteAlert.Infrastructure.Queries
{
    class GraphQuery : IGraphQuery
    {
        private readonly IGraphiteClient _graphiteClient;

        public GraphQuery(IGraphiteClient graphiteClient)
        {
            _graphiteClient = graphiteClient;
        }

        public IEnumerable<Graph> GetAll()
        {
            var allGraphs = _graphiteClient.GetAll();
            return allGraphs.Select(x => new Graph
            {
                Id = x.Id
            });
        }
    }
}