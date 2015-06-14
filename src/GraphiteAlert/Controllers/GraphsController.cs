using System.Linq;
using System.Web.Http;
using GraphiteAlert.Dtos;
using GraphiteAlert.Infrastructure.Queries;

namespace GraphiteAlert.Controllers
{
    [RoutePrefix("api/graphs")]
    public class GraphsController : ApiController
    {
        private readonly IGraphQuery _graphQuery;

        public GraphsController(IGraphQuery graphQuery)
        {
            _graphQuery = graphQuery;
        }

        public ItemCollection<GraphDto> Get(string q = null)
        {
            var dtos = _graphQuery.GetAll();
            var graphs = dtos.Select(x => new GraphDto
            {
                Id = x.Id
            });
            return new ItemCollection<GraphDto>
            {
                Items = graphs
            };
        }
    }
}
