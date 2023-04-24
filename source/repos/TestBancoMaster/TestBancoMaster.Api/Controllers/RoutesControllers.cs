using Microsoft.AspNetCore.Mvc;
using TestBancoMaster.DomainModel.Dto;
using TestBancoMaster.DomainModel.Interfaces;
using TestBancoMaster.DomainModel.Models;

namespace TestBancoMaster.Api.Controllers
{
    [Route("api/")]
    [ApiController]
    public class RoutesControllers : ControllerBase
    {
        private readonly IRoutesService _routesService;
        IWebHostEnvironment _appEnvironment;

        public RoutesControllers(IRoutesService routeService, IWebHostEnvironment appEnvironment)
        {
            _routesService = routeService;
            _appEnvironment = appEnvironment;   
        }

        [HttpPost("addRoutes")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Post([FromBody] List<Routes> routes)
        {
            await _routesService.AddRoute(routes, _appEnvironment.WebRootPath);
            return Ok();
        }

        [HttpGet("getCheapestRoute")]
        [ProducesResponseType(typeof(RoutesDto), 200)]
        [ProducesResponseType(typeof(RoutesDto), 400)]
        public async Task<IActionResult> Get([FromQuery] string origin, [FromQuery] string destination)
        {
            var result = await _routesService.GetCheapestRoute(origin, destination, _appEnvironment.WebRootPath);
            return Ok(result);
        }
    }
}
