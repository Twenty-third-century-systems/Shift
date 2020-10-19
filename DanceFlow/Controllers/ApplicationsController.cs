using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
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
        // GET
        [HttpGet("")]
        public async Task<IActionResult> AllApplications()
        {
            var authorityUrl = "https://localhost:5002/U";
            using (var client = new HttpClient())
            {
                var office = User
                    .Claims
                    .Where(c => c.Type.Equals("office")&& c.Issuer.Equals("https://localhost:5002"))
                    .FirstOrDefault();
                var responce = await client.GetAsync($"{authorityUrl}/{office.Value}")
                    .Result
                    .Content
                    .ReadAsStringAsync();
                var examiners = JsonConvert.DeserializeObject<List<ExaminersDto>>(responce);
                ViewBag.Examiners = examiners;
            }

            return View();
        }

        
        [HttpGet("all")]
        public async Task<IActionResult> GetAllApplications()
        {
            using (var client = new HttpClient())
            {
                var office = User
                    .Claims
                    .Where(c => c.Type.Equals("office")&& c.Issuer.Equals("https://localhost:5002"))
                    .FirstOrDefault();
                var response = await client.GetAsync($"{ApiUrls.AllPendingApplications}/{office.Value}").Result.Content
                    .ReadAsStringAsync();
                AllApplicationsFromApiDto allApplicationsFromApi = JsonConvert.DeserializeObject<AllApplicationsFromApiDto>(response);
                
                int count = 0;
                var authorityUrl = "https://localhost:5001/U";
                foreach (var appliction in allApplicationsFromApi.NameSearchApplications)
                {
                    var applicantId = appliction.User;
                    var responce = await client.GetAsync($"{authorityUrl}/{applicantId}")
                        .Result
                        .Content
                        .ReadAsStringAsync();
                    allApplicationsFromApi.NameSearchApplications[count].User = responce;
                    count++;
                }

                count = 0;
                
                foreach (var appliction in allApplicationsFromApi.PvtEntityApplications)
                {
                    var applicantId = appliction.User;
                    var responce = await client.GetAsync($"{authorityUrl}/{applicantId}")
                        .Result
                        .Content
                        .ReadAsStringAsync();
                    allApplicationsFromApi.PvtEntityApplications[count].User = responce;
                    count++;
                }
                return Ok(allApplicationsFromApi);
            }
        }        

        
        [HttpPost("allocate")]
        public async Task<IActionResult> Allocate(TaskFromPrincipalDto task)
        {
            var user = User
                .Claims
                .Where(c => c.Type.Equals("sub")&& c.Issuer.Equals("https://localhost:5002"))
                .FirstOrDefault();
            task.Allocator = user.Value;
            using (var client = new HttpClient())
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                client.SetBearerToken(accessToken);
                var response = await client
                    .PostAsJsonAsync<TaskFromPrincipalDto>(ApiUrls.AllocateApplications, task).Result.Content
                    .ReadAsStringAsync();
                TaskFromPrincipalDto savedTask = JsonConvert.DeserializeObject<TaskFromPrincipalDto>(response);
                return Created("", savedTask);                
            }
        }
    }
}