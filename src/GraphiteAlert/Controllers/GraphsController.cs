using System.Web.Http;
using GraphiteAlert.Dtos;

namespace GraphiteAlert.Controllers
{
    [Authorize]
    [RoutePrefix("api/graphs")]
    public class GraphsController : ApiController
    {

        public ItemCollection<GraphDto> GetAll()
        {
            return new ItemCollection<GraphDto>
            {
                Items = new []
                {
                    new GraphDto{Title = ""}, 
                    new GraphDto()
                }
            };
        }

        public ItemCollection<GraphDto> Get(string q = null)
        {
            return new ItemCollection<GraphDto>()
            {
                Items = new[]
                {
                    new GraphDto(), 
                    new GraphDto()
                }
            };
        }
    }
}
