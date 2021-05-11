using RussiaPublicHealthProtection.DomainObjects;
using RussiaPublicHealthProtection.DomainObjects.Ports;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RussiaPublicHealthProtection.DomainObjects.Repositories
{
    public abstract class ReadOnlyPublicHealthProtectionRepositoryDecorator : IReadOnlyPublicHealthProtectionRepository
    {
        private readonly IReadOnlyPublicHealthProtectionRepository _publicHealthProtectionRepository;

        public ReadOnlyPublicHealthProtectionRepositoryDecorator(IReadOnlyPublicHealthProtectionRepository publicHealthProtectionRepository)
        {
            _publicHealthProtectionRepository = publicHealthProtectionRepository;
        }

        public virtual async Task<IEnumerable<PublicHealthProtection>> GetAllPublicHealthProtection()
        {
            return await _publicHealthProtectionRepository?.GetAllPublicHealthProtection();
        }

        public virtual async Task<PublicHealthProtection> GetPublicHealthProtection(long id)
        {
            return await _publicHealthProtectionRepository?.GetPublicHealthProtection(id);
        }

        public virtual async Task<IEnumerable<PublicHealthProtection>> QueryPublicHealthProtection(ICriteria<PublicHealthProtection> criteria)
        {
            return await _publicHealthProtectionRepository?.QueryPublicHealthProtection(criteria);
        }
    }
}
