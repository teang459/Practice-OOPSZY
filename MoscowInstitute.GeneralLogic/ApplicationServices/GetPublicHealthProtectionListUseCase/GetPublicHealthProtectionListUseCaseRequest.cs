using RussiaPublicHealthProtection.ApplicationServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RussiaPublicHealthProtection.ApplicationServices.GetPublicHealthProtectionListUseCase
{
    public class GetPublicHealthProtectionListUseCaseRequest : IUseCaseRequest<GetPublicHealthProtectionListUseCaseResponse>
    {
        public string global_id { get; private set; }
        public long? PublicHealthProtectionId { get; private set; }

        private GetPublicHealthProtectionListUseCaseRequest()
        { }

        public static GetPublicHealthProtectionListUseCaseRequest CreateAllPublicHealthProtectionRequest()
        {
            return new GetPublicHealthProtectionListUseCaseRequest();
        }

        public static GetPublicHealthProtectionListUseCaseRequest CreatePublicHealthProtectionRequest(long publicHealthProtectionId)
        {
            return new GetPublicHealthProtectionListUseCaseRequest() { PublicHealthProtectionId = publicHealthProtectionId };
        }
            public static GetPublicHealthProtectionListUseCaseRequest CreateEventPublicHealthProtectionRequest(string global_id)
            {
            return new GetPublicHealthProtectionListUseCaseRequest() { global_id = global_id };  
        }

        }
    }


