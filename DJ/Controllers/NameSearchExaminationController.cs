using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TurnTable.InternalServices.NameSearchExamination;

namespace DJ.Controllers {
    [Route("api/ex/name")]
    public class NameSearchExaminationController : Controller {
        private readonly INameSearchExaminationService _nameSearchExaminationService;

        public NameSearchExaminationController(INameSearchExaminationService nameSearchExaminationService)
        {
            _nameSearchExaminationService = nameSearchExaminationService;
        }

        [AllowAnonymous]
        [HttpPatch("{nameId}/{status}")]
        public async Task<IActionResult> ExamineName(int nameId, int status)
        {
            if (await _nameSearchExaminationService.ChangeNameStatusAsync(nameId, status) > 0)
                return NoContent();
            return BadRequest("Could not examine name");
        }

        [HttpPatch("f/{nameSearchId}")]
        public async Task<IActionResult> FinishNameSearchExamination(int nameSearchId)
        {
            if (await _nameSearchExaminationService.FinishExaminationAsync(nameSearchId) > 0)
                return NoContent();
            return BadRequest("Could not finish examination. Try again later.");
        }

        [AllowAnonymous]
        [HttpGet("{suggestedName}/contain")]
        public async Task<IActionResult> NamesThatContain(string suggestedName)
        {
            return Ok( await _nameSearchExaminationService.GetNamesThatContainAsync(suggestedName));
        }

        [AllowAnonymous]
        [HttpGet("{suggestedName}/starts")]
        public async Task<IActionResult> NamesThatStartWith(string suggestedName)
        {
            return Ok( await _nameSearchExaminationService.GetNamesThatStartWithAsync(suggestedName));
        }
        
        [AllowAnonymous]
        [HttpGet("{suggestedName}/ends")]
        public async Task<IActionResult> NamesThatEndsWith(string suggestedName)
        {
            return Ok(await _nameSearchExaminationService.GetNamesThatEndsWithAsync(suggestedName));
        }
    }
}