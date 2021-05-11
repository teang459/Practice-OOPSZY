using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace RussiaPublicHealthProtection.DomainObjects.Ports
{
    public interface IReadOnlyPublicHealthProtectionRepository
    {
        Task<PublicHealthProtection> GetPublicHealthProtection(long id);

        Task<IEnumerable<PublicHealthProtection>> GetAllPublicHealthProtection();

        Task<IEnumerable<PublicHealthProtection>> QueryPublicHealthProtection(ICriteria<PublicHealthProtection> criteria);

    }

    public interface IPublicHealthProtectionRepository
    {
        Task AddPublicHealthProtection(PublicHealthProtection publicHealthProtection);

        Task RemovePublicHealthProtection(PublicHealthProtection publicHealthProtection);
             
        Task UpdatePublicHealthProtection(PublicHealthProtection publicHealthProtection);
        Task ParseAndPush();
    }
}
