using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Cabinet.Dtos.Internal.Request;
using DanceFlow.Clients.Task;
using DanceFlow.Dtos;
using DanceFlow.Models;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DanceFlow.Controllers {
    [Authorize(Policy = "IsRegistrar")]
    [Route("applications")]
    public class ApplicationsController : Controller {
        private readonly ITaskApiClientService _taskApiClientService;

        public ApplicationsController(ITaskApiClientService taskApiClientService)
        {
            _taskApiClientService = taskApiClientService;
        }

        [HttpGet("")]
        public async Task<IActionResult> AllocateTasks()
        {
            var authorityUrl = "https://localhost:5002/U";
            var office = User
                .Claims
                .FirstOrDefault(c => c.Type.Equals("office") && c.Issuer.Equals("https://localhost:5002"));
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"{authorityUrl}/{office.Value}")
                    .Result
                    .Content
                    .ReadAsStringAsync();
                var examiners = JsonConvert.DeserializeObject<List<ExaminersDto>>(response);
                ViewBag.Examiners = examiners;
            }

            var unallocatedApplications = await _taskApiClientService.GetAllUnallocatedApplicationsAsync(Int32.Parse(office.Value));
            if (unallocatedApplications != null)
            {                
                ViewBag.NameSearchCount = unallocatedApplications.Count(a => a.Service.Equals("NameSearch"));
                ViewBag.PrivateEntitiesCount = unallocatedApplications.Count(a => a.Service.Equals("PrivateLimitedCompany"));
            }
            else
            {
                ViewBag.NameSearchCount = 0;
                ViewBag.PrivateEntitiesCount = 0;
            }

            return View();
        }


        [HttpGet("all")]
        public async Task<IActionResult> GetAllApplications()
        {
            // using (var client = new HttpClient())
            // {
            var office = User.Claims
                .FirstOrDefault(c => c.Type.Equals("office") && c.Issuer.Equals("https://localhost:5002"));
            // var response = await client.GetAsync($"{ApiUrls.AllPendingApplications}/{office.Value}").Result.Content
            //     .ReadAsStringAsync();
            // AllApplicationsFromApiDto allApplicationsFromApi =
            //     JsonConvert.DeserializeObject<AllApplicationsFromApiDto>(response);
            //
            // int count = 0;
            // var authorityUrl = "https://localhost:5001/U";
            // foreach (var appliction in allApplicationsFromApi.NameSearchApplications)
            // {
            //     var applicantId = appliction.User;
            //     var responce = await client.GetAsync($"{authorityUrl}/{applicantId}")
            //         .Result
            //         .Content
            //         .ReadAsStringAsync();
            //     allApplicationsFromApi.NameSearchApplications[count].User = responce;
            //     count++;
            // }
            //
            // count = 0;
            //
            // foreach (var appliction in allApplicationsFromApi.PvtEntityApplications)
            // {
            //     var applicantId = appliction.User;
            //     var responce = await client.GetAsync($"{authorityUrl}/{applicantId}")
            //         .Result
            //         .Content
            //         .ReadAsStringAsync();
            //     allApplicationsFromApi.PvtEntityApplications[count].User = responce;
            //     count++;
            // }
            //
            // return Ok(allApplicationsFromApi);
            // }

            return NotFound();
        }


        [HttpPost("allocate")]
        public async Task<IActionResult> Allocate(NewTaskAllocationRequestDto dto)
        {
            var user = User.Claims
                .FirstOrDefault(c => c.Type.Equals("sub") && c.Issuer.Equals("https://localhost:5002"));
            var office = User.Claims
                .FirstOrDefault(c => c.Type.Equals("office") && c.Issuer.Equals("https://localhost:5002"));
            if (user != null) dto.AssignedBy = Guid.Parse(user.Value);
            else return NotFound();
            dto.DateAssigned = DateTime.Now;
            if (office != null) dto.SortingOffice = Convert.ToInt32(office.Value);
            else return NotFound();

            var multipleApplicationTask = await _taskApiClientService.PostMultipleApplicationTaskAsync(dto);
            if (multipleApplicationTask != null) return Ok(multipleApplicationTask);
            else return BadRequest("Could not allocate");

            // task.Allocator = user.Value;
            // using (var client = new HttpClient())
            // {
            //     var accessToken = await HttpContext.GetTokenAsync("access_token");
            //     client.SetBearerToken(accessToken);
            //     var response = await client
            //         .PostAsJsonAsync<TaskFromPrincipalDto>(ApiUrls.AllocateApplications, task).Result.Content
            //         .ReadAsStringAsync();
            //     TaskFromPrincipalDto savedTask = JsonConvert.DeserializeObject<TaskFromPrincipalDto>(response);
            //     return Created("", savedTask);
            // }
        }
    }
}