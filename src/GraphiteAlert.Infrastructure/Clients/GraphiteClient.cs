using System.Collections.Generic;
using GraphiteAlert.Infrastructure.Clients.Dtos;

namespace GraphiteAlert.Infrastructure.Clients
{
    class GraphiteClient : IGraphiteClient
    {
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