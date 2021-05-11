using RussiaPublicHealthProtection.DomainObjects;
using RussiaPublicHealthProtection.ApplicationServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RussiaPublicHealthProtection.ApplicationServices.GetPublicHealthProtectionListUseCase
{
    public class GetPublicHealthProtectionListUseCaseResponse : UseCaseResponse
    {
        public IEnumerable<PublicHealthProtection> PublicHealthProtection { get; }

        public GetPublicHealthProtectionListUseCaseResponse(IEnumerable<PublicHealthProtection> publicHealthProtection) => PublicHealthProtection = publicHealthProtection;
    }
}
