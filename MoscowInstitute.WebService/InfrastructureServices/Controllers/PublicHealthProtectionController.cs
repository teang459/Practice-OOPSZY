using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RussiaPublicHealthProtection.DomainObjects;
using RussiaPublicHealthProtection.ApplicationServices.GetPublicHealthProtectionListUseCase;
using RussiaPublicHealthProtection.InfrastructureServices.Presenters;

namespace RussiaPublicHealthProtection.WebService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PublicHealthProtectionController : ControllerBase
    {
        private readonly ILogger<PublicHealthProtectionController> _logger;
        private readonly IGetPublicHealthProtectionListUseCase _getPublicHealthProtectionListUseCase;

        public PublicHealthProtectionController(ILogger<PublicHealthProtectionController> logger, IGetPublicHealthProtectionListUseCase getPublicHealthProtectionListUseCase)
        {
            _logger = logger;
            _getPublicHealthProtectionListUseCase = getPublicHealthProtectionListUseCase;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllPublicHealthProtections()
        {
            var presenter = new RussiaPublicHealthProtection.InfrastructureServices.Presenters.PublicHealthProtectionListPresenter();
            await _getPublicHealthProtectionListUseCase.Handle(GetPublicHealthProtectionListUseCaseRequest.CreateAllPublicHealthProtectionRequest(), presenter);
            return presenter.ContentResult;
        }

        [HttpGet("{publicHealthProtectionId}")]
        public async Task<ActionResult> GetPublicHealthProtection(long publicHealthProtectionId)
        {
            var presenter = new RussiaPublicHealthProtection.InfrastructureServices.Presenters.PublicHealthProtectionListPresenter();
            await _getPublicHealthProtectionListUseCase.Handle(GetPublicHealthProtectionListUseCaseRequest.CreatePublicHealthProtectionRequest(publicHealthProtectionId), presenter);
            return presenter.ContentResult;
        }
        [HttpGet("Events/{events}")]
        public async Task<ActionResult> GetEventFilter(string Event)
        {
            var presenter = new PublicHealthProtectionListPresenter();
            await _getPublicHealthProtectionListUseCase.Handle(GetPublicHealthProtectionListUseCaseRequest.CreateEventPublicHealthProtectionRequest(Event), presenter);
            return presenter.ContentResult;
        }
    }
}
