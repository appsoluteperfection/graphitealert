using GraphiteAlert.Infrastructure.Clients.Dtos;
using System.Collections.Generic;

namespace GraphiteAlert.Infrastructure.Clients
{
    interface IGraphiteClient
    {
        IEnumerable<GraphiteGraphDto> GetAll();
        IEnumerable<GraphiteGraphDto> Get(string searchQuery);
    }
}