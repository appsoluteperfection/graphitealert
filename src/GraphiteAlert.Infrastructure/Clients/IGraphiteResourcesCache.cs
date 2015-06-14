using System.Collections.Generic;
using GraphiteAlert.Infrastructure.Clients.Dtos;

namespace GraphiteAlert.Infrastructure.Clients
{
    interface IGraphiteResourcesCache
    {
        IEnumerable<GraphiteGraphDto> Items { get; set; } 
    }
}