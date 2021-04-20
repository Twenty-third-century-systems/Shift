using System.Threading.Tasks;
using Cabinet.Dtos.Internal.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TurnTable.InternalServices.PrivateEntityExamination;

namespace DJ.Controllers {
    [Route("api/ex/pvt")]
    public class PvtApplicationExaminationController : Controller {
        private readonly IPrivateEntityExaminationService _privateEntityExaminationService;

        public PvtApplicationExaminationController(IPrivateEntityExaminationService privateEntityExaminationService)
        {
            _privateEntityExaminationService = privateEntityExaminationService;
        }

        [AllowAnonymous]
        [HttpPost("q")]
        public async Task<IActionResult> RaiseQuery([FromBody] RaisedQueryRequestDto dto)
        {
            if (await _privateEntityExaminationService.RaiseQueryAsync(dto) > 0)
                return Created("", dto);
            return BadRequest("Could not raise query");
        }

        [AllowAnonymous]
        [HttpPatch("f/{applicationId}")]
        public async Task<IActionResult> FinishPvtApplicationExamination(int applicationId)
        {
            if (await _privateEntityExaminationService.FinishExaminationAsync(applicationId) > 0)
                return NoContent();

            return BadRequest("Could not finish this application.");
        }
    }
}