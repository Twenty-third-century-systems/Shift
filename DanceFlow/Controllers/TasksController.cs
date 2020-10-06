﻿using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using DanceFlow.Dtos;
using DanceFlow.Models;
using DJ.Dtos;
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
                var response = await client.GetAsync($"{ApiUrls.AllAllocatedTasks}/{task}/ns").Result.Content
                    .ReadAsStringAsync();
                List<NameSearchTaskApplicationsDto> taskApplications = JsonConvert.DeserializeObject<List<NameSearchTaskApplicationsDto>>(response);
                return Ok(taskApplications);
            }
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
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"{ApiUrls.AllAllocatedTasks}/{task}/pla").Result.Content
                    .ReadAsStringAsync();
                List<PvtApplicationTaskDto> taskApplications = JsonConvert.DeserializeObject<List<PvtApplicationTaskDto>>(response);
                return Ok(taskApplications);
            }
        }

        [HttpGet("examination/{name}/contain")]
        public async Task<IActionResult> NamesThatContain(string name)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"{ApiUrls.AllNamesExamination}/{name}/contain").Result.Content
                    .ReadAsStringAsync();
                var nameToExaminer = JsonConvert.DeserializeObject<List<NameOnExaminationDto>>(response);
                return Ok(nameToExaminer);
            }            
        }
        
        [HttpGet("examination/{name}/starts")]
        public IActionResult NamesThatStartWith(string name)
        {
            return Ok();
        }
    }
}