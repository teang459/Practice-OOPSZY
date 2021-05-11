using System.Threading.Tasks;
using System.Collections.Generic;
using RussiaPublicHealthProtection.DomainObjects;
using RussiaPublicHealthProtection.DomainObjects.Ports;
using RussiaPublicHealthProtection.ApplicationServices.Ports;

namespace RussiaPublicHealthProtection.ApplicationServices.GetPublicHealthProtectionListUseCase
{
    public class GetPublicHealthProtectionListUseCase : IGetPublicHealthProtectionListUseCase
    {
        private readonly IReadOnlyPublicHealthProtectionRepository _readOnlyPublicHealthProtectionRepository;

        public GetPublicHealthProtectionListUseCase(IReadOnlyPublicHealthProtectionRepository readOnlyPublicHealthProtectionRepository)
            => _readOnlyPublicHealthProtectionRepository = readOnlyPublicHealthProtectionRepository;

        public async Task<bool> Handle(GetPublicHealthProtectionListUseCaseRequest request, IOutputPort<GetPublicHealthProtectionListUseCaseResponse> outputPort)
        {
            IEnumerable<PublicHealthProtection> publicHealthProtections = null;
            if (request.PublicHealthProtectionId != null)
            {
                var publicHealthProtection = await _readOnlyPublicHealthProtectionRepository.GetPublicHealthProtection(request.PublicHealthProtectionId.Value);
                publicHealthProtections = (publicHealthProtection != null) ? new List<PublicHealthProtection>() { publicHealthProtection } : new List<PublicHealthProtection>();

            }
            else if (request.global_id != null)
            {
                publicHealthProtections = await _readOnlyPublicHealthProtectionRepository.QueryPublicHealthProtection(new EventCriteria(request.global_id));
            }
            else
            {
                publicHealthProtections = await _readOnlyPublicHealthProtectionRepository.GetAllPublicHealthProtection();
            }
            outputPort.Handle(new GetPublicHealthProtectionListUseCaseResponse(publicHealthProtections));
            return true;
        }
    }
}
