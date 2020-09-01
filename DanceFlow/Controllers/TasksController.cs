using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using DanceFlow.Dtos;
using DJ.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DanceFlow.Controllers {
    [Route("tasks")]
    public class TasksController : Controller {
        // GET
        [HttpGet("allocated")]
        public IActionResult Tasks()
        {
            return View();
        }

        [HttpGet("pending")]
        public async Task<IActionResult> Task(int task)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(ApiUrls.AllAllocatedTasks	).Result.Content
                    .ReadAsStringAsync();
                try
                {
                    TasksForExaminerDto tasks = JsonConvert.DeserializeObject<TasksForExaminerDto>(response);
                    return Ok(tasks);
                }
                catch (JsonReaderException ex)
                {
                    return NoContent();
                }
                
            }
        }

        [HttpGet("name-search/{task}")]
        public IActionResult NameSearches(int task)
        {
            ViewBag.TaskId = task;
            return View();
        }

        [HttpGet("name-search/{task}/applications")]
        public async Task<IActionResult> NameSearchTaskApplications(int task)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"{ApiUrls.AllAllocatedTasks}/{task}").Result.Content
                    .ReadAsStringAsync();
                List<NameSearchTaskApplicationsDto> taskApplications = JsonConvert.DeserializeObject<List<NameSearchTaskApplicationsDto>>(response);
                return Ok(taskApplications);
            }
            return Ok();
        }
        
        [HttpGet("pvt-entity/{task}")]
        public IActionResult PvtEntity(int task)
        {
            return View();
        }
    }
}