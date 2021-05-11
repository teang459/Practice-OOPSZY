using RussiaPublicHealthProtection.ApplicationServices.Ports.Cache;
using RussiaPublicHealthProtection.DomainObjects;
using RussiaPublicHealthProtection.DomainObjects.Ports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RussiaPublicHealthProtection.InfrastructureServices.Repositories
{
    public class NetworkPublicHealthProtectionRepository : NetworkRepositoryBase, IReadOnlyPublicHealthProtectionRepository
    {
        private readonly IDomainObjectsCache<PublicHealthProtection> _publicHealthProtectionCache;

        public NetworkPublicHealthProtectionRepository(string host, ushort port, bool useTls, IDomainObjectsCache<PublicHealthProtection> publicHealthProtectionCache)
            : base(host, port, useTls)
            => _publicHealthProtectionCache = publicHealthProtectionCache;

        public async Task<PublicHealthProtection> GetPublicHealthProtection(long id)
            => CacheAndReturn(await ExecuteHttpRequest<PublicHealthProtection>($"PublicHealthProtection/{id}"));

        public async Task<IEnumerable<PublicHealthProtection>> GetAllPublicHealthProtection()
            => CacheAndReturn(await ExecuteHttpRequest<IEnumerable<PublicHealthProtection>>($"PublicHealthProtection"), allObjects: true);

        public async Task<IEnumerable<PublicHealthProtection>> QueryPublicHealthProtection(ICriteria<PublicHealthProtection> criteria)
            => CacheAndReturn(await ExecuteHttpRequest<IEnumerable<PublicHealthProtection>>($"PublicHealthProtection"), allObjects: true)
               .Where(criteria.Filter.Compile());

        private IEnumerable<PublicHealthProtection> CacheAndReturn(IEnumerable<PublicHealthProtection> publicHealthProtection, bool allObjects = false)
        {
            if (allObjects)
            {
                _publicHealthProtectionCache.ClearCache();
            }
            _publicHealthProtectionCache.UpdateObjects(publicHealthProtection, DateTime.Now.AddDays(1), allObjects);
            return publicHealthProtection;
        }

        private PublicHealthProtection CacheAndReturn(PublicHealthProtection publicHealthProtection)
        {
            _publicHealthProtectionCache.UpdateObject(publicHealthProtection, DateTime.Now.AddDays(1));
            return publicHealthProtection;
        }
    }
}
