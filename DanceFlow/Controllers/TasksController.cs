using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Cabinet.Dtos.Internal.Request;
using Drinkers.InternalApiClients.NameSearch;
using Drinkers.InternalApiClients.PvtEntity;
using Drinkers.InternalApiClients.Task;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DanceFlow.Controllers {
    [Authorize(Policy = "IsExaminer")]
    [Route("tasks")]
    public class TasksController : Controller {
        private readonly ITaskApiClientService _taskApiClientService;
        private readonly INameSearchApiClientService _nameSearchApiClientService;
        private readonly IMapper _mapper;
        private readonly IPvtEntityApiClientService _pvtEntityApiClientService;

        public TasksController(ITaskApiClientService taskApiClientService, INameSearchApiClientService nameSearchApiClientService,
            IMapper mapper, IPvtEntityApiClientService pvtEntityApiClientService)
        {
            _taskApiClientService = taskApiClientService;
            _nameSearchApiClientService = nameSearchApiClientService;
            _mapper = mapper;
            _pvtEntityApiClientService = pvtEntityApiClientService;
        }

        [HttpGet("allocated")]
        public IActionResult Tasks()
        {
            return View();
        }


        [HttpGet("pending")]
        public async Task<IActionResult> AllocatedTasks()
        {
            var user = User.Claims.FirstOrDefault(c =>
                c.Type.Equals("sub") && c.Issuer.Equals("https://localhost:5002"));
            if (user != null)
            {
                var allocatedTasks = await _taskApiClientService.GetAllocatedTasksAsync(Guid.Parse(user.Value));
                if (allocatedTasks != null)
                    return Ok(allocatedTasks);
                return NoContent();
            }

            return NotFound();
        }


        [HttpGet("name-search/{task}")]
        public IActionResult NameSearches(int task)
        {
            ViewBag.TaskId = task;
            return View();
        }


        [HttpGet("name-search/{taskId}/applications")]
        public async Task<IActionResult> NameSearchTaskApplications(int taskId)
        {
            return Ok(await _taskApiClientService.GetAllocatedNameSearchTaskApplicationsAsync(taskId));
        }


        [HttpGet("pvt-entity/{task}")]
        public IActionResult PvtEntities(int task)
        {
            ViewBag.TaskId = task;
            return View();
        }


        [HttpGet("pvt-entity/{task}/applications")]
        public async Task<IActionResult> PvtEntityTaskApplications(int task)
        {
            var taskPrivateEntityApplications = await _taskApiClientService.GetPvtEntityTaskApplicationAsync(task);
            if (taskPrivateEntityApplications != null)
                return Ok(taskPrivateEntityApplications);
            return NotFound();
        }


        [HttpGet("examination/{suggestedName}/contain")]
        public async Task<IActionResult> NamesThatContain(string suggestedName)
        {
            return Ok(await _nameSearchApiClientService.GetNamesThatContainAsync(suggestedName));
        }


        [HttpGet("examination/{suggestedName}/starts")]
        public async Task<IActionResult> NamesThatStartWith(string suggestedName)
        {
            return Ok(await _nameSearchApiClientService.GetNamesThatStartWithAsync(suggestedName));
        }

        [HttpGet("examination/{suggestedName}/ends")]
        public async Task<IActionResult> NamesThatEndWith(string suggestedName)
        {
            return Ok(await _nameSearchApiClientService.GetNamesThatEndWithAsync(suggestedName));
        }

        [HttpPost("examination/query/pvt-entity")]
        public async Task<IActionResult> PrivateEntityQuery(QueryRequestDto dto)
        {
            if (await _pvtEntityApiClientService.RaiseQueryAsync(_mapper.Map<RaisedQueryRequestDto>(dto)))
                return Ok();
            return BadRequest();
        }

        [HttpGet("finish/{taskId}")]
        public async Task<IActionResult> FinishTask(int taskId)
        {
            if (await _taskApiClientService.FinishTaskAsync(taskId))
                return RedirectToAction("Tasks");
            return BadRequest("Could not finish task. Try again.");
        }
    }
}