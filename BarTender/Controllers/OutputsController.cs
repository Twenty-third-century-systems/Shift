using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TurnTable.ExternalServices.Outputs;

namespace BarTender.Controllers {
    [Authorize]
    [Route("api/outputs")]
    public class OutputsController : Controller {
        private readonly IOutputsService _outputsService;

        public OutputsController(IOutputsService outputsService)
        {
            _outputsService = outputsService;
        }

        [AllowAnonymous]
        [HttpGet("ns/{applicationId}/sum")]
        public async Task<IActionResult> GetRegisteredNameSummary(int applicationId)
        {
            return Ok(await _outputsService.NameSearchSummary(applicationId));
        }

        [AllowAnonymous]
        [HttpGet("pvt/{applicationId}/ns/sum")]
        public async Task<IActionResult> UsedNameSearchSummary(int applicationId)
        {
            return Ok(await _outputsService.GetUsedNameSearchApplicationId(applicationId));
        }

        [AllowAnonymous]
        [HttpGet("pvt/{applicationId}/sum")]
        public async Task<IActionResult> GetRegisteredPrivateEntitySummary(int applicationId)
        {
            return Ok(await _outputsService.GetRegisteredPrivateEntitySummary(applicationId));
        }

        [AllowAnonymous]
        [HttpGet("pvt/cert/{applicationId}")]
        public async Task<IActionResult> GetRegisteredPrivateEntityCertificate(int applicationId)
        {
            return Ok(await _outputsService.GetRegisteredPrivateEntity(applicationId));
        }
    }
}