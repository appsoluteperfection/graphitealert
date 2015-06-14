using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using GraphiteAlert.Infrastructure.Clients.Dtos;
using Newtonsoft.Json;

namespace GraphiteAlert.Infrastructure.Clients
{
    class FileGraphiteResourcesCache : IGraphiteResourcesCache
    {
        private const string FileName = "GraphiteGraphs.json";

        private static string FilePath
        {
            get { return Path.Combine(HttpRuntime.AppDomainAppPath, FileName); }
        }

        public IEnumerable<GraphiteGraphDto> Items
        {
            get
            {
                if (!File.Exists(FilePath)) return null;
                var fileContents = File.ReadAllText(FilePath);
                return JsonConvert.DeserializeObject<IEnumerable<GraphiteGraphDto>>(fileContents);
            }
            set
            {
                File.WriteAllText(FilePath, JsonConvert.SerializeObject(value));
            }
        }
    }
}