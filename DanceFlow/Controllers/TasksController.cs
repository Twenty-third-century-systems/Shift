using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using DanceFlow.Dtos;
using Drinkers.InternalClients.NameSearch;
using Drinkers.InternalClients.Task;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DanceFlow.Controllers {
    [Authorize(Policy = "IsExaminer")]
    [Route("tasks")]
    public class TasksController : Controller {
        private readonly ITaskApiClientService _taskApiClientService;
        private readonly INameSearchApiService _nameSearchApiService;

        public TasksController(ITaskApiClientService taskApiClientService, INameSearchApiService nameSearchApiService)
        {
            _taskApiClientService = taskApiClientService;
            _nameSearchApiService = nameSearchApiService;
        }

        [HttpGet("allocated")]
        public IActionResult Tasks()
        {
            return View();
        }


        [HttpGet("pending")]
        public async Task<IActionResult> Task(int task)
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

            // using (var client = new HttpClient())
            // {
            // var response = await client.GetAsync($"{ApiUrls.AllAllocatedTasks}/{user.Value}").Result.Content
            //     .ReadAsStringAsync();
            // try
            // {
            //     TasksForExaminerDto tasks = JsonConvert.DeserializeObject<TasksForExaminerDto>(response);
            //     return Ok(tasks);
            // }
            // catch (JsonReaderException ex)
            // {
            //     return NoContent();
            // }
            // }
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
            // using (var client = new HttpClient())
            // {
            //     var response = await client.GetAsync($"{ApiUrls.AllAllocatedTasks}/{task}/ns").Result.Content
            //         .ReadAsStringAsync();
            //     List<NameSearchTaskApplicationsDto> taskApplications =
            //         JsonConvert.DeserializeObject<List<NameSearchTaskApplicationsDto>>(response);
            //     return Ok(taskApplications);
            // }
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
            // using (var client = new HttpClient())
            // {
            //     var response = await client.GetAsync($"{ApiUrls.AllAllocatedTasks}/{task}/pla").Result.Content
            //         .ReadAsStringAsync();
            //     List<PvtApplicationTaskDto> taskApplications =
            //         JsonConvert.DeserializeObject<List<PvtApplicationTaskDto>>(response);
            //     return Ok(taskApplications);
            // }

            var taskPrivateEntityApplications = await _taskApiClientService.GetPvtEntityTaskApplication(task);
            if (taskPrivateEntityApplications != null)
                return Ok(taskPrivateEntityApplications);
            return NotFound();
        }


        [HttpGet("examination/{suggestedName}/contain")]
        public async Task<IActionResult> NamesThatContain(string suggestedName)
        {
            return Ok(await _nameSearchApiService.GetNamesThatContainAsync(suggestedName));
            // using (var client = new HttpClient())
            // {
            //     var namesToExaminer = new List<NameUnderExaminationResultDto>();
            //     var response = client.GetAsync($"{ApiUrls.AllNamesExamination}/{name}/{id}/contain").Result;
            //     if (response.IsSuccessStatusCode)
            //     {
            //         var strResp = await response.Content.ReadAsStringAsync();
            //         namesToExaminer = JsonConvert.DeserializeObject<List<NameUnderExaminationResultDto>>(strResp);
            //     }
            //
            //     return Ok(namesToExaminer);
            // }
        }


        [HttpGet("examination/{suggestedName}/starts")]
        public async Task<IActionResult> NamesThatStartWith(string suggestedName)
        {
            return Ok(await _nameSearchApiService.GetNamesThatStartWithAsync(suggestedName));
            // using (var client = new HttpClient())
            // {
            //     var namesToExaminer = new List<NameUnderExaminationResultDto>();
            //     var response = client.GetAsync($"{ApiUrls.AllNamesExamination}/{name}/{id}/starts").Result;
            //     if (response.IsSuccessStatusCode)
            //     {
            //         var strResp = await response.Content.ReadAsStringAsync();
            //         namesToExaminer = JsonConvert.DeserializeObject<List<NameUnderExaminationResultDto>>(strResp);
            //     }
            //
            //     return Ok(namesToExaminer);
            // }
        }

        [HttpGet("examination/{suggestedName}/ends")]
        public async Task<IActionResult> NamesThatEndWith(string suggestedName)
        {
            return Ok(await _nameSearchApiService.GetNamesThatEndWithAsync(suggestedName));
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