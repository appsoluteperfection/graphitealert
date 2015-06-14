using System.Collections.Generic;
using GraphiteAlert.Infrastructure.Clients.Dtos;

namespace GraphiteAlert.Infrastructure.Clients
{
    class MemoryGraphiteResourcesCache : IGraphiteResourcesCache
    {
        public IEnumerable<GraphiteGraphDto> Items { get; set; }
    }
}