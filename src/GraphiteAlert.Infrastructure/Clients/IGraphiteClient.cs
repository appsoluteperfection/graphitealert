using System;
using GraphiteAlert.Infrastructure.Clients.Dtos;
using System.Collections.Generic;

namespace GraphiteAlert.Infrastructure.Clients
{
    public interface IGraphiteClient
    {
        IEnumerable<GraphiteGraphDto> GetAll();
        IEnumerable<GraphiteGraphDto> Get(string searchQuery);
        IEnumerable<Tuple<dynamic, dynamic>> GetDataPoints(string collection);
    }
}