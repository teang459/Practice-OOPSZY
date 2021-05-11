using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RussiaPublicHealthProtection.ApplicationServices.Interfaces;

namespace RussiaPublicHealthProtection.ApplicationServices.GetPublicHealthProtectionListUseCase
{
    public interface IGetPublicHealthProtectionListUseCase : IUseCase<GetPublicHealthProtectionListUseCaseRequest, GetPublicHealthProtectionListUseCaseResponse>
    {
    }
}
