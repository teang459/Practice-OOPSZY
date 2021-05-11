using RussiaPublicHealthProtection.DomainObjects;
using RussiaPublicHealthProtection.DomainObjects.Ports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace RussiaPublicHealthProtection.ApplicationServices.GetPublicHealthProtectionListUseCase
{
    public class EventCriteria : ICriteria<PublicHealthProtection>
    {
        public string global_id { get; }

        public EventCriteria(string Event_type)
            => this.global_id = Event_type;

        public Expression<Func<PublicHealthProtection, bool>> Filter
            => (tr => tr.global_id == global_id);
    }
}
