using RussiaPublicHealthProtection.DomainObjects;
using RussiaPublicHealthProtection.DomainObjects.Ports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RussiaPublicHealthProtection.ApplicationServices.Repositories
{
    public class InMemoryPublicHealthProtectionRepository : IReadOnlyPublicHealthProtectionRepository,
                                                    IPublicHealthProtectionRepository
    {
        private readonly List<PublicHealthProtection> _publicHealthProtection = new List<PublicHealthProtection>();

        public InMemoryPublicHealthProtectionRepository(IEnumerable<PublicHealthProtection> publicHealthProtection = null)
        {
            if (publicHealthProtection != null)
            {
                _publicHealthProtection.AddRange(publicHealthProtection);
            }
        }

        public Task AddPublicHealthProtection(PublicHealthProtection publicHealthProtection)
        {
            _publicHealthProtection.Add(publicHealthProtection);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<PublicHealthProtection>> GetAllPublicHealthProtection()
        {
            return Task.FromResult(_publicHealthProtection.AsEnumerable());
        }

        public Task<PublicHealthProtection> GetPublicHealthProtection(long id)
        {
            return Task.FromResult(_publicHealthProtection.Where(o => o.Id == id).FirstOrDefault());
        }

        public Task<IEnumerable<PublicHealthProtection>> QueryPublicHealthProtection(ICriteria<PublicHealthProtection> criteria)
        {
            return Task.FromResult(_publicHealthProtection.Where(criteria.Filter.Compile()).AsEnumerable());
        }

        public Task RemovePublicHealthProtection(PublicHealthProtection publicHealthProtection)
        {
            _publicHealthProtection.Remove(publicHealthProtection);
            return Task.CompletedTask;
        }

        public Task UpdatePublicHealthProtection(PublicHealthProtection publicHealthProtection)
        {
            var foundPublicHealthProtection = GetPublicHealthProtection(publicHealthProtection.Id).Result;
            if (foundPublicHealthProtection == null)
            {
                AddPublicHealthProtection(publicHealthProtection);
            }
            else
            {
                if (foundPublicHealthProtection != publicHealthProtection)
                {
                    _publicHealthProtection.Remove(foundPublicHealthProtection);
                    _publicHealthProtection.Add(publicHealthProtection);
                }
            }
            return Task.CompletedTask;
        }
        public Task ParseAndPush()
        {
            throw new NotImplementedException();
        }
    }
}
