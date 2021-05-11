using RussiaPublicHealthProtection.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RussiaPublicHealthProtection.ApplicationServices.Ports.Gateways.Database
{
    public interface IPublicHealthProtectionDatabaseGateway
    {
        Task AddPublicHealthProtection(PublicHealthProtection publicHealthProtection);

        Task RemovePublicHealthProtection(PublicHealthProtection publicHealthProtection);

        Task UpdatePublicHealthProtection(PublicHealthProtection publicHealthProtection);

        Task<PublicHealthProtection> GetPublicHealthProtection(long id);

        Task<IEnumerable<PublicHealthProtection>> GetAllPublicHealthProtection();

        Task<IEnumerable<PublicHealthProtection>> QueryPublicHealthProtection(Expression<Func<PublicHealthProtection, bool>> filter);
        Task ParseAndPush();
    }
}
