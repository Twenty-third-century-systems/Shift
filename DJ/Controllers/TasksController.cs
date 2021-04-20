using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TurnTable.InternalServices.Task;

namespace DJ.Controllers {
    [Route("api/tasks")]
    public class TasksController : Controller {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [AllowAnonymous]
        [HttpGet("{examinerId}")]
        public async Task<IActionResult> GetTasks(Guid examinerId)
        {
            return Ok(await _taskService.GetAllocatedTasksAsync(examinerId));
        }

        [HttpGet("{taskId}/ns")]
        public async Task<IActionResult> GetNameSearchTask(int taskId)
        {
            return Ok(await _taskService.GetNameSearchTaskApplicationsAsync(taskId));
        }

        [AllowAnonymous]
        [HttpGet("{taskId}/pla")]
        public async Task<IActionResult> GetPvtApplicationTask(int taskId)
        {
            return Ok(await _taskService.GetPrivateEntityTaskApplicationAsync(taskId));
        }

        [AllowAnonymous]
        [HttpHead("{taskId}/f")]
        public async Task<IActionResult> FinishTask(int taskId)
        {
            if (await _taskService.FinishTaskAsync(taskId) > 0)
                return NoContent();
            return BadRequest("Could not finish task");
        }
    }
}