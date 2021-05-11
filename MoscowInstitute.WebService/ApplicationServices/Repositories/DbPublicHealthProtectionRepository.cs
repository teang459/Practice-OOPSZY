using System;
using RussiaPublicHealthProtection.DomainObjects;
using RussiaPublicHealthProtection.DomainObjects.Ports;
using RussiaPublicHealthProtection.ApplicationServices.Ports.Gateways.Database;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RussiaPublicHealthProtection.ApplicationServices.Repositories
{
    public class DbPublicHealthProtectionRepository : IReadOnlyPublicHealthProtectionRepository,
                                                  IPublicHealthProtectionRepository
    {
        private readonly IPublicHealthProtectionDatabaseGateway _databaseGateway;

        public DbPublicHealthProtectionRepository(IPublicHealthProtectionDatabaseGateway databaseGateway)
            => _databaseGateway = databaseGateway;

        public async Task<PublicHealthProtection> GetPublicHealthProtection(long id)
            => await _databaseGateway.GetPublicHealthProtection(id);

        public async Task<IEnumerable<PublicHealthProtection>> GetAllPublicHealthProtection()
            => await _databaseGateway.GetAllPublicHealthProtection();

        public async Task<IEnumerable<PublicHealthProtection>> QueryPublicHealthProtection(ICriteria<PublicHealthProtection> criteria)
            => await _databaseGateway.QueryPublicHealthProtection(criteria.Filter);

        public async Task AddPublicHealthProtection(PublicHealthProtection publicHealthProtection)
            => await _databaseGateway.AddPublicHealthProtection(publicHealthProtection);

        public async Task RemovePublicHealthProtection(PublicHealthProtection publicHealthProtection)
            => await _databaseGateway.RemovePublicHealthProtection(publicHealthProtection);

        public async Task UpdatePublicHealthProtection(PublicHealthProtection publicHealthProtection)
            => await _databaseGateway.UpdatePublicHealthProtection(publicHealthProtection);

        public async Task ParseAndPush()
            => await _databaseGateway.ParseAndPush();
    }
}
