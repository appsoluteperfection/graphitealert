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
                return _caches
                    .Select(x => x.Items)
                    .FirstOrDefault(x => null != x);
            }
            set
            {
                _caches.ToList().ForEach(x => x.Items = value);
            }
        }
    }
}