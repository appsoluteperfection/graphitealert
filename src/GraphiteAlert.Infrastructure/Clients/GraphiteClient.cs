using System;
using GraphiteAlert.Infrastructure.Clients.Dtos;
using GraphiteAlert.Infrastructure.Configuration;
using GraphiteAlert.Infrastructure.Extensions;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace GraphiteAlert.Infrastructure.Clients
{
    class GraphiteClient : IGraphiteClient
    {
        private readonly IGraphiteSettings _graphiteSettings;

        public GraphiteClient(IGraphiteSettings graphiteSettings)
        {
            _graphiteSettings = graphiteSettings;
   
        }

        public IEnumerable<GraphiteGraphDto> GetAll()
        {
            var allFromRoot = GetAllFromRoot();
            return allFromRoot;
        }

        public IEnumerable<GraphiteGraphDto> Get(string searchQuery)
        {
            var all = GetAll();
            return all
                .Where(x => x != null && x.Id != null)
                .Where(x => x.Id.ToLower().Contains(searchQuery));
        }

        public IEnumerable<Tuple<dynamic, dynamic>> GetDataPoints(string collection)
        {
            var response = GetResponse(
                new Uri(
                    "http://graphite.local.uship.com/render?from=-6h&target=carbon.agents.*.*&title=My%20First%20Graph&rawData=true"));
            var body = response.Content.ReadAsStringAsync().Result;
            var collections = body.Split('\n');
            var collectionDetails = collections.Select(s => s.Split('|'))
                .Select(d => new {LeftSide = d.ElementAt(0).Split(','), RightSide = d.ElementAt(1).Split(',')})
                .Select(
                    whole =>
                        new
                        {
                            name = whole.LeftSide.ElementAt(0),
                            startingX = whole.LeftSide.ElementAt(1),
                            interval = whole.LeftSide.ElementAt(3),
                            dataPoints = whole.RightSide
                        });
            var nameDataPoints = collectionDetails.Select(d => new
            {
                name = d.name,
                datapoints = GetDataPointsFor(d.dataPoints, d.startingX, d.interval)
            });

            return nameDataPoints.First().datapoints;
        }

        private IEnumerable<Tuple<dynamic, dynamic>> GetDataPointsFor(IEnumerable<Object> dataPoints, object startingX, object interval)
        {
            var startingXValue = Double.Parse(startingX.ToString());
            var intervalValue = Double.Parse((interval.ToString()));
            for (int i = 0; i < dataPoints.Count(); i++)
            {
                var x = startingXValue + intervalValue*i;
                var y = dataPoints.ElementAt(i);
                yield return new Tuple<dynamic, dynamic>(x, y);
            }
        }

        private IEnumerable<GraphiteGraphDto> GetAllFromRoot()
        {
            return GetAllFrom();
        }

        private IEnumerable<GraphiteGraphDto> GetAllFrom(GraphiteGraphDto dto = null)
        {
            var q = dto == null ? "*" : dto.Id + ".*";
            Uri requestUri = _graphiteSettings.GetSearchUri(q);
            var response = GetResponse(requestUri);
            if (null == response) yield break; // Bad response, prolly should not have these, but oh well for now
            if (HttpStatusCode.OK != response.StatusCode) yield break; // No bad children
            var json = response.Content.ReadAsStringAsync().Result;
            var results = JsonConvert.DeserializeObject<IEnumerable<GraphiteGraphDto>>(json).ToArray();
            if (!results.Any())
            {
                yield return dto;
            }
            else if (results.Count() > _graphiteSettings.MaximumGraphiteChildrenToSeek)
            {
                yield break; // too many results to process
            }
            else
            {
                var children = results.AsParallel().SelectMany(GetAllFrom);
                foreach (var child in children)
                {
                    yield return child;
                }
            }
        }

        private static HttpResponseMessage GetResponse(Uri requestUri)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
            HttpResponseMessage response = null;
            try
            {
                response = request.GetResponse();
            }
            catch
            {
                // TODO Not sure what to do with these now 
            }
            return response;
        }
    }
}