using RussiaPublicHealthProtection.ApplicationServices.Ports.Cache;
using RussiaPublicHealthProtection.DomainObjects;
using RussiaPublicHealthProtection.DomainObjects.Ports;
using RussiaPublicHealthProtection.DomainObjects.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RussiaPublicHealthProtection.InfrastructureServices.Repositories
{
    public class CachedReadOnlyPublicHealthProtectionRepository : ReadOnlyPublicHealthProtectionRepositoryDecorator
    {
        private readonly IDomainObjectsCache<PublicHealthProtection> _publicHealthProtectionsCache;

        public CachedReadOnlyPublicHealthProtectionRepository(IReadOnlyPublicHealthProtectionRepository publicHealthProtectionRepository,
                                             IDomainObjectsCache<PublicHealthProtection> publicHealthProtectionsCache)
            : base(publicHealthProtectionRepository)
            => _publicHealthProtectionsCache = publicHealthProtectionsCache;

        public async override Task<PublicHealthProtection> GetPublicHealthProtection(long id)
            => _publicHealthProtectionsCache.GetObject(id) ?? await base.GetPublicHealthProtection(id);

        public async override Task<IEnumerable<PublicHealthProtection>> GetAllPublicHealthProtection()
            => _publicHealthProtectionsCache.GetObjects() ?? await base.GetAllPublicHealthProtection();

        public async override Task<IEnumerable<PublicHealthProtection>> QueryPublicHealthProtection(ICriteria<PublicHealthProtection> criteria)
            => _publicHealthProtectionsCache.GetObjects()?.Where(criteria.Filter.Compile()) ?? await base.QueryPublicHealthProtection(criteria);
    }
}
