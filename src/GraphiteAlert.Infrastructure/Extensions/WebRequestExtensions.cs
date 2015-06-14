using System;
using System.Net.Http;

namespace GraphiteAlert.Infrastructure.Extensions
{
    public static class HttpRequestMessageExtensions
    {
        public static HttpResponseMessage GetResponse(this HttpRequestMessage request)
        {
            var handler = new HttpClientHandler
            {
                AllowAutoRedirect = false
            };
            var httpClient = new HttpClient(handler)
            {
                Timeout = TimeSpan.FromSeconds(30)
            };
            var sendAsync = httpClient.SendAsync(request);
            return sendAsync.Result;
        } 
    }
}