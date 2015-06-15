using System.Linq;
using System.Web.Http;
using GraphiteAlert.Dtos;
using GraphiteAlert.Infrastructure.Configuration;
using GraphiteAlert.Infrastructure.Queries;

namespace GraphiteAlert.Controllers
{
    [RoutePrefix("api/graphs")]
    public class GraphsController : ApiController
    {
        private readonly IGraphQuery _graphQuery;
        private readonly IGraphiteSettings _graphiteSettings;

        public GraphsController(IGraphQuery graphQuery,
            IGraphiteSettings graphiteSettings)
        {
            _graphQuery = graphQuery;
            _graphiteSettings = graphiteSettings;
        }

        public ItemCollection<GraphDto> Get(string q = null)
        {
            var dtos = _graphQuery.Get(q);
            var graphs = dtos.Select(x => new GraphDto
            {
                Id = x.Id,
                Title = x.Id, // TODO, beautify this, remove period, capitalize words, go to town
                Url = _graphiteSettings.GetImageUri(x.Id).ToString()
            });
            return new ItemCollection<GraphDto>
            {
                Items = graphs
            };
        }
    }
}
