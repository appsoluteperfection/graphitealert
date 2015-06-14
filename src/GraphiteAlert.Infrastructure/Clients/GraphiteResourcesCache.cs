using System.Collections.Generic;
using GraphiteAlert.Infrastructure.Clients.Dtos;

namespace GraphiteAlert.Infrastructure.Clients
{
    class GraphiteResourcesCache : IGraphiteResourcesCache
    {
        public IEnumerable<GraphiteGraphDto> Items { get; set; }
    }
}