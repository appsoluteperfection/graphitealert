using System;

namespace GraphiteAlert.Infrastructure.Configuration
{
    public class GraphiteSettings : IGraphiteSettings
    {
        private readonly Uri _baseUri;

        public GraphiteSettings(string baseUrl)
        {
            _baseUri = new Uri(baseUrl);
        }

        public Uri GetSearchUri(string searchText)
        {
            return new Uri(_baseUri, "metrics/json/find?query=" + searchText);
        }
    }
}