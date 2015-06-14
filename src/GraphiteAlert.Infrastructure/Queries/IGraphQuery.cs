using System.Collections.Generic;
using GraphiteAlert.Infrastructure.Entities;

namespace GraphiteAlert.Infrastructure.Queries
{
    public interface IGraphQuery
    {
        IEnumerable<Graph> GetAll();
    }
}