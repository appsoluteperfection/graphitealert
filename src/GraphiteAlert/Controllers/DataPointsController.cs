using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Antlr.Runtime;
using GraphiteAlert.Dtos;
using GraphiteAlert.Infrastructure.Clients;

namespace GraphiteAlert.Controllers
{
    [RoutePrefix("api/datapoints")]   
    public class DataPointsController : ApiController
    {
        private readonly IGraphiteClient _graphiteClient;

        public DataPointsController(IGraphiteClient graphiteClient)
        {
            _graphiteClient = graphiteClient;
        }

        public IEnumerable <DataPoint> GetAll()
        {
            return _graphiteClient.GetDataPoints("")
                .Select(d =>
                {
                    Double x;
                    Double y;
                    if (!Double.TryParse(d.Item1.ToString(), out x)) return null;
                    if (!Double.TryParse(d.Item2.ToString(), out y)) return null;
                    return new {x, y};
                })
                .Where(d => d != null)
                .Select(d => new DataPoint {XAxis = d.x, YAxis = d.y});  
        }
    }
}