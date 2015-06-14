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

        private IEnumerable<GraphiteGraphDto> GetAllFromRoot()
        {
            return GetAllFrom();
        }

        private IEnumerable<GraphiteGraphDto> GetAllFrom(GraphiteGraphDto dto = null)
        {
            var q = dto == null ? "*" : dto.Id + ".*";
            var request = new HttpRequestMessage(HttpMethod.Get, _graphiteSettings.GetSearchUri(q));
            HttpResponseMessage response = null;
            try
            {
                response = request.GetResponse();
            }
            catch
            {
                // TODO Not sure what to do with these now 
            }
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

    }
}