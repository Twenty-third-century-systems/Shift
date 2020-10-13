using System;
using System.Net.Http;
using System.Threading.Tasks;
using DanceFlow.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DanceFlow.Controllers {
    [Authorize]
    [Route("applications")]
    public class ApplicationsController : Controller {
        // GET
        [HttpGet("")]
        public IActionResult AllApplications()
        {
            return View();
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllApplications()
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(ApiUrls.AllPendingApplications).Result.Content
                    .ReadAsStringAsync();
                AllApplicationsFromApiDto allApplicationsFromApi = JsonConvert.DeserializeObject<AllApplicationsFromApiDto>(response);
                return Ok(allApplicationsFromApi);
            }
        }        

        [HttpPost("allocate")]
        public async Task<IActionResult> Allocate(TaskFromPrincipalDto task)
        {
            using (var client = new HttpClient())
            {
                var response = await client
                    .PostAsJsonAsync<TaskFromPrincipalDto>(ApiUrls.AllocateApplications, task).Result.Content
                    .ReadAsStringAsync();
                TaskFromPrincipalDto savedTask = JsonConvert.DeserializeObject<TaskFromPrincipalDto>(response);
                return Created("", savedTask);                
            }
        }
    }
}