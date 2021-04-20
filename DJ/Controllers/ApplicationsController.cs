using System.Threading.Tasks;
using Cabinet.Dtos.Internal.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TurnTable.InternalServices.Applications;
using TurnTable.InternalServices.Task;

namespace DJ.Controllers {
    [Route("api/applications")]
    public class ApplicationsController : Controller {
        private readonly ITaskService _taskService;
        private readonly IApplicationsService _applicationsService;

        public ApplicationsController(ITaskService taskService,
            IApplicationsService applicationsService)
        {
            _taskService = taskService;
            _applicationsService = applicationsService;
        }

        [AllowAnonymous]
        [HttpGet("all/{sortingOffice}")]
        public async Task<IActionResult> UnallocatedApplications(int sortingOffice)
        {
            return Ok(await _taskService.GetAllUnAllocatedApplicationsAsync(sortingOffice));
        }

        [HttpPost("allocate")]
        public async Task<IActionResult> AllocateApplicationsToExaminer([FromBody] NewTaskAllocationRequestDto dto)
        {
            return Created("", await _taskService.AllocateTasksAsync(dto));
        }

        [HttpGet("{sortingOffice}/pending/approval")]
        public async Task<IActionResult> ApplicationsPendingApproval(int sortingOffice)
        {
            return Ok( await _applicationsService.GetApplicationsPendingApproval(sortingOffice));
        }


        [HttpPatch("{applicationId}/approve")]
        public async Task<IActionResult> ApproveApplication(int applicationId)
        {
            if (await _applicationsService.ApprovePrivateEntityApplication(applicationId) > 0)
                return NoContent();
            return BadRequest();
        }
    }
}