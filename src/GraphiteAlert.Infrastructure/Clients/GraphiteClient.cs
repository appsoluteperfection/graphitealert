using System;
using System.Collections.Generic;
using System.Net.Http;
using GraphiteAlert.Infrastructure.Clients.Dtos;
using GraphiteAlert.Infrastructure.Configuration;
using GraphiteAlert.Infrastructure.Extensions;
using Newtonsoft.Json;

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
            var allFromRoot = GetAllFromRoot(); // TODO, cache
            return allFromRoot;
        }

        public IEnumerable<GraphiteGraphDto> Get(string searchQuery)
        {
            throw new System.NotImplementedException();
        }

        private IEnumerable<GraphiteGraphDto> GetAllFromRoot()
        {
            return GetAllFrom("*");
        }  
        
        private IEnumerable<GraphiteGraphDto> GetAllFrom(string q)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, _graphiteSettings.GetSearchUri(q));
            var response = request.GetResponse();
            var json = response.Content.ReadAsStringAsync().Result;
            var results = JsonConvert.DeserializeObject<IEnumerable<GraphiteGraphDto>>(json);
            return results;
        }
    }
}