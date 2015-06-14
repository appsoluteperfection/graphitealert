using System.Collections.Generic;
using System.Linq;
using GraphiteAlert.Infrastructure.Clients.Dtos;

namespace GraphiteAlert.Infrastructure.Clients
{
    class GraphiteResourcesCache : IGraphiteResourcesCache
    {

        private IEnumerable<IGraphiteResourcesCache> _caches; 

        public GraphiteResourcesCache()
        {
            _caches = new List<IGraphiteResourcesCache>
            {
                new MemoryGraphiteResourcesCache(),
                new FileGraphiteResourcesCache()
            };
        }

        public IEnumerable<GraphiteGraphDto> Items
        {
            get
            {
                var firstCache = _caches
                    .Select(x => x.Items)
                    .FirstOrDefault(x => null != x);
                if (null == firstCache) return null;
                var graphDtos = 
                    firstCache as GraphiteGraphDto[] ?? firstCache.ToArray();
                if (_caches.Any(x => null == x))
                {
                    Items = graphDtos;
                }
                return graphDtos;
            }
            set
            {
                foreach (var cache in _caches)
                {
                    lock (_cacheUpdateLock)
                    {
                        cache.Items = value.Select(x => x).ToArray();
                    }
                }
            }
        }

        private readonly object _cacheUpdateLock = new object();
    }
}