using System.Collections.Generic;
using System.Linq;
using GraphiteAlert.Infrastructure.Clients.Dtos;
using GraphiteAlert.Infrastructure.Configuration;

namespace GraphiteAlert.Infrastructure.Clients
{
    class CachedGraphiteClient : IGraphiteClient
    {
        private readonly IGraphiteResourcesCache _cache;
        private readonly GraphiteClient _graphiteClient;

        public CachedGraphiteClient(IGraphiteSettings graphiteSettings,
            IGraphiteResourcesCache cache)
        {
            _cache = cache;
            _graphiteClient = new GraphiteClient(graphiteSettings); // TODO, inject this better, maybe providere
        }

        private readonly object _getItemsLock = new object();
        public IEnumerable<GraphiteGraphDto> GetAll()
        {
            var items = _cache.Items;
            if (null != items) return _cache.Items;
            
            lock (_getItemsLock)
            {
                if (null != items) return _cache.Items;
                items = _graphiteClient.GetAll();
                _cache.Items = items;
            }
            return _cache.Items;
        }

        public IEnumerable<GraphiteGraphDto> Get(string searchQuery)
        {
            return GetAll()
                .Where(x => x != null && x.Id != null)
                .Where(x => x.Id.ToLower().Contains(searchQuery));
        }
    }
}