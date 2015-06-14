using System.Collections.Generic;
using GraphiteAlert.Infrastructure.Clients.Dtos;
using GraphiteAlert.Infrastructure.Configuration;

namespace GraphiteAlert.Infrastructure.Clients
{
    class GraphiteClient : IGraphiteClient
    {
        private readonly IGraphiteSettings _graphiteSettings;

        public GraphiteClient(IGraphiteSettings graphiteSettings)
        {
            _graphiteSettings = graphiteSettings;
        }

        public IEnumerable<GraphiteGraphDto> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<GraphiteGraphDto> Get(string searchQuery)
        {
            throw new System.NotImplementedException();
        }
    }
}