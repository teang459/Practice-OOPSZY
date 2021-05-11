using System.Net;
using Newtonsoft.Json;
using RussiaPublicHealthProtection.ApplicationServices.Ports;
using RussiaPublicHealthProtection.ApplicationServices.GetPublicHealthProtectionListUseCase;

namespace RussiaPublicHealthProtection.InfrastructureServices.Presenters
{
    public class PublicHealthProtectionListPresenter : IOutputPort<GetPublicHealthProtectionListUseCaseResponse>
    {
        public JsonContentResult ContentResult { get; }

        public PublicHealthProtectionListPresenter()
        {
            ContentResult = new JsonContentResult();
        }

        public void Handle(GetPublicHealthProtectionListUseCaseResponse response)
        {
            ContentResult.StatusCode = (int)(response.Success ? HttpStatusCode.OK : HttpStatusCode.NotFound);
            ContentResult.Content = response.Success ? JsonConvert.SerializeObject(response.PublicHealthProtection) : JsonConvert.SerializeObject(response.Message);
        }
    }
}
